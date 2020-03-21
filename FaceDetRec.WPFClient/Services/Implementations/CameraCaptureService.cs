using Emgu.CV;
using Emgu.CV.Structure;
using System.ComponentModel;
using FaceDetRec.WPFClient.Services.Interfaces;
using System.Diagnostics;

namespace FaceDetRec.WPFClient.Services.Implementations
{
    public class CameraCaptureService : ICameraCaptureService
    {
        private VideoCapture _capture;
        private BackgroundWorker _cameraWorker;

        public event ImageChangedEventHndler ImageChanged;
        public delegate void ImageChangedEventHndler(object sender, Image<Bgr, byte> image);

        public bool IsStarted() => _cameraWorker != null 
            && _cameraWorker.IsBusy;

        public void StartServiceAsync()
        {
            _cameraWorker.RunWorkerAsync();
        }

        public void StopServiceAsync()
        {
            _cameraWorker?.CancelAsync();
        }

        private void RaiseImageChangedEvent(Image<Bgr, byte> image)
        {
            ImageChanged?.Invoke(this, image);
        }

        public void StartWorkers()
        {
            _cameraWorker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };

            _cameraWorker.DoWork += _cameraWorker_DoWork;
            _cameraWorker.RunWorkerCompleted += _cameraWorker_RunWorkerCompleted;
        }

        private void _cameraWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                //todo: zasygnalizowac jakos koniec dzialania
            }
        }

        private void _cameraWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!_cameraWorker.CancellationPending)
            {
                if (_capture != null)
                {
                    try
                    {
                        var frame = _capture.QueryFrame();
                        var frameImage = frame.ToImage<Bgr, byte>();

                        RaiseImageChangedEvent(frameImage);
                    }
                    catch
                    {
                        Debug.WriteLine("Camera worker issue");
                    }                    
                }                
            }

            if (_cameraWorker.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        public void StartCapturing(int index)
        {
            if (_capture != null) return;

            _capture = new VideoCapture(index);

            StartWorkers();
            StartServiceAsync();
        }

        public void StopCapturing()
        {
            _capture = null;
            
            StopServiceAsync();
        }
    }
}

using Emgu.CV;
using Emgu.CV.Structure;
using FaceDetRec.WPFClient.Common.Enums;
using FaceDetRec.WPFClient.Config;
using FaceDetRec.WPFClient.DataModels;
using FaceDetRec.WPFClient.Services.Interfaces;
using FaceDetRec.WPFClient.Utils;
using FaceDetRec.WPFClient.ViewModels.MainWindow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Size = System.Drawing.Size;

namespace FaceDetRec.WPFClient.Services.Implementations
{
    public class FaceDetectionService : CameraCaptureService, IFaceDetectionService
    {
        private readonly IControls _controls;
        private readonly IParameters _parameters;
        private readonly IDatabaseService _databaseService;
        private readonly ILocalStorageService _localStorageService;
        private readonly IFileDirectoryService _fileDirectoryService;
        private readonly BackgroundWorker _saveWorker;
        private readonly IFaceRecogntionService _faceRecogntionService;

        private IEnumerable<Rectangle> _faces;
        private IEnumerable<Rectangle> _eyes;
        private bool _isDetecting;

        public event ImageWithDetectionChangedEventHandler ImageWithDetectionChanged;
        public event FaceDetectedEventHandler FaceDetected;

        public delegate void
            ImageWithDetectionChangedEventHandler(object sender, Image<Bgr, byte> image);
        public delegate void
            FaceDetectedEventHandler(object sender, Image<Bgr, byte> image, Bitmap bitmapImage);

        public FaceDetectionService(
            IDatabaseService databaseService,
            IControls controls,
            IParameters parameters,
            IFileDirectoryService fileDirectoryService,
            ILocalStorageService localStorageService, IFaceRecogntionService faceRecogntionService)
        {
            _controls = controls;
            _parameters = parameters;
            _fileDirectoryService = fileDirectoryService;
            _localStorageService = localStorageService;
            _faceRecogntionService = faceRecogntionService;
            _databaseService = databaseService;

            _faces = new List<Rectangle>();
            _eyes = new List<Rectangle>();

            StartServices();

            _saveWorker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };

            _saveWorker.DoWork += _saveWorker_DoWork;
            _saveWorker.RunWorkerCompleted += _saveWorker_RunWorkerCompleted;
        }

        public void ToogleCameraService()
        {
            if (!IsStarted())
            {
                Task.Delay(700).ContinueWith(_ =>
                {
                    StartCapturing(_parameters.DeviceIndex);

                    _controls.IsDeviceChangeEnabled = false;
                    _controls.ServiceButtonText = ButtonText.NoRetrieve.GetDesciption();
                    _controls.Name = string.Empty;
                    _controls.Age = string.Empty;
                    _controls.Address = string.Empty;
                    _controls.Details = string.Empty;

                    _controls.StatusText = StatusTypes.StreamStarted.GetDesciption();
                    _controls.IsNewUpdate = true;

                    _controls.IsSaveEnabled = false;

                    StatusBarUtility.ResestStatus(_controls);
                });

            }
            else
            {
                StopCapturing();

                _controls.IsDeviceChangeEnabled = true;
                _controls.ServiceButtonText = ButtonText.Retrieve.GetDesciption();
                _controls.KnownPeople = _parameters.UseExplorer 
                    ? _localStorageService.GetPeopleNames()
                    : _databaseService.GetPeopleNames();

                _controls.StatusText = StatusTypes.StreamPaused.GetDesciption();
                _controls.IsNewUpdate = true;

                if (_controls.TheNewestFace != null)
                {
                    _controls.IsSaveEnabled = true;
                }

                StatusBarUtility.ResestStatus(_controls);
            }
        }

        public void ChangeDevice()
        {
            _parameters.DeviceIndex = _controls.SelectedVideoInput.Index;
        }

        public void SaveDetected()
        {
            var person = new PersonModelBase()
            {
                Name = _controls.Name,
                Age = Convert.ToInt32(_controls.Age),
                Address = _controls.Address,
                Details = _controls.Details
            };

            var image = new ImageModelBase()
            {
                Data = BitmapUtility.BitmapToByteArray(_controls.TheNewestFace)
            };

            try
            {
                if (_parameters.UseExplorer)
                {
                    _localStorageService.SaveFace(person, image);
                }
                else
                {
                    _databaseService.SaveFace(new PersonModel(person), new ImageModel(image));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Wystąpił błąd podczas zapisu do bazy danych\r\n\r\n" + e);
            }

            _controls.KnownPeople = _parameters.UseExplorer
                ? _localStorageService.GetPeopleNames()
                : _databaseService.GetPeopleNames();
            _controls.StatusText = StatusTypes.SaveCompleted.GetDesciption();
            _controls.IsNewUpdate = true;

            StatusBarUtility.ResestStatus(_controls);
        }

        public void LoadPersonalData()
        {
            PersonModelBase person;
            if (_parameters.UseExplorer)
            {
                person = _localStorageService.GetPerson(_controls.Name);
            }
            else
            {
                var dbPerson = _databaseService.GetPersonalData(_controls.Name);

                if (dbPerson == null)
                {
                    return;
                }

                person = new PersonModelBase
                {
                    Id = dbPerson.Id,
                    Name = dbPerson.Name,
                    Age = (int)dbPerson.Age,
                    Address = dbPerson.Address,
                    Details = dbPerson.Details
                };
            }

            _controls.Age = person.Age.ToString();
            _controls.Address = person.Address;
            _controls.Details = person.Details;
        }

        public void ShouldDetectEyes()
        {
            _controls.AreEyesDetected = !_controls.AreEyesDetected;
        }

        public void ChangeDataMode()
        {
            _controls.IsExplorerPreffered = !_controls.IsExplorerPreffered;
            _parameters.UseExplorer = _controls.IsExplorerPreffered;

            if (_controls.IsExplorerPreffered
                && !_fileDirectoryService.CheckIfLocalStorageStructuresArePresent())
            {
                _fileDirectoryService.CreateLocalStorageStructures();
            }

            _faceRecogntionService.RetrainRecognizer();
        }

        public void StartServices()
        {
            ImageChanged += FaceDetectionService_ImageChanged;
        }

        private void RaiseImageWithDetectionChangedEvent(Image<Bgr, byte> image)
        {
            ImageWithDetectionChanged?.Invoke(this, image);

            try
            {
                if (image.Height > 0 && image.Width > 0)
                {
                    _controls.TheFrame = image.Bitmap;
                }                
            }
            catch (Exception)
            {
                Debug.WriteLine("Detection changed issue");
            }
        }

        private void RaiseFaceDetectedEvent(Image<Bgr, byte> image, Bitmap bitmap)
        {
            FaceDetected?.Invoke(this, image, bitmap);

            _controls.TheNewestFace = bitmap;
        }

        private async void FaceDetectionService_ImageChanged(object sender, Image<Bgr, byte> image)
        {
            var isDelayed = false;

            if (!_isDetecting)
            {
                _isDetecting = true;

                if (_controls.AreEyesDetected)
                {
                    var result = await DetectFacesAndEyesAsync(image);

                    isDelayed = true;

                    _faces = result.Item1;
                    _eyes = result.Item2;
                    _isDetecting = false;
                }
                else
                {
                    var result = await DetectFacesAsync(image);
                    isDelayed = true;

                    _faces = result;
                    _isDetecting = false;
                }

                _isDetecting = false;
            }

            if (isDelayed) return;

            DrawRectangles(image);
            RaiseImageWithDetectionChangedEvent(image);
        }

        private void DrawRectangles(Image<Bgr, byte> image)
        {
            foreach (var face in _faces)
                image.Draw(face, new Bgr(Color.DarkRed), 2);

            foreach (var eye in _eyes)
                image.Draw(eye, new Bgr(Color.DimGray), 2);
        }

        private static Bitmap CaptureFace(Image<Bgr, byte> image, Rectangle faceSelection)
        {
            var croppedImage = BitmapUtility.CropImage(image.Bitmap, faceSelection);

            return croppedImage;
        }

        private Task<Tuple<List<Rectangle>, List<Rectangle>>> DetectFacesAndEyesAsync(Image<Bgr, byte> image)
        {
            return Task.Run(() =>
            {
                var faces = new List<Rectangle>();
                var eyes = new List<Rectangle>();

                DetectFaceAndEyes(image, faces, eyes);

                if (faces.Any())
                {
                    RaiseFaceDetectedEvent(image, CaptureFace(image, faces.FirstOrDefault()));
                    _controls.IsAnyFaceMarked = true;
                }
                else
                {
                    _controls.IsAnyFaceMarked = false;
                }

                return new Tuple<List<Rectangle>, List<Rectangle>>(faces, eyes);
            });
        }

        private Task<List<Rectangle>> DetectFacesAsync(Image<Bgr, byte> image)
        {
            return Task.Run(() =>
            {
                var faces = new List<Rectangle>();

                DetectFace(image, faces);

                if (faces.Any())
                {
                    RaiseFaceDetectedEvent(image, CaptureFace(image, faces.FirstOrDefault()));
                    _controls.IsAnyFaceMarked = true;
                }
                else
                {
                    _controls.IsAnyFaceMarked = false;
                }

                return faces;
            });
        }

        private void DetectFace(Image<Bgr, byte> image, List<Rectangle> faces)
        {
            //Read the HaarCascade objects
            using (var face = new CascadeClassifier(RecognizerConfig.CascadeFaceFilePath))
            {
                using (var gray = image.Convert<Gray, byte>()) //Convert it to Grayscale
                {
                    //normalizes brightness and increases contrast of the image
                    gray._EqualizeHist();

                    //Detect the faces  from the gray scale image and store the locations as rectangle
                    //The first dimensional is the channel
                    //The second dimension is the index of the rectangle in the specific channel
                    var facesDetected = face.DetectMultiScale(
                        gray,
                        1.1,
                        10,
                        new Size(20, 20),
                        Size.Empty);

                    faces.AddRange(facesDetected);
                }
            }
        }

        private void DetectFaceAndEyes(Image<Bgr, byte> image, List<Rectangle> faces, ICollection<Rectangle> eyes)
        {
            //Read the HaarCascade objects
            using (var face = new CascadeClassifier(RecognizerConfig.CascadeFaceFilePath))
            using (var eye = new CascadeClassifier(RecognizerConfig.CascadeEyeFilePath))
            {
                using (var gray = image.Convert<Gray, byte>()) //Convert it to Grayscale
                {
                    //normalizes brightness and increases contrast of the image
                    gray._EqualizeHist();

                    //Detect the faces  from the gray scale image and store the locations as rectangle
                    //The first dimensional is the channel
                    //The second dimension is the index of the rectangle in the specific channel
                    var facesDetected = face.DetectMultiScale(
                        gray,
                        1.1,
                        10,
                        new Size(20, 20),
                        Size.Empty);

                    faces.AddRange(facesDetected);

                    foreach (var faceDetected in facesDetected)
                    {
                        //Set the region of interest on the faces
                        gray.ROI = faceDetected;
                        var eyesDetected = eye.DetectMultiScale(
                            gray,
                            1.1,
                            10,
                            new Size(20, 20),
                            Size.Empty);
                        gray.ROI = Rectangle.Empty;

                        foreach (var eyeDetected in eyesDetected)
                        {
                            var eyeRect = eyeDetected;
                            eyeRect.Offset(faceDetected.X, faceDetected.Y);
                            eyes.Add(eyeRect);
                        }
                    }
                }
            }
        }

        private void _saveWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!_saveWorker.CancellationPending)
            {
                SaveDetected();
            }

            if (_saveWorker.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void _saveWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                //todo: zasygnalizowac jakos koniec dzialania
            }
        }
    }
}

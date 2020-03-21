using Emgu.CV;
using Emgu.CV.Structure;
using FaceDetRec.WPFClient.Common.Enums;
using FaceDetRec.WPFClient.Config;
using FaceDetRec.WPFClient.Services.Interfaces;
using FaceDetRec.WPFClient.Utils;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace FaceDetRec.WPFClient.ViewModels.MainWindow
{
    public class Shell : IShell
    {
        private readonly IFaceDetectionService _faceDetectionService;
        private readonly IFaceRecogntionService _faceRecognionService;
        private readonly IMainPageViewModel _mainPageViewModel;
        private readonly IDatabaseService _databaseService;
        private readonly IFileDirectoryService _directoryService;
        
        public virtual Views.MainWindow Window { get; set; }
        
        public void Run()
        {
            Window.Show();
        }

        public Shell(IFaceDetectionService faceDetectionService,
            IFaceRecogntionService faceRecognitionService,
            IMainPageViewModel mainPageViewModel,
            IDatabaseService databaseService,
            IFileDirectoryService directoryService)
        {
            _faceDetectionService = faceDetectionService;
            _faceRecognionService = faceRecognitionService;
            _mainPageViewModel = mainPageViewModel;
            _databaseService = databaseService;
            _directoryService = directoryService;

            InitializeServices();
        }

        private void InitializeServices()
        {
            _directoryService.CreateDirectory(RecognizerConfig.FaceDetRecPath);

            _faceRecognionService.SetRecognizer();
            _faceRecognionService.TrainRecognizer(_mainPageViewModel.ViewModel.Parameters.RecognizerIndex);
            
            _faceDetectionService.ImageWithDetectionChanged
                += _faceDetectionService_ImageChanged;
            _faceDetectionService.FaceDetected += _faceDetectionService_FaceDetected;
        }

        private void _faceDetectionService_FaceDetected(object sender, Image<Bgr, byte> image, Bitmap bitmap)
        {
            _mainPageViewModel.ViewModel.MainControls.TheNewestFace = bitmap;
            _mainPageViewModel.ViewModel.MainControls.StatusText = StatusTypes.FaceDetected.GetDesciption();
            _mainPageViewModel.ViewModel.MainControls.IsNewUpdate = true;

            var id = _faceRecognionService.RecognizePerson(image.Convert<Gray, byte>(), 
                _mainPageViewModel.ViewModel.Parameters.RecognizerIndex);
            var person = _databaseService.GetPersonalData(id);

            _mainPageViewModel.ViewModel.MainControls.DetectedId = id;
            _mainPageViewModel.ViewModel.MainControls.DetectedName = 
                person != null ? person.Name : "Brak informacji";
            _mainPageViewModel.ViewModel.MainControls.DetectedAge = 
                person != null ? person.Age.ToString() : "";
            _mainPageViewModel.ViewModel.MainControls.DetectedAddress =
                person != null ? person.Address : "";
            _mainPageViewModel.ViewModel.MainControls.DetectedDetails =
                person != null ? person.Details : "";

            StatusBarUtility.ResestStatus(_mainPageViewModel.ViewModel.MainControls);
        }

        private void _faceDetectionService_ImageChanged(object sender, Image<Bgr, byte> image)
        {
            try
            {
                if (_mainPageViewModel.ViewModel.MainControls.TheFrame != null
                    && image.Height > 0 && image.Width > 0)
                {
                    _mainPageViewModel.ViewModel.MainControls.TheFrame = image.Bitmap;
                }          
            }
            catch (Exception)
            {
                Debug.WriteLine("Image changed issue");
            }
        }
    }
}

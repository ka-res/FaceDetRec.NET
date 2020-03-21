using FaceDetRec.WPFClient.Services.Interfaces;
using System.Windows.Input;

namespace FaceDetRec.WPFClient.ViewModels.MainWindow
{
    public class Methods : IMethods
    {
        private readonly IFaceDetectionService _faceCameraService;
        private readonly IFaceRecogntionService _faceRecogntionService;

        public ICommand ToggleCameraService { get; }
        public ICommand ChangeDevice { get; }
        public ICommand SetRecognizer { get; }
        public ICommand SaveDetected { get; }
        public ICommand LoadPersonalData { get; set; }
        public ICommand ShouldDetectEyes { get; set; }
        public ICommand ShouldAlwaysRetrain { get; set; }
        public ICommand RetrainRecognizer { get; set; }
        public ICommand ChangeDataMode { get; set; }

        public Methods(IFaceDetectionService faceCameraService,
            IFaceRecogntionService faceRecogntionService)
        {
            _faceCameraService = faceCameraService;
            _faceRecogntionService = faceRecogntionService;

            ToggleCameraService = new ModelCommand(
                _faceCameraService.ToogleCameraService);

            ChangeDevice = new ModelCommand(
                _faceCameraService.ChangeDevice);

            SetRecognizer = new ModelCommand(
                _faceRecogntionService.SetRecognizer);

            SaveDetected = new ModelCommand(
                _faceCameraService.SaveDetected);

            LoadPersonalData = new ModelCommand(
                _faceCameraService.LoadPersonalData);

            ShouldDetectEyes = new ModelCommand(
                _faceCameraService.ShouldDetectEyes);

            ShouldAlwaysRetrain = new ModelCommand(
                _faceRecogntionService.ShouldAlwaysRetrain);

            RetrainRecognizer = new ModelCommand(
                _faceRecogntionService.RetrainRecognizer);

            ChangeDataMode = new ModelCommand(
                _faceCameraService.ChangeDataMode);
        }
    }
}

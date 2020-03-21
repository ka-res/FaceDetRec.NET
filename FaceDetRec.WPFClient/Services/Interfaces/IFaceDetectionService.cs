using FaceDetRec.WPFClient.Services.Implementations;

namespace FaceDetRec.WPFClient.Services.Interfaces
{
    public interface IFaceDetectionService : ICameraCaptureService
    {
        void ToogleCameraService();
        void ChangeDevice();
        void SaveDetected();

        event FaceDetectionService.ImageWithDetectionChangedEventHandler ImageWithDetectionChanged;
        event FaceDetectionService.FaceDetectedEventHandler FaceDetected;
        
        void LoadPersonalData();
        void ShouldDetectEyes();
        void ChangeDataMode();
    }
}

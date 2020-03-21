namespace FaceDetRec.WPFClient.Services.Interfaces
{
    public interface ICameraCaptureService
    {
        bool IsStarted();
        void StartCapturing(int deviceIndex);
        void StopCapturing();
        void StartServiceAsync();
        void StopServiceAsync();
    }
}

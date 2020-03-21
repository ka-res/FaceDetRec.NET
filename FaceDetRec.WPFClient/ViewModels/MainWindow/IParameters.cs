namespace FaceDetRec.WPFClient.ViewModels.MainWindow
{
    public interface IParameters
    {
        int DeviceIndex { get; set; }
        int RecognizerIndex { get; set; }
        bool UseExplorer { get; set; }
    }
}

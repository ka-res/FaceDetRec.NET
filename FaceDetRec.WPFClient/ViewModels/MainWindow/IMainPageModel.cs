namespace FaceDetRec.WPFClient.ViewModels.MainWindow
{
    public interface IMainPageModel
    {
        IControls MainControls { get; set; }
        IMethods Methods { get; set; }
        IParameters Parameters { get; set; }
    }
}

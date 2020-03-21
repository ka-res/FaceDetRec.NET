namespace FaceDetRec.WPFClient.ViewModels.MainWindow
{
    public class MainPageModel : IMainPageModel
    {
        public IControls MainControls { get; set; }
        public IMethods Methods { get; set; }
        public IParameters Parameters { get; set; }
    }
}

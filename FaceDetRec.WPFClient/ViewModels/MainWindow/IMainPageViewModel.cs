namespace FaceDetRec.WPFClient.ViewModels.MainWindow
{
    public interface IMainPageViewModel
    {
        IMainPageModel ViewModel { get; set; }

        IMainPageModel Get();
    }
}

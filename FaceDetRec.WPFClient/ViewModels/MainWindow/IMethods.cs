using System.Windows.Input;

namespace FaceDetRec.WPFClient.ViewModels.MainWindow
{
    public interface IMethods
    {
        ICommand ToggleCameraService { get; }
        ICommand ChangeDevice { get; }
        ICommand SetRecognizer { get; }
        ICommand SaveDetected { get; }
        ICommand LoadPersonalData { get; set; }
        ICommand ShouldDetectEyes { get; set; }
        ICommand ShouldAlwaysRetrain { get; set; }
        ICommand RetrainRecognizer { get; set; }
        ICommand ChangeDataMode { get; set; }
    }
}

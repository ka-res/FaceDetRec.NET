using System.ComponentModel;

namespace FaceDetRec.WPFClient.ViewModels.MainWindow
{
    public class Parameters : IParameters, INotifyPropertyChanged
    {
        public int DeviceIndex { get; set; }
        public int RecognizerIndex { get; set; }
        public bool UseExplorer { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

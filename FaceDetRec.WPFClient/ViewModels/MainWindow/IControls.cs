using System.Collections.ObjectModel;
using System.Drawing;
using FaceDetRec.WPFClient.DataModels;

namespace FaceDetRec.WPFClient.ViewModels.MainWindow
{
    public interface IControls
    {
        ObservableCollection<CameraInputModel> AvailableVideoInputs { get; set; }
        CameraInputModel SelectedVideoInput { get; set; }
        RecognizerModel SelectedRecognizer { get; set; }
        bool IsDeviceChangeEnabled { get; set; }
        string ServiceButtonText { get; set; }

        Bitmap TheNewestFace { get; set; }
        ObservableCollection<string> KnownPeople { get; set; }
        ObservableCollection<RecognizerModel> Recognizers { get; set; }
        string Name { get; set; }
        string Age { get; set; }
        string Address { get; set; }
        string Details { get; set; }
        bool IsSaveEnabled { get; set; }

        string StatusText { get; set; }
        bool IsNewUpdate { get; set; }

        Bitmap TheFrame { get; set; }

        bool AreEyesDetected { get; set; }
        bool IsRetrainRequired { get; set; }
        int? DetectedId { get; set; }
        string DetectedName { get; set; }
        string DetectedAge { get; set; }
        string DetectedAddress { get; set; }
        string DetectedDetails { get; set; }
        bool IsAnyFaceMarked { get; set; }

        bool IsExplorerPreffered { get; set; }
    }
}

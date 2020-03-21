using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using FaceDetRec.WPFClient.DataModels;

namespace FaceDetRec.WPFClient.ViewModels.MainWindow
{
    public class Controls : IControls, INotifyPropertyChanged
    {
        private Bitmap _theFrame;
        private Bitmap _theNewestFace;
        private bool _isDeviceChangeEnabled;
        private string _serviceButtonText;
        private string _statusText;
        private bool _isNewUpdate;
        private string _name;
        private string _age;
        private string _address;
        private string _details;
        private bool _isSaveEnabled;
        private bool _areEyesDetected;
        private bool _isRetrainRequired;
        private int? _detectedId;
        private string _detectedName;
        private string _detectedAge;
        private string _detectedAddress;
        private string _detectedDetails;
        private bool _isAnyFaceMarked;
        private bool _isExplorerPreffered;
        private ObservableCollection<string> _knownPeople;

        public ObservableCollection<CameraInputModel> AvailableVideoInputs { get; set; }
        public ObservableCollection<RecognizerModel> Recognizers { get; set; }
        public CameraInputModel SelectedVideoInput { get; set; }
        public RecognizerModel SelectedRecognizer { get; set; }

        public bool IsExplorerPreffered
        {
            get => _isExplorerPreffered;
            set
            {
                _isExplorerPreffered = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(IsExplorerPreffered)));
            }
        }

        public ObservableCollection<string> KnownPeople
        {
            get => _knownPeople;
            set
            {
                _knownPeople = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(KnownPeople)));
            }
        }

        public string ServiceButtonText
        {
            get => _serviceButtonText;
            set
            {
                _serviceButtonText = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(ServiceButtonText)));
            }
        }
        
        public string StatusText
        {
            get => _statusText;
            set
            {
                _statusText = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(StatusText)));
            }
        }

        public bool IsNewUpdate
        {
            get => _isNewUpdate;
            set
            {
                _isNewUpdate = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(IsNewUpdate)));
            }
        }

        public Bitmap TheFrame
        {
            get => _theFrame;
            set
            {
                _theFrame = value;
                PropertyChanged?.Invoke(this, 
                    new PropertyChangedEventArgs(nameof(TheFrame)));
            }
        }

        public Bitmap TheNewestFace
        {
            get => _theNewestFace;
            set
            {
                _theNewestFace = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(TheNewestFace)));
            }
        }

        public bool IsDeviceChangeEnabled
        {
            get => _isDeviceChangeEnabled;
            set
            {
                _isDeviceChangeEnabled = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(IsDeviceChangeEnabled)));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Age)));
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Address)));
            }
        }

        public string Details
        {
            get => _details;
            set
            {
                _details = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Details)));
            }
        }

        public bool IsSaveEnabled
        {
            get => _isSaveEnabled;
            set
            {
                _isSaveEnabled = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(IsSaveEnabled)));
            }
        }

        public bool AreEyesDetected
        {
            get => _areEyesDetected;
            set
            {
                _areEyesDetected = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(AreEyesDetected)));
            }
        }

        public bool IsRetrainRequired
        {
            get => _isRetrainRequired;
            set
            {
                _isRetrainRequired = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(IsRetrainRequired)));
            }
        }

        public int? DetectedId
        {
            get => _detectedId;
            set
            {
                _detectedId = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(DetectedId)));
            }
        }

        public string DetectedName
        {
            get => _detectedName;
            set
            {
                _detectedName = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(DetectedName)));
            }
        }

        public string DetectedAge
        {
            get => _detectedAge;
            set
            {
                _detectedAge = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(DetectedAge)));
            }
        }

        public string DetectedAddress
        {
            get => _detectedAddress;
            set
            {
                _detectedAddress = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(DetectedAddress)));
            }
        }

        public string DetectedDetails
        {
            get => _detectedDetails;
            set
            {
                _detectedDetails = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(DetectedDetails)));
            }
        }

        public bool IsAnyFaceMarked
        {
            get => _isAnyFaceMarked;
            set
            {
                _isAnyFaceMarked = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(IsAnyFaceMarked)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

using FaceDetRec.WPFClient.Common.Enums;
using FaceDetRec.WPFClient.Services.Interfaces;
using FaceDetRec.WPFClient.Utils;

namespace FaceDetRec.WPFClient.ViewModels.MainWindow
{
    public class MainPageViewModel : IMainPageViewModel
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IDatabaseService _databaseService;

        public MainPageViewModel(ILocalStorageService localStorageService,
            IDatabaseService databaseService)
        {
            _localStorageService = localStorageService;
            _databaseService = databaseService;
        }

        public IMainPageModel ViewModel { get; set; }

        public IMainPageModel Get()
        {
            ViewModel.MainControls.AvailableVideoInputs = VideoHardwareUtility.GetCamerasList();
            ViewModel.MainControls.Recognizers = RecognizersUtility.GetRecognizers();
            ViewModel.MainControls.KnownPeople = _databaseService.GetPeopleNames();
            ViewModel.MainControls.SelectedVideoInput = ViewModel.MainControls.AvailableVideoInputs.SetDefaultDevice();
            ViewModel.MainControls.SelectedRecognizer = ViewModel.MainControls.Recognizers.SetDefaultRecognizer();
            ViewModel.MainControls.ServiceButtonText = ButtonText.Retrieve.GetDesciption();
            ViewModel.MainControls.IsDeviceChangeEnabled = true;
            ViewModel.MainControls.IsSaveEnabled = false;
            ViewModel.MainControls.IsExplorerPreffered = false;

            return ViewModel;
        }
    }
}

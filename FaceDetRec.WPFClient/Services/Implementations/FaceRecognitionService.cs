using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using FaceDetRec.WPFClient.Common.Enums;
using FaceDetRec.WPFClient.Config;
using FaceDetRec.WPFClient.DataModels;
using FaceDetRec.WPFClient.Services.Interfaces;
using FaceDetRec.WPFClient.Utils;
using FaceDetRec.WPFClient.ViewModels.MainWindow;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace FaceDetRec.WPFClient.Services.Implementations
{
    public class FaceRecognitionService : IFaceRecogntionService
    {
        private readonly IControls _controls;
        private readonly IParameters _parameters;
        private readonly IDatabaseService _databaseService;
        private readonly ILocalStorageService _localStorageService;
        private readonly IFileDirectoryService _directoryService;
        
        public FaceRecognizer FaceRecognizer;

        public FaceRecognitionService(IDatabaseService databaseService,
            IControls controls, IParameters parameters, 
            IFileDirectoryService directoryService, ILocalStorageService localStorageService)
        {
            _databaseService = databaseService;
            _parameters = parameters;
            _controls = controls;
            _directoryService = directoryService;
            _localStorageService = localStorageService;
        }

       public bool TrainRecognizer(int recognizerIndex)
        {
            var allFaces = new List<ImageWithLabelModel>();

            allFaces = _parameters.UseExplorer 
                ? _localStorageService.GetAllImagesWithLabels() 
                : _databaseService.GetAllImagesWithLabels();

            var count = allFaces.Count();

            if (count <= 0) return true;

            var faceImages = new Image<Gray, byte>[count];
            var faceLabels = new int[count];

            for (var i = 0; i < count; i++)
            {
                var stream = new MemoryStream();
                stream.Write(allFaces[i].Data, 0, allFaces[i].Data.Length);

                var faceImage = new Image<Gray, byte>(new Bitmap(stream));
                faceImages[i] = faceImage.Resize(100, 100, Inter.Cubic);

                faceLabels[i] = allFaces[i].PersonId;
            }

            FaceRecognizer.Train(faceImages, faceLabels);
            switch (recognizerIndex)
            {
                case 0:
                    FaceRecognizer.Write(
                        _parameters.UseExplorer
                        ? RecognizerConfig.EigenLocalConfigurationPath
                        : RecognizerConfig.EigenDatabaseConfigurationPath);
                    break;
                case 1:
                    FaceRecognizer.Write(
                        _parameters.UseExplorer
                        ? RecognizerConfig.FisherLocalConfigurationPath
                        : RecognizerConfig.FisherDatabaseConfigurationPath);
                    break;
                case 2:
                    FaceRecognizer.Write(
                        _parameters.UseExplorer
                        ? RecognizerConfig.LbphLocalConfigurationPath
                        : RecognizerConfig.LbphDatabaseConfigurationPath);
                    break;
            }

            return true;
        }

        public int RecognizePerson(Image<Gray, byte> userImage, int recognizerIndex)
        {
            try
            {
                switch (_parameters.RecognizerIndex)
                {
                    case 0:
                        FaceRecognizer.Read(
                            _parameters.UseExplorer
                            ? RecognizerConfig.EigenLocalConfigurationPath
                            : RecognizerConfig.EigenDatabaseConfigurationPath);
                        break;
                    case 1:
                        FaceRecognizer.Read(
                            _parameters.UseExplorer
                            ? RecognizerConfig.FisherLocalConfigurationPath
                            : RecognizerConfig.FisherDatabaseConfigurationPath);
                        break;
                    case 2:
                        FaceRecognizer.Read(
                            _parameters.UseExplorer
                            ? RecognizerConfig.LbphLocalConfigurationPath
                            : RecognizerConfig.LbphDatabaseConfigurationPath);
                        break;
                }

                var resizedImage = userImage.Resize(100, 100, Inter.Cubic);
                var result = FaceRecognizer.Predict(resizedImage);

                return result.Label;
            }
            catch (System.Exception)
            {
                return -1;
            }            
        }

        public void RetrainRecognizer()
        {
            TrainRecognizer(_parameters.RecognizerIndex);

            _controls.StatusText = StatusTypes.RetrainCompleted.GetDesciption();
            _controls.IsNewUpdate = true;
            
            StatusBarUtility.ResestStatus(_controls);
        }

        public void SetRecognizer()
        {
            _parameters.RecognizerIndex = _controls.SelectedRecognizer?.Index ?? 0;

            switch (_parameters.RecognizerIndex)
            {
                case 0:
                    FaceRecognizer = new EigenFaceRecognizer();
                    if (!_directoryService.CheckIfConfigFileExists(0) || _controls.IsRetrainRequired)
                    {
                        TrainRecognizer(_parameters.RecognizerIndex);
                    }
                    break;
                case 1:
                    FaceRecognizer = new FisherFaceRecognizer();
                    if (!_directoryService.CheckIfConfigFileExists(1) || _controls.IsRetrainRequired)
                    {
                        TrainRecognizer(_parameters.RecognizerIndex);
                    }
                    break;
                case 2:
                    FaceRecognizer = new LBPHFaceRecognizer();
                    if (!_directoryService.CheckIfConfigFileExists(2) || _controls.IsRetrainRequired)
                    {
                        TrainRecognizer(_parameters.RecognizerIndex);
                    }
                    break;
            }
        }

        public void ShouldAlwaysRetrain()
        {
            _controls.IsRetrainRequired = !_controls.IsRetrainRequired;
        }
    }
}

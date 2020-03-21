using FaceDetRec.WPFClient.Config;
using FaceDetRec.WPFClient.Services.Interfaces;
using System.IO;
using FaceDetRec.WPFClient.ViewModels.MainWindow;

namespace FaceDetRec.WPFClient.Services.Implementations
{
    public class FileDirectoryService : IFileDirectoryService
    {
        private readonly IParameters _parameters;

        public FileDirectoryService(IParameters parameters)
        {
            _parameters = parameters;
        }

        public bool CheckIfDirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public void CreateDirectory(string path)
        {
            if (!CheckIfDirectoryExists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public bool CheckIfConfigFileExists(int recognizerIndex)
        {
            switch (recognizerIndex)
            {
                case 0:
                    return CheckIfFileExists(
                        _parameters.UseExplorer 
                        ? RecognizerConfig.EigenLocalConfigurationPath
                        : RecognizerConfig.EigenDatabaseConfigurationPath);
                case 1:
                    return CheckIfFileExists(
                        _parameters.UseExplorer
                        ? RecognizerConfig.FisherLocalConfigurationPath
                        : RecognizerConfig.FisherDatabaseConfigurationPath);
                case 2:
                    return CheckIfFileExists(
                        _parameters.UseExplorer
                        ? RecognizerConfig.LbphLocalConfigurationPath
                        : RecognizerConfig.LbphDatabaseConfigurationPath);
            }

            return false;
        }

        public bool CheckIfFileExists(string path)
        {
            return File.Exists(path);
        }

        public void CreateFile(string path)
        {
            if (!CheckIfFileExists(path))
            {
                File.Create(path);
            }
        }

        public bool CheckIfLocalStorageStructuresArePresent()
        {
            return CheckIfDirectoryExists(RecognizerConfig.LocalStoragePath);
        }

        public void CreateLocalStorageStructures()
        {
            CreateDirectory(RecognizerConfig.LocalStoragePath);
            CreateDirectory(RecognizerConfig.PeoplePath);
            CreateDirectory(RecognizerConfig.ImagesPath);
            CreateFile(RecognizerConfig.PeopleFilePath);
        }
    }
}

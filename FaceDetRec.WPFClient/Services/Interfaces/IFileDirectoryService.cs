namespace FaceDetRec.WPFClient.Services.Interfaces
{
    public interface IFileDirectoryService
    {
        bool CheckIfDirectoryExists(string path);
        void CreateDirectory(string path);
        bool CheckIfConfigFileExists(int recognizerIndex);
        bool CheckIfFileExists(string path);
        void CreateFile(string path);
        bool CheckIfLocalStorageStructuresArePresent();
        void CreateLocalStorageStructures();
    }
}

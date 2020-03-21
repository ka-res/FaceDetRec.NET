using System.IO;

namespace FaceDetRec.WPFClient.Repositories.Interfaces.LocalStorage
{
    public interface IBaseRepositoryLs
    {
        StreamReader OpenStream(string filepath);
    }
}

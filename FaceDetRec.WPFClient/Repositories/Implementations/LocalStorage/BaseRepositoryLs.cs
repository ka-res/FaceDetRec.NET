using System.IO;
using FaceDetRec.WPFClient.Repositories.Interfaces.LocalStorage;

namespace FaceDetRec.WPFClient.Repositories.Implementations.LocalStorage
{
    public class BaseRepositoryLs : IBaseRepositoryLs
    {
        public StreamReader OpenStream(string filepath)
        {
            throw new System.NotImplementedException();
        }
    }
}

using FaceDetRec.WPFClient.DataBase;
using FaceDetRec.WPFClient.Repositories.Interfaces.DataBase;

namespace FaceDetRec.WPFClient.Repositories.Implementations.DataBase
{
    public class BaseRepositoryDb : IBaseRepositoryDb
    {
        protected readonly FaceDetRecDB Context;

        public BaseRepositoryDb(FaceDetRecDB context)
        {
            Context = context;
        }
    }
}
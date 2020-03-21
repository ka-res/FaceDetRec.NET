using System.Collections.Generic;
using FaceDetRec.WPFClient.DataModels;

namespace FaceDetRec.WPFClient.Repositories.Interfaces.LocalStorage
{
    public interface IImageRepositoryLs
    {
        ImageModelBase GetImage(int id);
        ICollection<ImageModelBase> GetImages();
        void SaveImage(ImageModelBase image, int personId);
    }
}

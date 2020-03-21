using System.Linq;
using FaceDetRec.WPFClient.DataBase;
using FaceDetRec.WPFClient.DataModels;

namespace FaceDetRec.WPFClient.Repositories.Interfaces.DataBase
{
    public interface IImageRepositoryDb
    {
        Image GetImage(int id);
        Image AddImage(ImageModelBase image, int personId);
        void EditImage(int id, ImageModel image);
        void RemoveImage(int id);
        IQueryable<Image> GetImagesByName(string name);
        IQueryable<Image> GetImages();
    }
}

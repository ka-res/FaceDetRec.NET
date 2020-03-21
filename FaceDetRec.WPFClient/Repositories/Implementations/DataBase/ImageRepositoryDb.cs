using System;
using System.Linq;
using FaceDetRec.WPFClient.DataBase;
using FaceDetRec.WPFClient.DataModels;
using FaceDetRec.WPFClient.Repositories.Interfaces.DataBase;

namespace FaceDetRec.WPFClient.Repositories.Implementations.DataBase
{
    public class ImageRepositoryDb : BaseRepositoryDb, IImageRepositoryDb
    {
        public ImageRepositoryDb(FaceDetRecDB context) 
            : base(context)
        {
        }

        public Image GetImage(int id)
        {
            return Context.Images.SingleOrDefault(x => x.Id == id);
        }

        public Image AddImage(ImageModelBase image, int personId)
        {
            image.PersonId = personId;
            var newImage = Context.Images.Add(new Image
            {
                Id = image.Id,
                Data = image.Data,
                PersonId = image.PersonId,
                AddDateTime = DateTime.Now
            });

            Context.SaveChanges();

            return newImage;
        }

        public void EditImage(int id, ImageModel image)
        {
            var current = Context.Images.Find(id);
            Context.Entry(current).CurrentValues.SetValues(image);

            Context.SaveChanges();
        }

        public void RemoveImage(int id)
        {
            var current = Context.Images.Find(id);
            if (current != null)
                Context.Images.Remove(current);

            Context.SaveChanges();
        }

        public IQueryable<Image> GetImagesByName(string name)
        {
            var personId = Context.People.SingleOrDefault(x => x.Name == name)?.Id;
            var images = Context.Images.Where(x => x.PersonId == personId);

            return images;
        }

        public IQueryable<Image> GetImages()
        {
            return Context.Images;
        }
    }
}

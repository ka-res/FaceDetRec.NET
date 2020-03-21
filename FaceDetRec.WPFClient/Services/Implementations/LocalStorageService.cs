using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FaceDetRec.WPFClient.DataModels;
using FaceDetRec.WPFClient.Repositories.Interfaces.LocalStorage;
using FaceDetRec.WPFClient.Services.Interfaces;

namespace FaceDetRec.WPFClient.Services.Implementations
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IImageRepositoryLs _imageRepositoryLs;
        private readonly IPersonRepositoryLs _personRepositoryLs;

        public LocalStorageService(
            IImageRepositoryLs imageRepositoryLs, 
            IPersonRepositoryLs personRepositoryLs)
        {
            _imageRepositoryLs = imageRepositoryLs;
            _personRepositoryLs = personRepositoryLs;
        }

        public ICollection<PersonModelBase> GetPeople()
        {
            throw new System.NotImplementedException();
        }

        public ICollection<ImageModelBase> GetImages()
        {
            throw new System.NotImplementedException();
        }

        public PersonModelBase GetPerson(string name)
        {
            return _personRepositoryLs.GetPerson(name);
        }

        public void SaveFace(PersonModelBase person, ImageModelBase image)
        {
            _personRepositoryLs.SavePerson(person, out var id);
            _imageRepositoryLs.SaveImage(image, id);
        }

        public List<ImageWithLabelModel> GetAllImagesWithLabels()
        {
            var images = _imageRepositoryLs.GetImages();
            var imagesWithLabels = new List<ImageWithLabelModel>();

            foreach (var image in images)
            {
                imagesWithLabels = images.Select(x => new ImageWithLabelModel
                {
                    Data = image.Data,
                    PersonId = image.PersonId
                }).ToList();
            }

            return imagesWithLabels;
        }

        public ObservableCollection<string> GetPeopleNames()
        {
            var result = new ObservableCollection<string>(_personRepositoryLs
                .GetPeopleNames().ToList());

            return result;
        }
    }
}

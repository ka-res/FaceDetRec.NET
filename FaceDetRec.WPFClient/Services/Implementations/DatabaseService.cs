using FaceDetRec.WPFClient.DataBase;
using FaceDetRec.WPFClient.DataModels;
using FaceDetRec.WPFClient.Services.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FaceDetRec.WPFClient.Repositories.Interfaces.DataBase;

namespace FaceDetRec.WPFClient.Services.Implementations
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IPersonRepositoryDb _personRepository;
        private readonly IImageRepositoryDb _imageRepository;

        public DatabaseService(
            IPersonRepositoryDb personRepository,
            IImageRepositoryDb imageRepository)
        {
            _personRepository = personRepository;
            _imageRepository = imageRepository;
        }

        public void SaveFace(PersonModelBase person, ImageModelBase image)
        {
            var possiblePerson = _personRepository.GetPerson(person.Name);
            int personId;

            if (possiblePerson != null)
            {
                personId = possiblePerson.Id;
                person.Id = personId;
                _personRepository.EditPerson(possiblePerson.Id, new PersonModel(person));
            }
            else
            {
                var newPerson = _personRepository.AddPerson(person);
                personId = newPerson.Id;
            }
            
            _imageRepository.AddImage(image, personId);
        }

        public ObservableCollection<string> GetPeopleNames()
        {
            var result = new ObservableCollection<string>(_personRepository
                .GetPeopleNames().ToList());

            return result;
        }

        public Person GetPersonalData(string name)
        {
            return _personRepository.GetPerson(name);
        }

        public Person GetPersonalData(int id)
        {
            return _personRepository.GetPerson(id);
        }

        public List<ImageWithLabelModel> GetAllImagesWithLabels()
        {
            var images = _imageRepository.GetImages();
            var imagesWithLabels = new List<ImageWithLabelModel>();

            foreach (var image in images)
            {
                imagesWithLabels.Add(new ImageWithLabelModel
                {
                    Data = image.Data,
                    PersonId = image.PersonId
                });
            }

            return imagesWithLabels;
        }

        public string GetPersonsName(int? id)
        {
            return id != null 
                ? _personRepository.GetPerson(id.Value).Name 
                : null;
        }
    }
}

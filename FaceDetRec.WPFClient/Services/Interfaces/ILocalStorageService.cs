using System.Collections.Generic;
using System.Collections.ObjectModel;
using FaceDetRec.WPFClient.DataModels;

namespace FaceDetRec.WPFClient.Services.Interfaces
{
    public interface ILocalStorageService
    {
        ICollection<PersonModelBase> GetPeople();
        ICollection<ImageModelBase> GetImages();
        PersonModelBase GetPerson(string name);
        void SaveFace(PersonModelBase person, ImageModelBase image);
        List<ImageWithLabelModel> GetAllImagesWithLabels();
        ObservableCollection<string> GetPeopleNames();
    }
}

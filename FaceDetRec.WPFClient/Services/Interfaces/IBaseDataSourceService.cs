using FaceDetRec.WPFClient.DataBase;
using FaceDetRec.WPFClient.DataModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FaceDetRec.WPFClient.Services.Interfaces
{
    public interface IBaseDataSourceService
    {
        void SaveFace(PersonModelBase person, ImageModelBase image);
        ObservableCollection<string> GetPeopleNames();
        Person GetPersonalData(string name);
        Person GetPersonalData(int id);
        List<ImageWithLabelModel> GetAllImagesWithLabels();
        string GetPersonsName(int? id);
    }
}

using System.Collections.Generic;
using System.Linq;
using FaceDetRec.WPFClient.DataModels;

namespace FaceDetRec.WPFClient.Repositories.Interfaces.LocalStorage
{
    public interface IPersonRepositoryLs
    {
        PersonModelBase GetPerson(int id);
        PersonModelBase GetPerson(string name);
        ICollection<PersonModelBase> GetPeople();
        void SavePerson(PersonModelBase person, out int id);
        bool CheckIfPersonExists(PersonModelBase person);
        IEnumerable<string> GetPeopleNames();
    }
}

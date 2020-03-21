using System.Linq;
using FaceDetRec.WPFClient.DataBase;
using FaceDetRec.WPFClient.DataModels;

namespace FaceDetRec.WPFClient.Repositories.Interfaces.DataBase
{
    public interface IPersonRepositoryDb
    {
        Person GetPerson(int id);
        Person GetPerson(string name);
        Person AddPerson(PersonModelBase person);
        void EditPerson(int id, PersonModel person);
        void RemovePerson(int id);
        IQueryable<Person> GetPeople();
        IQueryable<string> GetPeopleNames();
    }
}

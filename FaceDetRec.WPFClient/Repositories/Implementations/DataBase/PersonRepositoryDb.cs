using System;
using System.Linq;
using FaceDetRec.WPFClient.DataBase;
using FaceDetRec.WPFClient.DataModels;
using FaceDetRec.WPFClient.Repositories.Interfaces;
using FaceDetRec.WPFClient.Repositories.Interfaces.DataBase;

namespace FaceDetRec.WPFClient.Repositories.Implementations.DataBase
{
    public class PersonRepositoryDb : BaseRepositoryDb, IPersonRepositoryDb
    {
        public PersonRepositoryDb(FaceDetRecDB context)
            : base(context)
        {
        }

        public IQueryable<string> GetPeopleNames()
        {
            return GetPeople().Distinct().Select(x => x.Name);
        }

        public IQueryable<Person> GetPeople()
        {
            return Context.People;
        }

        public Person GetPerson(int id)
        {
            return Context.People.SingleOrDefault(x => x.Id == id);
        }

        public Person GetPerson(string name)
        {
            return Context.People.SingleOrDefault(x => x.Name == name);
        }

        public Person AddPerson(PersonModelBase person)
        {
            var newPerson = Context.People.Add(new Person
            {
                Id = person.Id,
                Name = person.Name,
                Age = person.Age,
                Address = person.Address,
                Details = person.Details,
                AddDateTime = DateTime.Now
            });

            Context.SaveChanges();

            return newPerson;
        }

        public void EditPerson(int id, PersonModel person)
        {
            var current = Context.People.Find(id);
            person.AddDateTime = DateTime.Now;
            Context.Entry(current).CurrentValues.SetValues(person);

            Context.SaveChanges();
        }

        public void RemovePerson(int id)
        {
            var current = Context.People.Find(id);
            if (current != null)
                Context.People.Remove(current);

            Context.SaveChanges();
        }
    }
}

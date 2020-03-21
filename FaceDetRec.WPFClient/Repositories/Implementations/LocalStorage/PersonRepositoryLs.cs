using System;
using FaceDetRec.WPFClient.Config;
using FaceDetRec.WPFClient.DataModels;
using FaceDetRec.WPFClient.Repositories.Interfaces.LocalStorage;
using FaceDetRec.WPFClient.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FaceDetRec.WPFClient.Repositories.Implementations.LocalStorage
{
    public class PersonRepositoryLs : IPersonRepositoryLs
    {
        public PersonModelBase GetPerson(int id)
        {
            var line = File.ReadLines(RecognizerConfig.PeopleFilePath)
                .Skip(id - 1).Take(1).First();
            var personInfo = line.Split(new string[1] {"<_>"}, StringSplitOptions.None);

            return new PersonModelBase
            {
                Id = Convert.ToInt32(personInfo[0]),
                Name = personInfo[1],
                Age = Convert.ToInt32(personInfo[2]),
                Address = personInfo[3],
                Details = personInfo[4]
            };
        }

        public ICollection<PersonModelBase> GetPeople()
        {
            var lines = File.ReadAllLines(RecognizerConfig.PeopleFilePath);
            var people = new List<PersonModelBase>();

            for (var i = 1; i <= lines.Length; i++)
            {
                people.Add(GetPerson(i));
            }

            return people;
        }

        public PersonModelBase GetPerson(string name)
        {
            return GetPeople().FirstOrDefault(x => x.Name == name);
        }

        public void SavePerson(PersonModelBase person, out int id)
        {
            if (CheckIfPersonExists(person))
            {
                id = GetPerson(person.Name).Id;
            }
            else
            {
                id = SetNewId();

                var fileStream = new FileStream(RecognizerConfig.PeopleFilePath, FileMode.Append);
                var streamWriter = new StreamWriter(fileStream);

                streamWriter.WriteLine(LocalStorageUtility.PrepareLocalStoragePersonInfo(person, id));

                streamWriter.Close();
                fileStream.Close();
            }
        }

        public bool CheckIfPersonExists(PersonModelBase person)
        {
            return GetPeople().Any(x => x.Name == person.Name);
        }

        public IEnumerable<string> GetPeopleNames()
        {
            return GetPeople().Distinct().Select(x => x.Name);
        }

        private static int SetNewId()
        {
            var fileStream = new FileStream(RecognizerConfig.PeopleFilePath, FileMode.OpenOrCreate);
            var streamReader = new StreamReader(fileStream);
            var count = 0;

            while (streamReader.ReadLine() != null)
            {
                count++;
            }

            streamReader.Close();
            fileStream.Close();

            return count + 1;
        }
    }
}

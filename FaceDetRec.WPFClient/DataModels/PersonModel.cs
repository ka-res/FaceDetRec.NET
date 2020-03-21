using System;

namespace FaceDetRec.WPFClient.DataModels
{
    public class PersonModelBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public string Details { get; set; }
    }

    public class PersonModel : PersonModelBase
    {
        public DateTime? AddDateTime { get;set; }

        public PersonModel(PersonModelBase person)
        {
            Id = person.Id;
            Name = person.Name;
            Age = person.Age;
            Address = person.Address;
            Details = person.Details;
            AddDateTime = DateTime.Now;
        }
    }
}

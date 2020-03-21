using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using FaceDetRec.WPFClient.DataBase;

namespace FaceDetRec.WPFClient.DataModels
{
    public class ImageModelBase
    {
        public int Id { get; set; }

        public byte[] Data { get; set; }

        [ForeignKey(nameof(People))]
        public int PersonId { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }

    public class ImageModel : ImageModelBase
    {
        public DateTime? AddDateTime { get; set; }

        public ImageModel(ImageModelBase image)
        {
            Id = image.Id;
            Data = image.Data;
            PersonId = image.PersonId;
            AddDateTime = DateTime.Now;
        }
    }
}

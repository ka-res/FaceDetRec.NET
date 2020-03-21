using System;
using FaceDetRec.WPFClient.DataModels;

namespace FaceDetRec.WPFClient.Utils
{
    public static class LocalStorageUtility
    {
        public static string PrepareImageName(int imageId, int personId)
        {
            return $"i{imageId}_p{personId}";
        }

        public static Tuple<int, int> GetInfoFromImageName(string imageName)
        {
            var stringInfo = imageName.Split('_');
            var imageId = Convert.ToInt32(stringInfo[0].Replace("i", ""));
            var personId = Convert.ToInt32(stringInfo[1].Replace("p", ""));

            return new Tuple<int, int>(imageId, personId);
        }

        public static string PrepareLocalStoragePersonInfo(PersonModelBase person, int id)
        {
            person.Id = id;

            return $"{person.Id}<_>{person.Name}<_>{person.Age}<_>{person.Address}<_>{person.Details}";
        }
    }
}

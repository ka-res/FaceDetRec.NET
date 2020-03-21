using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using FaceDetRec.WPFClient.Config;
using FaceDetRec.WPFClient.DataModels;
using FaceDetRec.WPFClient.Repositories.Interfaces.LocalStorage;
using FaceDetRec.WPFClient.Utils;

namespace FaceDetRec.WPFClient.Repositories.Implementations.LocalStorage
{
    public class ImageRepositoryLs : IImageRepositoryLs
    {
        public ImageModelBase GetImage(int id)
        {
            var files = Directory.GetFiles(RecognizerConfig.ImagesPath);
            var imageName = files.SingleOrDefault(x => x.Contains($"i{id}_"));
            var imageNameTmp = imageName.Replace($"i{id}_p", "")
                .Replace(RecognizerConfig.DefaultImageExtension, "")
                .Replace(RecognizerConfig.ImagesPath, "");
            var resultString = Regex.Match(imageNameTmp, @"\d+").Value;
            var personId = Convert.ToInt32(resultString);
            var data = File.Open(imageName, FileMode.Open);
            var imageModel = new ImageModelBase
            {
                Id = id,
                PersonId = personId,
                Data = ReadFully(data)
            };

            return imageModel;
        }

        public ICollection<ImageModelBase> GetImages()
        {
            var files = Directory.GetFiles(RecognizerConfig.ImagesPath);
            var images = new List<ImageModelBase>();

            for (var i = 1; i <= files.Length; i++)
            {
                images.Add(GetImage(i));
            }

            return images;
        }

        public void SaveImage(ImageModelBase image, int personId)
        {
            var imageId = PrepareNewId();
            var imageFileName = $"{LocalStorageUtility.PrepareImageName(imageId, personId)}" +
                                $".{RecognizerConfig.DefaultImageExtension}";
            using (var imageToSave = Image.FromStream(new MemoryStream(image.Data)))
            {
                imageToSave.Save(Path.Combine(RecognizerConfig.ImagesPath, imageFileName), 
                    ImageFormat.Jpeg);
            }
        }

        public int PrepareNewId()
        {
            return Directory.GetFiles(RecognizerConfig.ImagesPath).Length + 1;
        }

        public static byte[] ReadFully(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
}
}

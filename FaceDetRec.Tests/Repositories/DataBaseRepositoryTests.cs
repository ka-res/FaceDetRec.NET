using NUnit.Framework;
using Moq;
using FaceDetRec.WPFClient.Repositories.Interfaces.DataBase;
using FaceDetRec.WPFClient.DataBase;
using FaceDetRec.WPFClient.DataModels;
using System;

namespace FaceDetRec.Tests.Repositories
{
    [TestFixture]
    public class DataBaseRepositoryTests
    {
        [Test]
        public void If_Is_Adding_Image()
        {
            var imagesRepository = new Mock<IImageRepositoryDb>();

            imagesRepository
                .Setup(x => x.AddImage(It.IsAny<ImageModelBase>(), It.IsAny<int>()))
                .Returns(new Image());

            var imageId = imagesRepository.Object.AddImage(new ImageModelBase(), 1);

            Assert.IsInstanceOf<Int32>(imageId);
            Assert.AreEqual(1, imageId);
            imagesRepository.VerifyAll();
        }

        [Test]
        public void Insert_Duplicate_Country_Throws_Exception()
        {
            var imagesRepository = new Mock<IImageRepositoryDb>();

            imagesRepository
                .Setup(x => x.AddImage(It.IsAny<ImageModelBase>(), It.IsAny<int>()))
                .Throws(new Exception());

            try
            {
                var imageId = imagesRepository.Object.AddImage(new ImageModelBase(), 1);

                Assert.Fail("Exception not thrown");
            }
            catch (Exception)
            {
                imagesRepository.VerifyAll();
            }
        }
    }
}

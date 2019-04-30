using Xunit;
using Moq;
using WebApp.Models;
using System.IO;

namespace Tests.ImageSavingTests
{
    public class ImageSaverTests
    {
        private ImageSaver imageSaver;
        private Mock<IFileIdProvider> idMock;
        private Mock<IImageWriter> writerMock;

        public ImageSaverTests()
        {
            Directory.CreateDirectory("test/");
        }

        [Fact]
        public void GetIdInGivenDirectoryAndFileExtention()
        {
            idMock = new Mock<IFileIdProvider>();
            writerMock = new Mock<IImageWriter>();

            imageSaver = new ImageSaver(idMock.Object, writerMock.Object);

            imageSaver.SaveImage("aaa", ".png","test/");

            idMock.Verify(im => im.GetId("test/", ".png"));
        }

        [Fact]
        public void PassFileNameBasedOnRecivedIdAndImageDataToWriter()
        {
            idMock = new Mock<IFileIdProvider>();
            idMock.Setup(im => im.GetId(It.IsAny<string>(), It.IsAny<string>())).Returns("id");
            writerMock = new Mock<IImageWriter>();

            imageSaver = new ImageSaver(idMock.Object, writerMock.Object);

            imageSaver.SaveImage("aaa", ".png", "test/");

            writerMock.Verify(wm => wm.SaveImage("id.png", "aaa"));
        }

        [Fact]
        public void ReturnValidFileId()
        {
            idMock = new Mock<IFileIdProvider>();
            idMock.Setup(im => im.GetId(It.IsAny<string>(), It.IsAny<string>())).Returns("id");
            writerMock = new Mock<IImageWriter>();

            imageSaver = new ImageSaver(idMock.Object, writerMock.Object);

            var @out = imageSaver.SaveImage("aaa", ".png", "test/");

            Assert.Equal("id", @out);
        }
    }
}

using Xunit;
using Moq;
using System.IO;
using WebApp.Models.FileManagement;

namespace Tests.FileManagementTests
{
    public class FileSaverTests
    {
        private FileSaver imageSaver;
        private Mock<IFileIdProvider> idMock;
        private Mock<IFileWriter> writerMock;

        public FileSaverTests()
        {
            Directory.CreateDirectory("test/");
        }

        [Fact]
        public void GetIdInGivenDirectoryAndFileExtention()
        {
            idMock = new Mock<IFileIdProvider>();
            writerMock = new Mock<IFileWriter>();

            imageSaver = new FileSaver(idMock.Object, writerMock.Object);

            imageSaver.SaveFile("aaa", ".png","test/");

            idMock.Verify(im => im.GetId("test/", ".png"));
        }

        [Theory]
        [InlineData(".png")]
        [InlineData(".jpg")]
        [InlineData(".json")]
        [InlineData(".jp2gmd")]
        public void PassFileNameBasedOnRecivedIdAndFileExtentionFileDataToWriter(string extention)
        {
            idMock = new Mock<IFileIdProvider>();
            idMock.Setup(im => im.GetId(It.IsAny<string>(), It.IsAny<string>())).Returns("id");
            writerMock = new Mock<IFileWriter>();

            imageSaver = new FileSaver(idMock.Object, writerMock.Object);

            imageSaver.SaveFile("aaa", extention, "test/");

            writerMock.Verify(wm => wm.SaveFile("test/id"+extention, "aaa"));
        }

        [Fact]
        public void ReturnValidFileId()
        {
            idMock = new Mock<IFileIdProvider>();
            idMock.Setup(im => im.GetId(It.IsAny<string>(), It.IsAny<string>())).Returns("id");
            writerMock = new Mock<IFileWriter>();

            imageSaver = new FileSaver(idMock.Object, writerMock.Object);

            var @out = imageSaver.SaveFile("aaa", ".png", "test/");

            Assert.Equal("id", @out);
        }
    }
}

using Moq;
using WebApp.Models.Factories;
using WebApp.Models.FileManagement;
using Xunit;
namespace Tests.FileManagementTests
{
    public class JsonFileManagerTests
    {
        private JsonFileManager imageManager;
        private Mock<IFileSaver> saverMock;
        private Mock<IFileRemover> removerMock;

        public JsonFileManagerTests()
        {
            saverMock = new Mock<IFileSaver>();
            removerMock = new Mock<IFileRemover>();

            var facMock = new Mock<IFileSaverFactory>();
            facMock.Setup(fm => fm.GetSaver(It.IsAny<SaverType>())).Returns(saverMock.Object);

            imageManager = new JsonFileManager(facMock.Object, removerMock.Object);
        }

        [Fact]
        public void CallSaverForSavingFile()
        {
            imageManager.SaveFile("data", "test/");

            saverMock.Verify(sm => sm.SaveFile("data", ".json", "test/"));
        }

        [Fact]
        public void CallRemoverForRemovingFile()
        {
            imageManager.RemoveFile("id", "test/");

            removerMock.Verify(sm => sm.RemoveImage("id", "test/", ".json"));
        }

        [Fact]
        public void ReturnRecivedIdFromSaver()
        {
            saverMock = new Mock<IFileSaver>();
            saverMock.Setup(sm => sm.SaveFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns("id");

            var facMock = new Mock<IFileSaverFactory>();
            facMock.Setup(fm => fm.GetSaver(It.IsAny<SaverType>())).Returns(saverMock.Object);

            imageManager = new JsonFileManager(facMock.Object, removerMock.Object);

            var id = imageManager.SaveFile("data", "test/");

            Assert.Equal("id", id);
        }
    }
}

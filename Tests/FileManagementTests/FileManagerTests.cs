using Moq;
using WebApp.Models.FileManagement;
using Xunit;
namespace Tests.FileManagementTests
{
    public class JsonFileManagerTests
    {
        private JsonFileManager imageManager;
        private Mock<IFileSaver> saverMock;
        private Mock<IFileRemover> removerMock;

        [Fact]
        public void CallSaverForSavingFile()
        {
            saverMock = new Mock<IFileSaver>();
            removerMock = new Mock<IFileRemover>();

            imageManager = new JsonFileManager(saverMock.Object, removerMock.Object);

            imageManager.SaveFile("data", "test/");

            saverMock.Verify(sm => sm.SaveImage("data", ".json", "test/"));
        }

        [Fact]
        public void CallRemoverForRemovingFile()
        {
            saverMock = new Mock<IFileSaver>();
            removerMock = new Mock<IFileRemover>();

            imageManager = new JsonFileManager(saverMock.Object, removerMock.Object);

            imageManager.RemoveFile("id", "test/");

            removerMock.Verify(sm => sm.RemoveImage("id","test/",".json"));
        }

        [Fact]
        public void ReturnRecivedIdFromSaver()
        {
            saverMock = new Mock<IFileSaver>();
            saverMock.Setup(sm => sm.SaveImage(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns("id");

            removerMock = new Mock<IFileRemover>();

            imageManager = new JsonFileManager(saverMock.Object, removerMock.Object);

            var id = imageManager.SaveFile("data", "test/");

            Assert.Equal("id", id);
        }
    }
}

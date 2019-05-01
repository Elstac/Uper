using Moq;
using WebApp.Models.ImageManagement;
using Xunit;
namespace Tests.ImageSavingTests
{
    public class ImageManagerTests
    {
        private PngImageManager imageManager;
        private Mock<IImageSaver> saverMock;
        private Mock<IImageRemover> removerMock;

        [Fact]
        public void CallSaverForSavingFile()
        {
            saverMock = new Mock<IImageSaver>();
            removerMock = new Mock<IImageRemover>();

            imageManager = new PngImageManager(saverMock.Object, removerMock.Object);

            imageManager.SaveImage("data", "test/");

            saverMock.Verify(sm => sm.SaveImage("data", ".png", "test/"));
        }

        [Fact]
        public void CallRemoverForRemovingFile()
        {
            saverMock = new Mock<IImageSaver>();
            removerMock = new Mock<IImageRemover>();

            imageManager = new PngImageManager(saverMock.Object, removerMock.Object);

            imageManager.RemoveImage("id", "test/");

            removerMock.Verify(sm => sm.RemoveImage("id","test/",".png"));
        }

        [Fact]
        public void ReturnRecivedIdFromSaver()
        {
            saverMock = new Mock<IImageSaver>();
            saverMock.Setup(sm => sm.SaveImage(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns("id");

            removerMock = new Mock<IImageRemover>();

            imageManager = new PngImageManager(saverMock.Object, removerMock.Object);

            var id = imageManager.SaveImage("data", "test/");

            Assert.Equal("id", id);
        }
    }
}

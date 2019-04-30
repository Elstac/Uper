using Xunit;
using Moq;
using WebApp.Models;

namespace Tests.ImageSavingTests
{
    public class ImageSaverTests
    {
        private ImageSaver imageSaver;
        private Mock<IImageIdProvider> idMock;
        private Mock<IImageWriter> writerMock;

        public ImageSaverTests()
        {

        }
    }
}

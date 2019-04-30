﻿using Xunit;
using Moq;
using WebApp.Models;
using System.IO;

namespace Tests.ImageSavingTests
{
    public class ImageSaverTests
    {
        private ImageSaver imageSaver;
        private Mock<IImageIdProvider> idMock;
        private Mock<IImageWriter> writerMock;

        public ImageSaverTests()
        {
            Directory.CreateDirectory("test/");
        }

        [Fact]
        public void GetIdInGivenDirectoryAndFileExtention()
        {
            idMock = new Mock<IImageIdProvider>();
            writerMock = new Mock<IImageWriter>();

            imageSaver = new ImageSaver(idMock.Object, writerMock.Object);

            imageSaver.SaveImage("aaa", ".png","test/");

            idMock.Verify(im => im.GetId("test/", ".png"));
        }

        [Fact]
        public void PassFileNameBasedOnRecivedIdAndImageDataToWriter()
        {
            idMock = new Mock<IImageIdProvider>();
            idMock.Setup(im => im.GetId(It.IsAny<string>(), It.IsAny<string>())).Returns("id");
            writerMock = new Mock<IImageWriter>();

            imageSaver = new ImageSaver(idMock.Object, writerMock.Object);

            imageSaver.SaveImage("aaa", ".png", "test/");

            writerMock.Verify(wm => wm.SaveImage("id.png", "aaa"));
        }
    }
}

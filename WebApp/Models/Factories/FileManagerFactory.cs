using System;
using WebApp.Models.FileManagement;
using Microsoft.Extensions.DependencyInjection;

public enum FileType
{
    Json,
    Png
}
namespace WebApp.Models.Factories
{
    public interface IFileManagerFactory
    {
        IFileManager GetManager(FileType type);
    }

    public class FileManagerFactory:IFileManagerFactory
    {
        private IServiceProvider provider;

        public FileManagerFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IFileManager GetManager(FileType type)
        {
            if (type == FileType.Json)
                return provider.GetService<JsonFileManager>();

            if (type == FileType.Png)
                return provider.GetService<PngFileManager>();

            throw new NotImplementedException();
        }
    }
}

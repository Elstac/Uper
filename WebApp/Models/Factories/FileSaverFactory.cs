using System;
using WebApp.Models.FileManagement;
using Microsoft.Extensions.DependencyInjection;
public enum SaverType
{
    Text,
    Image
}

namespace WebApp.Models.Factories
{

    public interface IFileSaverFactory
    {
        IFileSaver GetSaver(SaverType type);
    }

    public class FileSaverFactory : IFileSaverFactory
    {
        private IServiceProvider provider;

        public FileSaverFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IFileSaver GetSaver(SaverType type)
        {
            var idProvider = provider.GetService<IFileIdProvider>();

            if (type == SaverType.Text)
                return new FileSaver(idProvider, new TextFileWriter());

            if(type==SaverType.Image)
                return new FileSaver(idProvider, new ImageWriter());

            throw new NotImplementedException($"Given saver type: {type} creation not implemented");
        }
    }
}

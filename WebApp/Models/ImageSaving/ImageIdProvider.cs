namespace WebApp.Models
{
    public interface IImageIdProvider
    {
        string GetId(string directory,string extention);
    }

    public class ImageIdProvider : IImageIdProvider
    {
        public string GetId(string directory,string extention)
        {
            throw new System.NotImplementedException();
        }
    }
}

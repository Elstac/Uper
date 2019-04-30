namespace WebApp.Models
{
    public interface IImageIdProvider
    {
        string GetId();
    }

    public class ImageIdProvider : IImageIdProvider
    {
        public string GetId()
        {
            throw new System.NotImplementedException();
        }
    }
}

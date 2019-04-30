namespace WebApp.Models
{
    public interface IFileIdProvider
    {
        string GetId(string directory,string extention);
    }

    public class FileIdProvider : IFileIdProvider
    {
        public string GetId(string directory,string extention)
        {
            throw new System.NotImplementedException();
        }
    }
}

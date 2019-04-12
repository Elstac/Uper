namespace WebApp.Services
{
    public interface ITemplateProvider
    {
        string GetTemplate(string messageType);
    }
}
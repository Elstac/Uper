namespace WebApp.Models.HtmlNotifications
{
    public interface IHtmlNotificationProvider
    {
        string GetNotificationBody(string pClass, string content);
    }

    public class HtmlNotificationProvider : IHtmlNotificationProvider
    {
        public string GetNotificationBody(string pClass, string content)
        {
            throw new System.NotImplementedException();
        }
    }
}

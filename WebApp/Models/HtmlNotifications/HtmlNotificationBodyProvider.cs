namespace WebApp.Models.HtmlNotifications
{
    public interface INotificationBodyProvider
    {
        string GetNotificationBody(string pClass, string content);
    }

    public class HtmlNotificationBodyProvider : INotificationBodyProvider
    {
        public string GetNotificationBody(string pClass, string content)
        {
            return $"<div class =\"notification\"><p class=\"{pClass}\">{content}</p></div>";
        }
    }
}

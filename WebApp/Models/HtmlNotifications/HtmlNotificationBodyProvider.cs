namespace WebApp.Models.HtmlNotifications
{
    public interface IHtmlNotificationBodyProvider
    {
        string GetNotificationBody(string pClass, string content);
    }

    public class HtmlNotificationBodyProvider : IHtmlNotificationBodyProvider
    {
        public string GetNotificationBody(string pClass, string content)
        {
            return $"<div class =\"notification\"><p class=\"{pClass}\">{content}</p></div>";
        }
    }
}

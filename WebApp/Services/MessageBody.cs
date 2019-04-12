using System.Collections.Generic;

namespace WebApp.Services
{
    public class MessageBody
    {
        string Template { get; set; }
        string Head { get; set; }
        List<string> BodyParts { get; set; }
        string Footer { get; set; }
    }
}

using System.Collections.Generic;

namespace WebApp.Services
{
    public class MessageBody
    {
        public string Template { get; set; }
        public string Head { get; set; }
        public List<string> BodyParts { get; set; }
        public string Footer { get; set; }
    }
}

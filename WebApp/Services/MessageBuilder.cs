using System;
using System.Collections.Generic;
using MimeKit;
namespace WebApp.Services
{
    public interface IMessageBuilder
    {
        string Template { get; set; }
        string Head { get; set; }
        List<string> BodyParts { get; set; }
        string Footer { get; set; }
        
        MimeMessage BuildMessage();
    }

    public class MessageBuilder : IMessageBuilder
    {
        public string Template { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Head { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<string> BodyParts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Footer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public MimeMessage BuildMessage()
        {
            throw new NotImplementedException();
        }
    }
}

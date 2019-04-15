using System.Collections.Generic;

namespace WebApp.Services
{
    public interface IMessageBodyDictionary
    {
        string GetReplacement(string key);
    }

    public class MessageBody : Dictionary<string, string>, IMessageBodyDictionary
    {
        public void AddReplacement(string value, string key)
        {
            Add(key, value);
        }

        public string GetReplacement(string key)
        {
            return this[key];
        }
    }
}

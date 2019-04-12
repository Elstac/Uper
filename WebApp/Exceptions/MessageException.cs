using System;

namespace WebApp.Exceptions
{
    public class MessageException:Exception
    {
        public MessageException(string msg):base(msg)
        {

        }
    }
}

using System;
using System.Diagnostics.CodeAnalysis;

namespace WebApp.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class MessageException:Exception
    {
        public MessageException(string msg):base(msg)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Exceptions
{
    public class OwnNullArgumentException : ArgumentNullException
    {
        public OwnNullArgumentException(string message):base(message)
        {
        }
    }
}

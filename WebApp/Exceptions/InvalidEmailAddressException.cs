﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Exceptions
{
    public class InvalidEmailAddressException:Exception
    {
        public InvalidEmailAddressException(string message):base(message)
        {

        }
    }
}

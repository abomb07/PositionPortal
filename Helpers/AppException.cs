/**
 * Adam Bender
 * CST452 Mark Reha
 * 2/6/22
 * Custom exception handler
 **/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PositionPortal.Helpers
{
    // Custom exception class for throwing application specific exceptions that can be caught and handled within the application
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}

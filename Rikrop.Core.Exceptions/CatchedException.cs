using System;

namespace Rikrop.Core.Exceptions
{
    public class CatchedException : Exception
    {
        public CatchedException(string message, Exception exception)
            :base(message, exception)
        {
            
        }

        public CatchedException()
        {
            
        }
    }
}
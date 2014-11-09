using System;
using Rikrop.Core.Framework.Exceptions;

namespace Rikrop.Core.Exceptions
{
    public class CatchedExceptionHandler : IExceptionHandler
    {
        public bool Handle(Exception exception)
        {
            return exception is CatchedException;
        }
    }
}
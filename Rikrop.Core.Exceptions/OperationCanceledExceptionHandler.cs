using System;
using Rikrop.Core.Framework.Exceptions;

namespace Rikrop.Core.Exceptions
{
    public class OperationCanceledExceptionHandler : IExceptionHandler
    {
        public bool Handle(Exception exception)
        {
            return exception is OperationCanceledException;
        }
    }
}
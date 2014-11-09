using System;
using System.Diagnostics.Contracts;
using Rikrop.Core.Framework.Exceptions;

namespace Rikrop.Core.Exceptions
{
    public class RethrowCathedExceptionHandler : IExceptionHandler
    {
        private readonly IExceptionHandler _handler;

        public RethrowCathedExceptionHandler(IExceptionHandler handler)
        {
            Contract.Requires<ArgumentNullException>(handler != null);

            _handler = handler;
        }

        public bool Handle(Exception exception)
        {
            if (_handler.Handle(exception))
            {
                throw new CatchedException("Отловленное исключение", exception);
            }

            return false;
        }
    }
}
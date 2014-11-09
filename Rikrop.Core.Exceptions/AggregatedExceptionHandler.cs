using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Rikrop.Core.Framework.Exceptions;

namespace Rikrop.Core.Exceptions
{
    /// <summary>
    /// Пытается обработать исключение используя список обработчиков.
    /// Обрабатывает исключения по порядку хендлеров в списке.
    /// Останавливается, когда нашёлся первый обработчик, обработавший исключение.
    /// </summary>
    public class AggregatedExceptionHandler : IExceptionHandler
    {
        private readonly IReadOnlyCollection<IExceptionHandler> _exceptionHandlers;

        public AggregatedExceptionHandler(IReadOnlyCollection<IExceptionHandler> exceptionHandlers)
        {
            Contract.Requires<ArgumentNullException>(exceptionHandlers != null);

            _exceptionHandlers = exceptionHandlers;
        }

        public bool Handle(Exception exception)
        {
            return _exceptionHandlers.Any(handler => handler.Handle(exception));
        }
    }
}
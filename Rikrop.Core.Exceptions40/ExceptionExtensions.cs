using System;
using System.Collections.Generic;
using System.Text;

namespace Rikrop.Core.Exceptions
{
    public static class ExceptionExtensions
    {
        public static string Unwind(this Exception exception)
        {
            if (exception == null)
            {
                return null;
            }
            var inner = exception.InnerException.Unwind();
            if (string.IsNullOrWhiteSpace(inner))
            {
                return exception.Message;
            }
            return string.Format("{0}{1}{1}{2}",
                                 exception.Message,
                                 Environment.NewLine,
                                 inner);
        }

        public static string UnwindAll(this Exception exception)
        {
            //note: fuck recursion
            if (exception == null)
                return null;

            Exception ex;
            var inner = exception;
            var sb = new StringBuilder();

            do
            {
                ex = inner;
                if (inner != exception)
                    sb.Append("\t=> ");
                sb.AppendLine(ex.Message);
                inner = inner.InnerException;
            } while (inner != null);

            return String.Format("{0}{1}", sb, Environment.NewLine);
        }

        private const string ExceptionTemplate = "{0}Message: {1};\r\n{0}Exception Data: ({2});\r\n";
        private const string ExceptionTraceTemplate = "{0}Message: {1};\r\n{0}Exception Data: ({2});\r\n{0}StackTrace: {3};\r\n";

        public static string AsString(this Exception ex, bool withStackTrace, int tabulation = 0)
        {
            var tabs = tabulation > 0
                           ? new string('\t', tabulation)
                           : string.Empty;

            var lst = new List<string>();

            foreach (var key in ex.Data.Keys)
            {
                lst.Add(key + ": " + ex.Data[key]);
            }

            var data = string.Join("; ", lst);

            string str = withStackTrace
                             ? string.Format(ExceptionTraceTemplate, tabs, ex.Message, data, ex.StackTrace)
                             : string.Format(ExceptionTemplate, tabs, ex.Message, data);

            return str;
        }

        public static string UnwindAsString(this Exception ex, bool withStackTrace = true)
        {
            var sb = new StringBuilder()
                .AppendLine(ex.AsString(withStackTrace));

            var exception = ex.InnerException;

            var depth = 0;

            while (exception != null)
            {
                depth++;
                sb.AppendFormat("{0}InnerException:\r\n{1}", new string('\t', depth - 1), exception.AsString(withStackTrace, depth))
                    .AppendLine();

                exception = exception.InnerException;
            }

            return sb.ToString();
        }
    }
}
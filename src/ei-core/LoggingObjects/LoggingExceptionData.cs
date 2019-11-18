using ei_core.Converters;
using System;

namespace ei_core.LoggingObjects
{
    public class LoggingExceptionData
    {
        public Exception Exception { get; set; }

        public override string ToString()
        {
            return ObjectToStringConverter.ConvertExceptionToString(Exception);
        }
    }
}

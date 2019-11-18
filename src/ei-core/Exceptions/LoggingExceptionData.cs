using ei_core.Converters;
using System;

namespace ei_core.Exceptions
{
    public class LoggingExceptionData : Exception
    {
        public override string ToString()
        {
            return ObjectToStringConverter.ConvertExceptionToString(this);
        }
    }
}

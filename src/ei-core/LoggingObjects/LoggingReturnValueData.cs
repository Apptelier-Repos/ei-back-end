using ei_core.Converters;

namespace ei_core.LoggingObjects
{
    public class LoggingReturnValueData
    {
        public object Value { get; set; }

        public override string ToString()
        {
            return ObjectToStringConverter.ConvertToString(Value);
        }
    }
}

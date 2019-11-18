using ei_core.Converters;
using System.Reflection;
using System.Linq;

namespace ei_core.LoggingObjects
{
    public class LoggingOutputsData
    {
        public ParameterInfo[] OutputParameters { get; set; }
        public object[] OutputValues { get; set; }

        public override string ToString()
        {
            if (OutputParameters == null && OutputValues == null)
            {
                return string.Empty;
            }

            if (OutputParameters == null)
            {
                return ObjectToStringConverter.ConvertCollectionToString(OutputValues);
            }

            if (OutputValues == null)
            {
                return ObjectToStringConverter.ConvertCollectionToString(OutputParameters.Select(p => p.Name));
            }

            return ObjectToStringConverter.ConvertSomeToString(OutputParameters.Select(p => p.Name).ToArray(), OutputValues);
        }

    }
}

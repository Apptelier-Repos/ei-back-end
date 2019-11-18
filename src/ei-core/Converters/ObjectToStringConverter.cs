using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ei_core.Converters
{
    public static class ObjectToStringConverter
    {
        public static string ConvertSomeToString(string[] valuesNames, object[] values)
        {
            if (values == null || valuesNames == null)
            {
                return string.Empty;
            }

            var valuesCount = values.Length;
            if (valuesNames.Length != valuesCount)
            {
                return string.Empty;
            }

            var objectsData = new StringBuilder();
            for (int objectInx = 0; objectInx < valuesCount; objectInx++)
            {
                if (objectInx > 0)
                {
                    objectsData.Append("; ");
                }

                objectsData.Append(ConvertToString(valuesNames[objectInx], values[objectInx]));
            }

            return objectsData.ToString();
        }

        public static string ConvertToString(string valueName, object value)
        {
            return string.Format("{0}={1}", valueName, ConvertToString(value));
        }

        public static string ConvertToString(object value)
        {
            if (value == null)
            {
                return "null";
            }

            var valueType = value.GetType();

            if (valueType == typeof(string) || valueType.IsEnum || IsParsable(valueType))
            {
                return value.ToString();
            }

            if (value is Exception)
            {
                return ConvertExceptionToString(value as Exception);
            }

            if (value is IEnumerable)
            {
                return ConvertCollectionToString(value as IEnumerable);
            }

            if (value is Type)
            {
                return ConvertTypeToString(value as Type);
            }

            return ConvertClassToString(value);
        }

        public static string ConvertCollectionToString(IEnumerable col)
        {
            if (col == null)
            {
                return "null";
            }

            var sb = new StringBuilder("{");

            bool isFirstElement = true;

            foreach (var elem in col)
            {
                if (!isFirstElement)
                {
                    sb.Append(", ");
                }

                sb.Append(ConvertToString(elem));

                isFirstElement = false;
            }

            sb.Append("}");

            return sb.ToString();
        }

        public static string ConvertClassToString(object value)
        {
            if (value == null)
            {
                return "null";
            }

            var sb = new StringBuilder("{");

            bool isFirstElement = true;

            var classProperties = value.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead);

            foreach (var pi in classProperties)
            {
                if (!isFirstElement)
                {
                    sb.Append(", ");
                }

                sb.Append(ConvertToString(pi.Name, pi.GetValue(value, null)));

                isFirstElement = false;
            }

            var classFields = value.GetType()
                .GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var fi in classFields)
            {
                if (fi.IsStatic)
                {
                    continue;
                }

                if (!isFirstElement)
                {
                    sb.Append(", ");
                }

                sb.Append(ConvertToString(fi.Name, fi.GetValue(value)));

                isFirstElement = false;
            }

            sb.Append("}");

            return sb.ToString();
        }

        public static string ConvertExceptionToString(Exception e)
        {
            if (e == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            sb.Append(e.Message ?? string.Empty);

            if (e.InnerException != null)
            {
                sb.Append(" -> ");
                sb.Append(ConvertExceptionToString(e.InnerException));
            }

            return sb.ToString();
        }

        public static string ConvertTypeToString(Type t)
        {
            if (t == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            sb.Append(t.FullName);
            sb.Append(", ");
            sb.Append(t.Assembly.FullName);

            return sb.ToString();
        }

        private static bool IsParsable(Type t)
        {
            if (t == null)
            {
                return false;
            }

            var mi = t.GetMethod("Parse", new[] { typeof(string) });
            return mi != null;
        }
    }
}

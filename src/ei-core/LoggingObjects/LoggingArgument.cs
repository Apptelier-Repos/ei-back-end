using System;
using System.Text;

namespace ei_core.LoggingObjects
{
    public class LoggingArgument
    {
        public LoggingArgument()
        {
            LogTime = DateTime.Now;
            LogType = LoggingType.Information;
        }

        public DateTime LogTime { get; set; }
        public string OperationName { get; set; }
        public LoggingType LogType { get; set; }
        public LoggingInputsData InputsData { get; set; }
        public LoggingOutputsData OutputsData { get; set; }
        public LoggingReturnValueData ReturnValueData { get; set; }
        public LoggingExceptionData ExceptionData { get; set; }
        public LoggingInformationData InformationData { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Operation: {0}", OperationName ?? String.Empty);
            sb.AppendLine();
            sb.AppendFormat("\tLog type: {0}", LogType);
            sb.AppendLine();
            sb.AppendFormat("\tLog time: {0}", LogTime);
            sb.AppendLine();

            if (InputsData != null)
            {
                string strInputs = InputsData.ToString();
                if (!string.IsNullOrEmpty(strInputs))
                {
                    sb.AppendFormat("\tInputs: {0}", strInputs);
                    sb.AppendLine();
                }
            }

            if (OutputsData != null)
            {
                string strOutputs = OutputsData.ToString();
                if (!string.IsNullOrEmpty(strOutputs))
                {
                    sb.AppendFormat("\tOutputs: {0}", strOutputs);
                    sb.AppendLine();
                }
            }

            if (ReturnValueData != null)
            {
                string strReturnValue = ReturnValueData.ToString();
                if (!string.IsNullOrEmpty(strReturnValue))
                {
                    sb.AppendFormat("\tReturn value: {0}", strReturnValue);
                    sb.AppendLine();
                }
            }

            if (ExceptionData != null)
            {
                string strException = ExceptionData.ToString();
                if (!string.IsNullOrEmpty(strException))
                {
                    sb.AppendFormat("\tException: {0}", strException);
                    sb.AppendLine();
                }
            }

            if (InformationData != null)
            {
                string strInformation = InformationData.ToString();
                if (!string.IsNullOrEmpty(strInformation))
                {
                    sb.AppendFormat("\tInformation: {0}", strInformation);
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

    }
}

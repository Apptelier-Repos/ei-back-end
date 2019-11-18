using ei_core.Interfaces;
using ei_infrastructure.Logging.Strategy;
using System.Reflection;

namespace ei_infrastructure.Logging.Context
{
    public class LoggingContextDetails
    {
        #region Properties
        public MethodInfo MethodDetails { get; set; }
        public object[] Inputs { get; set; }

        public bool LogErrors { get; set; }
        public bool LogWarnings { get; set; }
        public bool LogInformation { get; set; }
        #endregion /Properties

        #region LoggingStrategy
        ILoggingStrategy _loggingStrategy;
        public ILoggingStrategy LoggingStrategy
        {
            get { return _loggingStrategy ?? (_loggingStrategy = new ConsoleLoggingStrategy()); }
            set { _loggingStrategy = value; }
        }
        #endregion
    }
}

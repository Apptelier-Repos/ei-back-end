using ei_core.Interfaces;
using ei_infrastructure.Logging.Strategy;
using System;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ei_infrastructure.Logging
{
    public class LoggingBehaviorAttribute : Attribute, IOperationBehavior/*, IServiceBehavior */
    {
        #region Properties

        public bool LogBeforeCall { get; set; }
        public bool LogAfterCall { get; set; }
        public bool LogErrors { get; set; }
        public bool LogWarnings { get; set; }
        public bool LogInformation { get; set; }

        #region LoggingStrategyType
        private Type _loggingStrategyType;
        public Type LoggingStrategyType
        {
            get { return _loggingStrategyType; }
            set
            {
                if (value != null &&
                    !value.GetInterfaces().Contains(typeof(ILoggingStrategy)))
                {
                    throw new ArgumentException("The specified type is not instance of ILoggingStrategy.");
                }

                _loggingStrategyType = value;
            }
        }
        #endregion /LoggingStrategyType

        #endregion /Properties

        public LoggingBehaviorAttribute()
        {
            LogBeforeCall = true;
            LogAfterCall = true;
            LogErrors = true;
            LogWarnings = true;
            LogInformation = true;
        }

        #region IOperationBehavior Members

        public void AddBindingParameters(OperationDescription operationDescription,
            BindingParameterCollection bindingParameters)
        {
        }

        public void Validate(OperationDescription operationDescription)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            throw new NotImplementedException();
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            throw new NotImplementedException();
        }

        #endregion /IOperationBehavior Members

        ILoggingStrategy GetLoggingStrategy()
        {
            if (LoggingStrategyType != null)
            {
                return Activator.CreateInstance(LoggingStrategyType) as ILoggingStrategy;
            }

            return new ConsoleLoggingStrategy();
        }
    }
}

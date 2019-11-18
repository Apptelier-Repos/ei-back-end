using ei_core.LoggingObjects;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel;

namespace ei_infrastructure.Logging.Context
{
    public class LoggingContext
    {
        #region Properties

        #region Contexts
        static Dictionary<string, LoggingContextDetails> _contexts =
            new Dictionary<string, LoggingContextDetails>();
        protected static Dictionary<string, LoggingContextDetails> Contexts
        {
            get { return _contexts; }
        }
        #endregion /Contexts

        public static LoggingContext Current
        {
            get
            {
                var res = new LoggingContext();

                var currContextId = GetCurrentContextId();

                lock (Contexts)
                {
                    if (Contexts.ContainsKey(currContextId))
                    {
                        res.Details = Contexts[currContextId];
                    }
                }

                return res;
            }
        }

        #region Details
        LoggingContextDetails _details;
        public LoggingContextDetails Details
        {
            get { return _details ?? (_details = new LoggingContextDetails()); }
            protected set { _details = value; }
        }
        #endregion /Details

        #endregion /Properties

        #region Contexts methods
        public static bool SetCurrentContextDetails(LoggingContextDetails contextDetails)
        {
            var currContextId = GetCurrentContextId();
            if (currContextId == null)
            {
                return false;
            }

            AddContext(currContextId, contextDetails, true);

            return true;
        }

        public static bool ClearCurrentContextDetails()
        {
            var currContextId = GetCurrentContextId();
            if (currContextId == null)
            {
                return false;
            }

            RemoveContext(currContextId);

            return true;
        }

        protected static string GetCurrentContextId()
        {
            var currContext = OperationContext.Current;
            if (currContext == null)
            {
                return null;
            }

            return currContext.Channel.SessionId;
        }

        protected static void AddContext(string id,
            LoggingContextDetails contextDetails,
            bool replaceIfExist)
        {
            if (id == null)
            {
                return;
            }

            lock (Contexts)
            {
                if (replaceIfExist && Contexts.ContainsKey(id))
                {
                    Contexts.Remove(id);
                }

                if (!Contexts.ContainsKey(id) && contextDetails != null)
                {
                    Contexts.Add(id, contextDetails);
                }
            }
        }

        protected static void RemoveContext(string id)
        {
            if (id == null)
            {
                return;
            }

            lock (Contexts)
            {
                if (Contexts.ContainsKey(id))
                {
                    Contexts.Remove(id);
                }
            }
        }
        #endregion /Contexts methods

        private LoggingArgument CreateArgumentForCommonLog()
        {
            var arg = new LoggingArgument();

            var mi = Details.MethodDetails;
            if (mi != null)
            {
                arg.OperationName = mi.Name;

                if (Details.Inputs != null && Details.Inputs.Length > 0)
                {
                    arg.InputsData = new LoggingInputsData
                    {
                        InputParameters = mi.GetParameters().Where(p => !p.IsOut).ToArray(),
                        InputValues = Details.Inputs
                    };
                }
            }

            return arg;
        }

        public bool Log(Exception ex, string text, LoggingType logType)
        {
            var arg = CreateArgumentForCommonLog();
            arg.LogType = logType;

            if (ex != null)
            {
                arg.ExceptionData = new LoggingExceptionData
                {
                    Exception = ex
                };
            }

            if (text != null)
            {
                arg.InformationData = new LoggingInformationData
                {
                    Text = text
                };
            }

            return Details.LoggingStrategy.Log(arg);
        }

        public bool LogError(Exception ex, string text)
        {
            if (Details.LogErrors)
            {
                return Log(ex, text, LoggingType.Error);
            }

            return false;
        }

        public bool LogWarning(Exception ex, string text)
        {
            if (Details.LogWarnings)
            {
                return Log(ex, text, LoggingType.Warning);
            }

            return false;
        }

        public bool LogInformation(string text)
        {
            if (Details.LogInformation)
            {
                return Log(null, text, LoggingType.Information);
            }

            return false;
        }

    }
}

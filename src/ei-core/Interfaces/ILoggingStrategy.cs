using ei_core.LoggingObjects;

namespace ei_core.Interfaces
{
    public interface ILoggingStrategy
    {
        bool Log(LoggingArgument arg);
    }
}

namespace ei_core.Interfaces
{
    /// <summary>
    /// This type eliminates the need to depend directly on the ASP.NET Core logging types.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAppLogger<T>
    {
        void LogInformation(T state, string message, params object[] args);
        void LogWarning(T state, string message, params object[] args);
    }
}
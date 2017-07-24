namespace VPortal.Core.Log
{
    public interface ILogProvider
    {
        ILogger GetLogger(string name);
    }
}
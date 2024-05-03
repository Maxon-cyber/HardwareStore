namespace HardwareStore.Services.Utils.Monitoring.Logger.Abstractions;

public interface ILogger
{
    Task LogInfoAsync(string message);

    void LogMessage(string message);

    void LogError(Exception exception, string message);
}
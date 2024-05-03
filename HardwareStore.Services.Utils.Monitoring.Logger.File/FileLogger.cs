using HardwareStore.Services.Utils.Monitoring.Logger.Abstractions;
using HardwareStore.Services.Utils.Monitoring.Templates.DirectoryInfoManagement;

namespace HardwareStore.Services.Utils.Monitoring.Logger.File;

public sealed class FileLogger : ILogger
{
    private FileInfoModel _fileInfo;
    private readonly string SEPARATOR = new string('-', 40);
    private const string MESSAGE_PATTERN = "{0:yyyy-MM-dd HH:mm:ss} [{1}]: {2}";

    public DirectoryInfoModel Directory { get; }

    private FileLogger(string path)
        => Directory = new DirectoryInfoModel(path);

    public static FileLogger Create(string path)
        => new FileLogger(path);

    public FileLogger SetFile(string fileName)
    {
        _fileInfo = Directory[fileName]
            ?? throw new FileNotFoundException($"Файл {Path.Combine(Directory.FullName, fileName)} не найден");

        return this;
    }

    public async Task LogInfoAsync(string message)
    {
        message = string.Format(MESSAGE_PATTERN, DateTime.Now, "Info", message);
        await _fileInfo.WriteAsync(message, SEPARATOR, WriteMode.Append);
    }

    public void LogError(Exception exception, string message)
    {
        throw new NotImplementedException();
    }

    public void LogMessage(string message)
    {
        throw new NotImplementedException();
    }
}
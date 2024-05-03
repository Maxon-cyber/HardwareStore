namespace HardwareStore.Services.Utils.Monitoring.Templates.DirectoryInfoManagement;

public enum WriteMode
{
    Append = 0,
    WriteAll = 1,
}

public sealed class FileInfoModel
{
    public string Name { get; internal set; }

    public string FullName { get; internal set; }

    public long Size { get; internal set; }

    public long SizeLimit { get; } = 4096L;

    public string Extension { get; internal set; }

    public DateTime LastAccessTime { get; internal set; }

    public DateTime LastWriteTime { get; internal set; }

    internal FileInfoModel() { }

    public async Task<string?> ReadAsync()
    {
        string content = await File.ReadAllTextAsync(FullName);

        return content ?? null;
    }

    public async Task WriteAsync(string content, string separator, WriteMode writeMode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(separator);

        if (Size >= SizeLimit)
            while (Size != SizeLimit / 2)
            {
                string[] lines = await File.ReadAllLinesAsync(FullName);

                await File.WriteAllLinesAsync(FullName, lines.SkipWhile(line => line != separator));
            }

        switch (writeMode)
        {
            case WriteMode.Append:
                await File.AppendAllTextAsync(FullName, content);
                break;
            case WriteMode.WriteAll:
                await File.WriteAllTextAsync(FullName, content);
                break;
            default:
                break;
        }
    }

    public async Task ClearAsync()
        => await File.WriteAllTextAsync(FullName, string.Empty);
}
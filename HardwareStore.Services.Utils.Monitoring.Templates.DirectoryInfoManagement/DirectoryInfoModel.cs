namespace HardwareStore.Services.Utils.Monitoring.Templates.DirectoryInfoManagement;

public sealed class DirectoryInfoModel
{
    private readonly List<FileInfoModel> _files = [];

    public string Name { get; }

    public string FullName { get; }

    public int CountOfFiles { get; }

    public DateTime LastAccessTime { get; }

    public DateTime LastWriteTime { get; }

    public DirectoryInfoModel(string path)
    {
        DirectoryInfo directory = new DirectoryInfo(path);

        Name = directory.Name;
        FullName = directory.FullName;
        LastAccessTime = directory.LastAccessTime;
        LastWriteTime = directory.LastWriteTime;

        FileInfo[] files = directory.GetFiles();

        foreach (FileInfo file in files)
            _files.Add(new FileInfoModel()
            {
                Name = Path.GetFileNameWithoutExtension(file.Name),
                FullName = file.FullName,
                Size = file.Length,
                Extension = file.Extension,
                LastAccessTime = file.LastAccessTime,
                LastWriteTime = file.LastWriteTime
            });

        CountOfFiles = _files.Count;
    }

    public FileInfoModel? this[string fileName]
    {
        get
        {
            foreach (FileInfoModel file in _files)
                if (string.Equals(file.Name, fileName, StringComparison.CurrentCultureIgnoreCase))
                    return file;

            return null;
        }
    }
}
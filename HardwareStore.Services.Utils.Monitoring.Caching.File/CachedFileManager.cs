using HardwareStore.Services.Utils.Monitoring.Caching.Abstractions;
using HardwareStore.Services.Utils.Monitoring.Templates.DirectoryInfoManagement;
using HardwareStore.Services.Utils.Serializer.Yaml;
using System.Collections.Immutable;

namespace HardwareStore.Services.Utils.Monitoring.Caching.File;

public sealed class CachedFileManager<TKey, TValue> : ICache<TKey, TValue>
    where TKey : IEquatable<TKey>
{
    private readonly string SEPARATOR = new string('-', 40);

    private IImmutableDictionary<TKey, TValue> _data = ImmutableDictionary.Create<TKey, TValue>();

    private FileInfoModel _fileInfo;
    private readonly YamlSerializer _serializer = new YamlSerializer();
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 20);

    public DirectoryInfoModel Directory { get; }

    private CachedFileManager(string path)
        => Directory = new DirectoryInfoModel(path);

    public static CachedFileManager<TKey, TValue> Create(string path)
        => new CachedFileManager<TKey, TValue>(path);

    public async Task<CachedFileManager<TKey, TValue>> SetFile(string fileName)
    {
        _fileInfo = Directory[fileName]
            ?? throw new FileNotFoundException($"Файл {Path.Combine(Directory.FullName, fileName)} не найден");

        _data = await ReadAsync();

        return this;
    }

    public async Task<IImmutableDictionary<TKey, TValue>?> ReadAsync()
    {
        await _semaphore.WaitAsync();

        string? content = await _fileInfo.ReadAsync();

        if (content == null)
            return null;

        _data = _serializer.Deserialize<Dictionary<TKey, TValue>>(content).ToImmutableDictionary();

        _semaphore.Release();

        return _data;
    }

    public async Task<TValue?> ReadByKeyAsync(TKey key)
    {
        await _semaphore.WaitAsync();

        if (!(_data.Count == 0))
            _data = await ReadAsync();

        TValue? result = _data.Where(dict => dict.Key.Equals(key)).First().Value;

        _semaphore.Release();

        return result;
    }

    public async Task WriteAsync(TKey key, TValue value)
    {
        await _semaphore.WaitAsync();

        string serializedValues = _serializer.Serialize(new Dictionary<TKey, TValue>()
        {
            {
                key,
                value
            },
        });

        await _fileInfo.WriteAsync(serializedValues, SEPARATOR, WriteMode.Append);

        _semaphore.Release();
    }

    public async Task<bool> ContainsKeyAsync(TKey key)
    {
        await _semaphore.WaitAsync();

        bool isContains = _data.Any(dict => dict.Key.Equals(key));

        _semaphore.Release();

        return isContains;
    }

    public async Task ClearAsync()
    {
        await _semaphore.WaitAsync();

        await _fileInfo.ClearAsync();

        _semaphore.Release();
    }

    public async Task RemoveByAsync(TKey key)
    {
        await _semaphore.WaitAsync();


        if (_data == null)
            return;

        _data = _data.Where(dict => !dict.Key.Equals(key)).ToImmutableDictionary();

        string serializedValues = _serializer.Serialize(_data);
        await _fileInfo.WriteAsync(serializedValues, SEPARATOR, WriteMode.WriteAll);

        _semaphore.Release();
    }
}
using HardwareStore.Services.Utils.Monitoring.Caching.Abstractions;
using System.Collections.Immutable;

namespace HardwareStore.Services.Utils.Monitoring.Caching.InMemory;

public sealed class MemoryCache<TKey, TValue>() : ICache<TKey, TValue>
    where TKey : IEquatable<TKey>
{
    private readonly static Dictionary<TKey, TValue> _cache = [];
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 20);

    public async Task ClearAsync()
    {
        await _semaphore.WaitAsync();

        _cache.Clear();

        _semaphore.Release();
    }

    public async Task<bool> ContainsKeyAsync(TKey key)
    {
        await _semaphore.WaitAsync();

        bool isContains = await Task.FromResult(_cache.ContainsKey(key));

        _semaphore.Release();

        return isContains;
    }

    public async Task<IImmutableDictionary<TKey, TValue>?> ReadAsync()
    {
        await _semaphore.WaitAsync();

        ImmutableDictionary<TKey, TValue> cache = _cache.ToImmutableDictionary();

        _semaphore.Release();

        return cache;
    }

    public async Task<TValue?> ReadByKeyAsync(TKey key)
    {
        await _semaphore.WaitAsync();

        if (_cache.TryGetValue(key, out TValue? value))
            return await Task.FromResult(value);

        _semaphore.Release();

        return await Task.FromResult(default(TValue));
    }

    public async Task<IEnumerable<TValue>> ReadByValueAsync(Func<TValue, bool> predicate)
    {
        await _semaphore.WaitAsync();

        IEnumerable<TValue> values = _cache.Values.Where(predicate);

        _semaphore.Release();

        return await Task.FromResult(values);
    }

    public async Task RemoveByAsync(TKey key)
    {
        await _semaphore.WaitAsync();

        _cache.Remove(key);

        _semaphore.Release();
    }

    public async Task WriteAsync(TKey key, TValue value)
    {
        await _semaphore.WaitAsync();

        _cache[key] = value;

        _semaphore.Release();
    }
}
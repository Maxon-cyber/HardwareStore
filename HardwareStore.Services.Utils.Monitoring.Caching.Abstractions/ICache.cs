using System.Collections.Immutable;

namespace HardwareStore.Services.Utils.Monitoring.Caching.Abstractions;

public interface ICache<TKey, TValue>
    where TKey : IEquatable<TKey>
{
    Task<IImmutableDictionary<TKey, TValue>?> ReadAsync();

    Task<TValue?> ReadByKeyAsync(TKey key);

    Task WriteAsync(TKey key, TValue value);

    Task<bool> ContainsKeyAsync(TKey key);

    Task RemoveByAsync(TKey key);

    Task ClearAsync();
}
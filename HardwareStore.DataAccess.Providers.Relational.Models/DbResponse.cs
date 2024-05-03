using System.Collections.Immutable;

namespace HardwareStore.DataAccess.Providers.Relational.Models;

public sealed class DbResponse<TType>()
{
    public IImmutableQueue<TType> QueryResult { get; } = ImmutableQueue.Create<TType>();

    public IImmutableDictionary<object, object> AdditionalData { get; } = ImmutableDictionary.Create<object, object>();

    public string Message { get; set; }

    public Exception? Error { get; set; }

    public object? OutputValue { get; set; }

    public object? ReturnedValue { get; set; }
}
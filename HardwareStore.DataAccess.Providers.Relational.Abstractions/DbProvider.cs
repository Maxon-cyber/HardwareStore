using HardwareStore.DataAccess.Providers.Relational.Abstractions.Common;
using HardwareStore.DataAccess.Providers.Relational.Models;
using HardwareStore.Entities;
using System.Data.Common;

namespace HardwareStore.DataAccess.Providers.Relational.Abstractions;

public abstract class DbProvider(ConnectionParameters connectionParameters) : IDisposable, IAsyncDisposable
{
    protected readonly ConnectionParameters _connectionParameters = connectionParameters;

    protected DbConnection _dbConnection;
    protected DbCommand _dbCommand;

    public abstract string Prefix { get; }

    public abstract string Provider { get; }

    public abstract DbConnection DbConnection { get; }

    public abstract DbCommand DbCommand { get; }

    public abstract void Dispose();

    public abstract ValueTask DisposeAsync();
}
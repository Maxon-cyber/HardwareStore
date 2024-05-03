using HardwareStore.DataAccess.Providers.Relational.Abstractions;
using HardwareStore.DataAccess.Providers.Relational.Abstractions.Common;
using HardwareStore.DataAccess.Providers.Relational.Models;
using HardwareStore.Entities;
using HardwareStore.Providers.Relational.DataTools.ADO;
using Microsoft.Data.SqlClient;

namespace HardwareStore.DataAccess.Providers.Relational.SqlServer;

public sealed class SqlServerProvider : DbProvider
{
    private readonly ADOEntityDataAccessService _ado;

    public SqlServerProvider(ConnectionParameters connectionParameters) : base(connectionParameters)
        => _ado = new ADOEntityDataAccessService(Provider, Prefix, DbConnection, DbCommand);

    public override string Prefix => "@";

    public override string Provider => "SqlServerProvider";

    public override SqlConnection DbConnection
    {
        get
        {
            SqlConnectionStringBuilder sqlConnectionBuilder = new SqlConnectionStringBuilder
            {
                DataSource = $"{_connectionParameters.Server}",
                InitialCatalog = _connectionParameters.Database,
                IntegratedSecurity = _connectionParameters.IntegratedSecurity
            };

            if (!_connectionParameters.IntegratedSecurity)
            {
                sqlConnectionBuilder.UserID = _connectionParameters.Username;
                sqlConnectionBuilder.Password = _connectionParameters.Password;
            }

            sqlConnectionBuilder.TrustServerCertificate = _connectionParameters.TrustServerCertificate;

            if (_connectionParameters.ConnectionTimeout.HasValue)
                sqlConnectionBuilder.ConnectTimeout = (int)_connectionParameters.ConnectionTimeout.Value.TotalSeconds;
            if (_connectionParameters.MaxPoolSize.HasValue)
                sqlConnectionBuilder.MaxPoolSize = _connectionParameters.MaxPoolSize.Value;

            _dbConnection = new SqlConnection(sqlConnectionBuilder.ToString());

            return (_dbConnection as SqlConnection)!;
        }
    }

    public override SqlCommand DbCommand
    {
        get
        {
            if (_dbConnection == null)
                throw new Exception();

            _dbCommand = new SqlCommand()
            {
                Connection = _dbConnection as SqlConnection,
                CommandTimeout = 30
            };

            return (_dbCommand as SqlCommand)!;
        }
    }

    public Task<DbResponse<TEntity>> GetByAsync<TEntity>(QueryParameters queryParameters, TEntity entityCondition, CancellationToken token)
        where TEntity : Entity
        => _ado.GetEntityByAsync(queryParameters, entityCondition, token);

    public Task<DbResponse<TEntity>> SelectAsync<TEntity>(QueryParameters queryParameters, CancellationToken token)
        where TEntity : Entity
        => _ado.SelectEntitiesAsync<TEntity>(queryParameters, token);

    public Task<DbResponse<TEntity>> SelectByAsync<TEntity>(QueryParameters queryParameters, TEntity entityCondition, CancellationToken token)
        where TEntity : Entity
        => _ado.SelectEntitiesByAsync(queryParameters, entityCondition, token);

    public Task<DbResponse<TEntity>> ChangeAsync<TEntity>(QueryParameters queryParameters, TEntity entity, CancellationToken token)
        where TEntity : Entity
        => _ado.ChangeEntityAsync(queryParameters, entity, token);

    public Task<IEnumerable<DbResponse<TEntity>>> ChangeAsync<TEntity>(QueryParameters queryParameters, IEnumerable<TEntity> entities, CancellationToken token)
        where TEntity : Entity
        => _ado.ChangeEntityAsync(queryParameters, entities, token);

    public override void Dispose()
        => _ado.Dispose();

    public override async ValueTask DisposeAsync()
        => await _ado.DisposeAsync();
}
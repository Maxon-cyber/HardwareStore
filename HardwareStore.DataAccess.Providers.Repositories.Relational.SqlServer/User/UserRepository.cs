using HardwareStore.DataAccess.Providers.Relational.Models;
using HardwareStore.DataAccess.Providers.Relational.SqlServer;
using HardwareStore.DataAccess.Repositories.Relational.Abstractions;
using HardwareStore.Entities.User;

namespace HardwareStore.DataAccess.Providers.Repositories.Relational.SqlServer.User;

public sealed class UserRepository(SqlServerProvider sqlServer) : IRepository<UserEntity>
{
    public Task<DbResponse<UserEntity>> GetByAsync(QueryParameters queryParameters, UserEntity entityCondition, CancellationToken token)
        => sqlServer.GetByAsync(queryParameters, entityCondition, token);

    public Task<DbResponse<UserEntity>> SelectAsync(QueryParameters queryParameters, CancellationToken token)
        => sqlServer.SelectAsync<UserEntity>(queryParameters, token);

    public Task<DbResponse<UserEntity>> SelectByAsync(QueryParameters queryParameters, UserEntity userCondition, CancellationToken token)
        => sqlServer.SelectByAsync(queryParameters, userCondition, token);

    public Task<DbResponse<UserEntity>> ChangeAsync(QueryParameters queryParameters, UserEntity user, CancellationToken token)
        => sqlServer.ChangeAsync(queryParameters, user, token);

    public Task<IEnumerable<DbResponse<UserEntity>>> ChangeAsync(QueryParameters queryParameters, IEnumerable<UserEntity> users, CancellationToken token)
        => sqlServer.ChangeAsync(queryParameters, users, token);

    public void Dispose()
        => sqlServer.Dispose();

    public async ValueTask DisposeAsync()
        => await sqlServer.DisposeAsync();
}
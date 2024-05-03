using HardwareStore.DataAccess.Providers.Relational.Models;
using HardwareStore.DataAccess.Providers.Relational.SqlServer;
using HardwareStore.DataAccess.Repositories.Relational.Abstractions;
using HardwareStore.Entities.Order;

namespace HardwareStore.DataAccess.Providers.Repositories.Relational.SqlServer.Order;

public sealed class OrderRepository(SqlServerProvider sqlServer) : IRepository<OrderEntity>
{
    public Task<DbResponse<OrderEntity>> GetByAsync(QueryParameters queryParameters, OrderEntity orderCondition, CancellationToken token)
        => sqlServer.GetByAsync(queryParameters, orderCondition, token);

    public Task<DbResponse<OrderEntity>> SelectAsync(QueryParameters queryParameters, CancellationToken token)
        => sqlServer.SelectAsync<OrderEntity>(queryParameters, token);

    public Task<DbResponse<OrderEntity>> SelectByAsync(QueryParameters queryParameters, OrderEntity orderCondition, CancellationToken token)
        => sqlServer.SelectByAsync(queryParameters, orderCondition, token);

    public Task<DbResponse<OrderEntity>> ChangeAsync(QueryParameters queryParameters, OrderEntity order, CancellationToken token)
        => sqlServer.ChangeAsync(queryParameters, order, token);

    public Task<IEnumerable<DbResponse<OrderEntity>>> ChangeAsync(QueryParameters queryParameters, IEnumerable<OrderEntity> orders, CancellationToken token)
        => sqlServer.ChangeAsync(queryParameters, orders, token);

    public void Dispose()
        => sqlServer.Dispose();

    public async ValueTask DisposeAsync()
        => await sqlServer.DisposeAsync();
}
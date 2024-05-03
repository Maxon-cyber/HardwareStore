using HardwareStore.DataAccess.Providers.Relational.Models;
using HardwareStore.Entities;

namespace HardwareStore.Providers.Relational.DataTools.Abstractions;

public interface IEntityDataAccessService : IDisposable, IAsyncDisposable
{
    Task<DbResponse<TEntity>> GetEntityByAsync<TEntity>(QueryParameters query, TEntity entityCondition, CancellationToken token)
        where TEntity : Entity;

    Task<DbResponse<TEntity>> SelectEntitiesAsync<TEntity>(QueryParameters query, CancellationToken token)
        where TEntity : Entity;

    Task<DbResponse<TEntity>> SelectEntitiesByAsync<TEntity>(QueryParameters query, TEntity entityCondition, CancellationToken token)
        where TEntity : Entity;

    Task<DbResponse<TEntity>> ChangeEntityAsync<TEntity>(QueryParameters query, TEntity entity, CancellationToken token)
        where TEntity : Entity;

    Task<IEnumerable<DbResponse<TEntity>>> ChangeEntityAsync<TEntity>(QueryParameters query, IEnumerable<TEntity> entities, CancellationToken token)
        where TEntity : Entity;
}
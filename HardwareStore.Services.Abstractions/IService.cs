using HardwareStore.Entities;
using System.Collections.Immutable;

namespace HardwareStore.Services.Abstractions;

public interface IService<TEntity> : IDisposable, IAsyncDisposable
    where TEntity : Entity, new()
{
    Task<TEntity?> GetByAsync(TEntity condition);

    Task<IEnumerable<TEntity>?> SelectAsync();

    Task<IEnumerable<TEntity>?> SelectByAsync(TEntity condition);

    Task<object?> ChangeAsync(TypeOfUpdateCommand typeOfCommand, TEntity entity);

    Task<ImmutableDictionary<string, object?>> ChangeAsync(TypeOfUpdateCommand typeOfCommand, IEnumerable<TEntity> entities);

}
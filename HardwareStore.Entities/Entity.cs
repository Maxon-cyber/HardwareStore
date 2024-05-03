using HardwareStore.Entities.Common.Attributes;
using System.Data;

namespace HardwareStore.Entities;

public abstract class Entity()
{
    [ColumnData(ColumnName = "id", DbType = DbType.Guid)]
    public Guid Id { get; }

    [ColumnData(ColumnName = "time_created", DbType = DbType.DateTime2)]
    public DateTime TimeCreated { get; }

    [ColumnData(ColumnName = "last_access_time", DbType = DbType.DateTime2)]
    public DateTime LastAccessTime { get; }

    [ColumnData(ColumnName = "last_update_time", DbType = DbType.DateTime2)]
    public DateTime LastUpdateTime { get; }
}
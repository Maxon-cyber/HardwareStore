using HardwareStore.Entities.Common.Attributes;
using HardwareStore.Entities.Product;
using System.Data;

namespace HardwareStore.Entities.Order;

public sealed class OrderEntity : Entity
{
    [ColumnData(ColumnName = "user_id", DbType = DbType.UInt64)]
    public ulong UserId { get; init; }

    [ColumnData(ColumnName = "products", DbType = DbType.Xml)]
    public IList<ProductEntity> Products { get; init; }

    [ColumnData(ColumnName = "total_amount", DbType = DbType.Decimal)]
    public decimal TotalAmount { get; init; }

    [ColumnData(ColumnName = "order_date", DbType = DbType.DateTime2)]
    public DateTime OrderDate { get; init; }

    [ColumnData(ColumnName = "status", DbType = DbType.String)]
    public Status Status { get; init; }
}
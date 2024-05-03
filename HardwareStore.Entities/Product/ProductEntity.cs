using HardwareStore.Entities.Common.Attributes;
using System.Data;

namespace HardwareStore.Entities.Product;

public sealed class ProductEntity() : Entity
{
    [ColumnData(ColumnName = "name", DbType = DbType.String)]
    public string Name { get; init; }

    [ColumnData(ColumnName = "image", DbType = DbType.Binary)]
    public byte[] Image { get; init; }

    [ColumnData(ColumnName = "quantity", DbType = DbType.UInt32)]
    public uint Quantity { get; init; }

    [ColumnData(ColumnName = "category", DbType = DbType.String)]
    public string Category { get; init; }

    [ColumnData(ColumnName = "price", DbType = DbType.Decimal)]
    public decimal Price { get; init; }

    public string ToString(string message)
        => $"{Name}-{Price}({message})";

    public override string ToString()
        => $"{Name}-{Price}";

    public override bool Equals(object? obj)
        => base.Equals(obj);

    public override int GetHashCode()
        => base.GetHashCode();
}
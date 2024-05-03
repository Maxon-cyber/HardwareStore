using System.Data;
using System.Text;

namespace HardwareStore.Providers.Relational.DataTools.ADO.Sql;

internal static class SqlHelper
{
    internal static object ConvertToDbType(object dbValue, DbType dbType)
        => dbType switch
        {
            DbType.AnsiString or DbType.String => Convert.ChangeType(dbValue, typeof(string)),
            DbType.AnsiStringFixedLength or DbType.StringFixedLength => Convert.ChangeType(dbValue, typeof(char[])),
            DbType.Binary => Encoding.UTF8.GetBytes((string)dbValue),
            DbType.Boolean => Convert.ChangeType(dbValue, typeof(bool)),
            DbType.Byte => Convert.ChangeType(dbValue, typeof(byte)),
            DbType.Currency or DbType.Decimal => Convert.ChangeType(dbValue, typeof(decimal)),
            DbType.Date or DbType.DateTime or DbType.DateTime2 => Convert.ChangeType(dbValue, typeof(DateTime)),
            DbType.DateTimeOffset => Convert.ChangeType(dbValue, typeof(DateTimeOffset)),
            DbType.Double => Convert.ChangeType(dbValue, typeof(double)),
            DbType.Guid => Convert.ChangeType(dbValue, typeof(Guid)),
            DbType.Int16 => Convert.ChangeType(dbValue, typeof(short)),
            DbType.Int32 => Convert.ChangeType(dbValue, typeof(int)),
            DbType.Int64 => Convert.ChangeType(dbValue, typeof(long)),
            DbType.Object => Convert.ChangeType(dbValue, typeof(object)),
            DbType.SByte => Convert.ChangeType(dbValue, typeof(sbyte)),
            DbType.Single => Convert.ChangeType(dbValue, typeof(float)),
            DbType.Time => Convert.ChangeType(dbValue, typeof(TimeSpan)),
            DbType.UInt16 => Convert.ChangeType(dbValue, typeof(ushort)),
            DbType.UInt32 => Convert.ChangeType(dbValue, typeof(uint)),
            DbType.UInt64 => Convert.ChangeType(dbValue, typeof(ulong)),
            DbType.VarNumeric => Convert.ChangeType(dbValue, typeof(decimal)),
            DbType.Xml => Convert.ChangeType(dbValue, typeof(string)),
            _ => throw new ArgumentException($"Unsupported DbType: {dbType}")
        };

    internal static object ConvertToCLRType(object value, Type conversionType)
    {
        if (conversionType.IsValueType)
        {
            if (conversionType.IsEnum)
                return Enum.Parse(conversionType, (string)value);
        }

        return Convert.ChangeType(value, conversionType);
    }
}
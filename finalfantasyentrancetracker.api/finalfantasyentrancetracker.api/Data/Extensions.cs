using System.Data;
using System.Reflection;

namespace finalfantasyentrancetracker.api.Data
{
    public static class Extensions
    {
        public static string ToSqlType(this Type type)
        {
            SqlDbType rValue = SqlDbType.NVarChar;

            if (type == typeof(Int32))
                rValue = SqlDbType.Int;
            else if (type == typeof(Int64))
                rValue = SqlDbType.BigInt;
            else if (type == typeof(DateTime))
                rValue = SqlDbType.DateTime;

            if (rValue == SqlDbType.NVarChar)
                return $"{rValue}(255)";
            return $"{rValue}";
        }

        public static string? GetSqlSchema(this Type type)
        {
            var attribute = type.GetCustomAttribute<SqlTableAttribute>();
            if (attribute != null)
                return attribute.Schema;
            return null;
        }
        
        public static string? GetSqlTableName(this Type type)
        {
            var attribute = type.GetCustomAttribute<SqlTableAttribute>();
            if (attribute != null)
                return attribute.Name;
            throw new NullReferenceException("Missing sql table name"); ;
        }

        public static bool SqlIgnore(this PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<SqlColumnAttribute>();
            if (attribute == null)
                return false;
            return attribute.Ignore;
        }
    }
}

using finalfantasyentrancetracker.api.Data;
using finalfantasyentrancetracker.api.Model;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace finalfantasyentrancetracker.api.UseCase.SQL
{
    public class BaseSqlUseCase : BaseUseCase
    {
        public BaseSqlUseCase(IOptions<ConfigConnectionStrings> options) : base(options)
        {
        }

        protected async Task<List<Type>> GetClassesWithSqlAttributeAsync()
        {
            // Get the currently loaded assemblies
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // Find all types with the Sql attribute across all loaded assemblies
            var sqlClasses = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetCustomAttributes(typeof(SqlTableAttribute), false).Length > 0)
                .ToList();

            return sqlClasses;
        }

        protected async Task<IEnumerable<PropertyInfo>> GetOrderedProperties(Type csClass)
        {
            var properties = csClass.GetProperties();
            return properties.OrderBy(x =>
            {
                var sql = x.GetCustomAttribute<SqlColumnAttribute>();
                if (sql == null) return 2;
                if (sql.First) return 1;
                if (sql.Last) return 3;
                return 2;
            });
        }

      
    }
}

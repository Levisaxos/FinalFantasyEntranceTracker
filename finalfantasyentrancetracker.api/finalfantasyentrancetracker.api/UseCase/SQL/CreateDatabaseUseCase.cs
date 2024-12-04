using finalfantasyentrancetracker.api.Data;
using finalfantasyentrancetracker.api.Helper;
using finalfantasyentrancetracker.api.Model;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace finalfantasyentrancetracker.api.UseCase.SQL
{
    public class CreateDatabaseUseCase : BaseSqlUseCase
    {
        private const string CREATETABLE = "CREATE TABLE";
        private const string OPENINGBRACKET = "(";
        private const string CLOSINGBRACKET = ")";
        private const string COMMA = ", ";
        private const string DEFAULTSCHEMA = "dbo";
        private const string NOTNULL = "NOT NULL";

        private List<Type> _csClasses;
        private List<PropertyInfo> _foreignKeys;

        public CreateDatabaseUseCase(IOptions<ConfigConnectionStrings> options) : base(options)
        {
        }

        public async Task Handle()
        {
            await InitializeAsync();
            await CreateDatabase();
            LogHelper.Instance.Report("Created database");
        }

        private async Task CreateDatabase()
        {
            var query = await GetCreationQuery();
            await ExecuteSql(query);
        }

        private async Task InitializeAsync()
        {
            _foreignKeys = new List<PropertyInfo>();
            _csClasses = await GetClassesWithSqlAttributeAsync();
        }

        private async Task<string> GetCreationQuery()
        {
            var rValue = new StringBuilder();
            foreach (var csClass in _csClasses)
            {
                rValue.Append(await GetTableCreationQuery(csClass));
            }

            foreach (var foreignKey in _foreignKeys)
            {
                rValue.Append(await GetForeignKeyQuery(foreignKey));
            }

            return rValue.ToString();
        }



        private async Task<string> GetTableCreationQuery(Type csClass)
        {
            var rValue = new StringBuilder();

            var sql = csClass.GetCustomAttribute<SqlTableAttribute>();
            var schema = csClass.GetSqlSchema() ?? DEFAULTSCHEMA;
            var tableName = csClass.GetSqlTableName();

            rValue.Append($"{CREATETABLE} {schema}.{tableName} {OPENINGBRACKET}");
            foreach (var csProperty in await GetOrderedProperties(csClass))
            {
                if (csProperty.SqlIgnore())
                    continue;

                var foreignKey = csProperty.GetCustomAttribute<ForeignKeyAttribute>();
                if (foreignKey != null)
                    _foreignKeys.Add(csProperty);

                rValue.Append(await GetPropertyQuery(csProperty));
                rValue.Append(COMMA);
            }
            rValue.Remove(rValue.Length - COMMA.Length, COMMA.Length);
            rValue.Append(CLOSINGBRACKET);
            rValue.Append(Environment.NewLine);
            return rValue.ToString();
        }

        private async Task<string> GetForeignKeyQuery(PropertyInfo property)
        {
            var foreignKey = property.GetCustomAttribute<ForeignKeyAttribute>();
            if (foreignKey == null)
                throw new NullReferenceException($"{nameof(GetForeignKeyQuery)}.{nameof(foreignKey)}");

            var csClass = _csClasses.First(x => x.Name == property.DeclaringType?.Name);
            var sourceSchema = csClass.GetSqlSchema() ?? DEFAULTSCHEMA;
            var sourceTable = csClass.GetSqlTableName();
            var sourceColumn = property.Name;

            var targetClass = _csClasses.First(x => x.Name == foreignKey.TargetTable);
            var targetSchema = targetClass.GetSqlSchema() ?? DEFAULTSCHEMA;
            var targetTable = targetClass.GetSqlTableName();
            var targetColumn = foreignKey.TargetColumn;

            return $"{Environment.NewLine}ALTER TABLE {sourceSchema}.{sourceTable} ADD CONSTRAINT fk_{sourceTable}_{sourceColumn}_{targetTable}_{targetColumn} FOREIGN KEY ({sourceColumn}) REFERENCES {targetSchema}.{targetTable}({targetColumn})";
        }

        private async Task<string> GetPropertyQuery(PropertyInfo csProperty)
        {
            try
            {
                var sqlAttribute = csProperty.GetCustomAttribute<SqlColumnAttribute>();
                var isPrimary = (sqlAttribute != null && sqlAttribute.PrimaryKey) ? "PRIMARY KEY" : "";
                var isIdentity = (sqlAttribute != null && sqlAttribute.Identity) ? "IDENTITY" : "";
                var rValue = new StringBuilder();
                rValue.Append($"{csProperty.Name} {csProperty.PropertyType.ToSqlType()} {NOTNULL} {isPrimary} {isIdentity}".Trim());
                return rValue.ToString();
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
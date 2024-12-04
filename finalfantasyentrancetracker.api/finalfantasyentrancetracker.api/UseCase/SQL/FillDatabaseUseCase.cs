using Dapper;
using finalfantasyentrancetracker.api.Data;
using finalfantasyentrancetracker.api.Helper;
using finalfantasyentrancetracker.api.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace finalfantasyentrancetracker.api.UseCase.SQL
{
    public class FillDatabaseUseCase : BaseSqlUseCase
    {
        public FillDatabaseUseCase(IOptions<ConfigConnectionStrings> options) : base(options)
        {
        }

        public async Task Handle()
        {
            await FillTable<MQItemType>("json/ItemTypes.json", ["Name"]);
            await FillTable<MQItem>("json/Items.json", ["Name", "ImagePath", "ItemRow", "Sequence", "ItemTypeId"]);
        }



        private async Task FillTable<T>(string jsonFilePath, string[] properties)
        {
            var items = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(jsonFilePath));

            var classType = typeof(T);
            var schemaName = classType.GetSqlSchema();
            var tableName = classType.GetSqlTableName();

            var parameterNames = properties.Select(p => $"@{p.ToLower()}").ToArray();
            var columnNames = properties.Select(p => p).ToArray();

            var insertSql = @$"
DECLARE @updatedOn DATETIME = GETDATE()
INSERT INTO {schemaName}.{tableName}(
    {string.Join(", ", columnNames)}, 
    CreatedOn, 
    UpdatedOn
) 
VALUES(
    {string.Join(", ", parameterNames)}, 
    @updatedOn, 
    @updatedOn
)";

            foreach (var item in items)
            {
                var parameters = new DynamicParameters();

                foreach (var prop in classType.GetProperties())
                {
                    if (!properties.Any(x => x == prop.Name))
                        continue;

                    var value = prop.GetValue(item);
                    parameters.Add($"@{prop.Name.ToLower()}", value);
                }
                await ExecuteSql(insertSql, parameters);
                LogHelper.Instance.Report($"Inserted item to {schemaName}.{tableName}");
            }
        }
    }
}

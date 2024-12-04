
using finalfantasyentrancetracker.api.Helper;
using finalfantasyentrancetracker.api.Model;
using Microsoft.Extensions.Options;

namespace finalfantasyentrancetracker.api.UseCase.SQL
{
    public class DropDatabaseUseCase : BaseSqlUseCase
    {
        public DropDatabaseUseCase(IOptions<ConfigConnectionStrings> options) : base(options)
        {
        }

        internal async Task Handle()
        {
            if (await DropAllKeys())
                LogHelper.Instance.Report("Dropped all keys");
            else
                LogHelper.Instance.Report("Failed to drop all keys");
            if (await DropAllTables())
                LogHelper.Instance.Report("Dropped all tables");
            else
                LogHelper.Instance.Report("Failed to drop all tables");
        }

        private async Task<bool> DropAllKeys()
        {
            var sql = @"
DECLARE @sql NVARCHAR(MAX) = (SELECT STRING_AGG('ALTER TABLE '+ TABLE_SCHEMA+'.' + TABLE_NAME+ ' DROP CONSTRAINT ' + CONSTRAINT_NAME, CHAR(10) + CHAR(13)) FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE)
EXEC (@sql)";
            var result = await ExecuteSql(sql);
            return result;
        }

        private async Task<bool> DropAllTables()
        {
            var sql = @"
DECLARE @sql NVARCHAR(MAX) = (SELECT STRING_AGG('DROP TABLE ' + TABLE_SCHEMA + '.' + TABLE_NAME, CHAR(10) + CHAR(13)) FROM INFORMATION_SCHEMA.TABLES)
EXEC(@sql)";
            var result = await ExecuteSql(sql);
            return result;
        }
    }
}

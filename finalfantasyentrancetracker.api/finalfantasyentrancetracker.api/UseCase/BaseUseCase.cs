using Dapper;
using finalfantasyentrancetracker.api.Helper;
using finalfantasyentrancetracker.api.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace finalfantasyentrancetracker.api.UseCase
{
    public class BaseUseCase
    {
        protected readonly ConfigConnectionStrings _config;
        public BaseUseCase(IOptions<ConfigConnectionStrings> options)
        {
            _config = options.Value;
        }
        protected async Task<bool> ExecuteSql(string sql, DynamicParameters parameters = null)
        {
            try
            {
                using (var conn = new SqlConnection(_config.HomeConnection))
                    await conn.QueryAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                LogHelper.Instance.Report(@$"
---------------------------------------------------------------------------------------------------------------------------------------
Failed to execute query: 
{sql}

Error: {ex.Message}");
                return false;
            }
            return true;
        }
    }
}

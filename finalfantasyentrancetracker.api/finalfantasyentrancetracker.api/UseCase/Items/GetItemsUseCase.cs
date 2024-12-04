using Dapper;
using finalfantasyentrancetracker.api.Model;
using finalfantasyentrancetracker.api.Model.Api;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace finalfantasyentrancetracker.api.UseCase.Items
{
    public class GetItemsUseCase : BaseUseCase
    {
        public GetItemsUseCase(IOptions<ConfigConnectionStrings> options) : base(options)
        {
        }

        public async Task<IEnumerable<MQItem>> Handle()
        {
            return await GetItems();

        }

        private async Task<IEnumerable<MQItem>> GetItems()
        {
            using (var conn = new SqlConnection(_config.HomeConnection))
            {
                return conn.Query<MQItem>(@"
    SELECT i.Id,
       i.Name,
       i.ImagePath,
       i.ItemTypeId,
	   it.Name AS ItemType,
       i.ItemRow,
       i.Sequence,
       i.CreatedOn,
       i.UpdatedOn FROM master.Item i
	INNER JOIN master.ItemType it ON it.Id = i.ItemTypeId");
            }
        }
    }
}

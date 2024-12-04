using finalfantasyentrancetracker.api.Model;
using finalfantasyentrancetracker.api.UseCase.Items;
using Microsoft.AspNetCore.Mvc;

namespace finalfantasyentrancetracker.api.Controllers
{
    [ApiController]
    public class ItemController : ControllerBase
    {
        GetItemsUseCase _getItemsUseCase;
        public ItemController(
                IConfiguration configuration,
                GetItemsUseCase getItemsUseCase) : base(configuration)
        {
            _getItemsUseCase = getItemsUseCase;
        }

        [Route("/api/getItems")]
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await _getItemsUseCase.Handle();
            var apiResult = new ApiResultDto<IEnumerable<MQItem>>(items);
            return OkJson(apiResult);

        }
    }
}
using finalfantasyentrancetracker.api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;

namespace finalfantasyentrancetracker.api.Controllers
{
    public class ControllerBase
    {
        public readonly string _connectionString;
        public readonly IConfiguration _configuration;
        public ControllerBase(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration["ConnectionStrings:HomeConnection"];
        }
        [NonAction]
        public virtual OkResult Ok()
        {
            return new OkResult();
        }

        public virtual OkObjectResult OkJson<T>([ActionResultObjectValue] ApiResultDto<T> result)
        {
            return new OkObjectResult(JsonConvert.SerializeObject(result));
        }

        [NonAction]
        public virtual OkObjectResult Ok([ActionResultObjectValue] object value)
        {
            return new OkObjectResult(value);
        }

        [NonAction]
        public virtual BadRequestResult BadRequest()
        {
            return new BadRequestResult();
        }

        [NonAction]
        public virtual BadRequestObjectResult BadRequest([ActionResultObjectValue] object error)
        {
            return new BadRequestObjectResult(error);
        }

        [NonAction]
        public virtual UnauthorizedResult Unauthorized()
        {
            return new UnauthorizedResult();
        }

    }
}

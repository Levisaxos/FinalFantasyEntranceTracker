using finalfantasyentrancetracker.api.Data;
using finalfantasyentrancetracker.api.Helper;
using finalfantasyentrancetracker.api.UseCase.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Reflection;

namespace finalfantasyentrancetracker.api.Controllers
{
    [ApiController]
    public class SqlController : ControllerBase
    {

        private IConfiguration _configuration;
        private DropDatabaseUseCase _dropDatabaseUseCase;
        private CreateDatabaseUseCase _createDatabaseUseCase;
        private FillDatabaseUseCase _fillDatabaseUseCase;
        public SqlController(
            IConfiguration configuration,
            DropDatabaseUseCase dropDatabaseUseCase,
            CreateDatabaseUseCase createDatabaseUseCase,
            FillDatabaseUseCase fillDatabaseUseCase) : base(configuration)
        {
            _configuration = configuration;
            _dropDatabaseUseCase = dropDatabaseUseCase;
            _createDatabaseUseCase = createDatabaseUseCase;
            _fillDatabaseUseCase = fillDatabaseUseCase;
        }

        [Route("/api/sql/dropdatabase")]
        [HttpGet]
        public async Task<IActionResult> DropDatabase()
        {
            await _dropDatabaseUseCase.Handle();
            return FinishRequest();
        }

        [Route("/api/sql/createdatabase")]
        [HttpGet]
        public async Task<IActionResult> CreateDatabase()
        {
            await _createDatabaseUseCase.Handle();
            return FinishRequest();
        }

        [Route("/api/sql/recreatedatabase")]
        [HttpGet]
        public async Task<IActionResult> RecreateDatabase()
        {
            await _dropDatabaseUseCase.Handle();
            await _createDatabaseUseCase.Handle();
            await _fillDatabaseUseCase.Handle();
            return FinishRequest();
        }


        private IActionResult FinishRequest()
        {
            var logReport = LogHelper.Instance.GetLogReport();
            if (!string.IsNullOrEmpty(logReport))
                return BadRequest(logReport);

            return Ok();
        }
    }
}

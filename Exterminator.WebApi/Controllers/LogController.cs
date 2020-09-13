using Exterminator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Exterminator.WebApi.Controllers
{
    [Route("api/logs")]
    public class LogController : Controller
    {
        // TODO: Implement route which gets all logs from the ILogService, which should be injected through the constructor
        private ILogService _logService;
        public LogController(ILogService logSerivce)
        {
            _logService = logSerivce;   
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetAllLogs()
        {
            return Ok(_logService.GetAllLogs());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using my_books_V1._0.Data.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace my_books_V1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private LogsService _logsService;

        public LogsController(LogsService logsService)
        {
            _logsService = logsService;
        }
        [HttpGet("get-all-logs-from-db")]
        // GET: /<controller>/
        public IActionResult GetAllLogsfromDB()
        {
            try
            {
                var alllogs = _logsService.GetAllLogsfromDB();
                return Ok(alllogs);
            }
            catch(Exception)
            {
                return BadRequest("Could not load logs from the database");
            }
        }
    }
}

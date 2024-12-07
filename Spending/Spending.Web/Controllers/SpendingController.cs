using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spending.Web.Repositories;

namespace Spending.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpendingController : ControllerBase
    {
        private ISpendingRepository _repository;

        public SpendingController(ISpendingRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentSpedning()
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            var data = await _repository.GetSpending(month, year);

            return Ok(data);
        }

        [HttpGet("month")]
        public async Task<IActionResult> GetSpedningForMonth([FromQuery] int month, [FromQuery] int year, [FromQuery] int template)
        {
            bool temp = template == 0 ? false : true;

            var data = await _repository.GetSpending(month, year, temp);

            return Ok(data);
        }

        [HttpGet("template")]
        public async Task<IActionResult> GetTemplate()
        {
            var data = await _repository.GetTemplate();

            return Ok(data);
        }
    }
}

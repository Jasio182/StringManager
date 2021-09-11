using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain.Requests;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IMediator mediator;

        public ManufacturersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("instruments")]
        public async Task<ActionResult> GetInstrumentsManufacturersAsync([FromQuery] GetInstrumentsManufacturersRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response.Data);
        }

        [HttpGet]
        [Route("strings")]
        public async Task<ActionResult> GetStringsManufacturersAsync([FromQuery] GetStringsManufacturersRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<ActionResult> AddManufacturerAsync([FromBody] AddManufacturerRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response.Data);
        }
    }
}

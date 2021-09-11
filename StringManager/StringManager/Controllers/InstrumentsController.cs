using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain.Requests;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InstrumentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public InstrumentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetInstrumentsAsync([FromQuery] GetInstrumentsRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<ActionResult> AddInstrumentAsync([FromBody] AddInstrumentRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response.Data);
        }
    }
}

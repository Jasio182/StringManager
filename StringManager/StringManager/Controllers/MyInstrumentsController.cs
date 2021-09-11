using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain.Requests;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MyInstrumentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public MyInstrumentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetMyInstrumentsAsync([FromQuery] GetMyInstrumentsRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response.Data);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult> GetMyInstrumentAsync([FromQuery] GetMyInstrumentRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<ActionResult> AddMyInstrumentAsync([FromBody] AddMyInstrumentRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response.Data);
        }

        [HttpPut]
        public async Task<ActionResult> ModifyMyInstrumentAsync([FromBody] ModifyMyInstrumentRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response.Data);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveMyInstrumentAsync([FromBody] RemoveMyInstrumentRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response.Data);
        }
    }
}

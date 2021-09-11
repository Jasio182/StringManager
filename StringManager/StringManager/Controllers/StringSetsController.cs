using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain.Requests;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StringSetsController : ControllerBase
    {
        private readonly IMediator mediator;

        public StringSetsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetStringsSetsAsync([FromQuery] GetStringsSetsRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response.Data);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult> GetStringsSetAsync([FromQuery] GetStringsSetRequest request)
        {
            var response = await mediator.Send(request);
            if (response.Data == null)
                return NotFound();
            return Ok(response.Data);
        }
    }
}

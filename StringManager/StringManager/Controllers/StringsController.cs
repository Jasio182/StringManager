using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain.Requests;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StringsController : ControllerBase
    {
        private readonly IMediator mediator;

        public StringsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetStringsAsync([FromQuery] GetStringsRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response.Data);
        }
    }
}

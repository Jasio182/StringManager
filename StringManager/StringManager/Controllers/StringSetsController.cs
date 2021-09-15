using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StringSetsController : ApiControllerBase
    {
        public StringSetsController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public Task<IActionResult> GetStringsSetsAsync([FromQuery] GetStringsSetsRequest request)
        {
            return HandleResult<GetStringsSetsRequest, GetStringsSetsResponse>(request);
        }

        [HttpGet]
        [Route("{Id}")]
        public Task<IActionResult> GetStringsSetAsync([FromQuery] GetStringsSetRequest request)
        {
            return HandleResult<GetStringsSetRequest, GetStringsSetResponse>(request);
        }
    }
}

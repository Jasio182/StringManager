using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StringsController : ApiControllerBase
    {
        public StringsController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public Task<IActionResult> GetStringsAsync([FromQuery] GetStringsRequest request)
        {
            return HandleResult<GetStringsRequest, GetStringsResponse>(request);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StringsController : ApiControllerBase<StringsController>
    {
        public StringsController(IMediator mediator, ILogger<StringsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("StringsController started");
        }

        [HttpGet]
        public Task<IActionResult> GetStringsAsync([FromQuery] GetStringsRequest request)
        {
            return HandleResult<GetStringsRequest, GetStringsResponse>(request);
        }
    }
}

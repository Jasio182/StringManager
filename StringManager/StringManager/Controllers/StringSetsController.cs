using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class StringSetsController : ApiControllerBase<StringSetsController>
    {
        public StringSetsController(IMediator mediator, ILogger<StringSetsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("StringSetsController started");
        }

        [HttpGet]
        public Task<IActionResult> GetStringsSetsAsync([FromQuery] GetStringsSetsRequest request)
        {
            return HandleResult<GetStringsSetsRequest, StatusCodeResponse>(request);
        }

        [HttpGet]
        [Route("{Id}")]
        public Task<IActionResult> GetStringsSetAsync([FromQuery] GetStringsSetRequest request)
        {
            return HandleResult<GetStringsSetRequest, StatusCodeResponse>(request);
        }
    }
}

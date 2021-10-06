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
    public class StringsInSetsController : ApiControllerBase<StringsInSetsController>
    {
        public StringsInSetsController(IMediator mediator, ILogger<StringsInSetsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("StringsInSetsController started");
        }

        [HttpPost]
        public Task<IActionResult> AddStringInSetAsync([FromBody] AddStringInSetRequest request)
        {
            return HandleResult<AddStringInSetRequest, StatusCodeResponse>(request);
        }

        [HttpPut]
        public Task<IActionResult> ModifyStringInSetAsync([FromBody] ModifyStringInSetRequest request)
        {
            return HandleResult<ModifyStringInSetRequest, StatusCodeResponse>(request);
        }

        [HttpDelete]
        public Task<IActionResult> RemoveStringInSetAsync([FromQuery] RemoveStringInSetRequest request)
        {
            return HandleResult<RemoveStringInSetRequest, StatusCodeResponse>(request);
        }
    }
}

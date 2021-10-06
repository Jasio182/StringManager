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
    public class StringsController : ApiControllerBase<StringsController>
    {
        public StringsController(IMediator mediator, ILogger<StringsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("StringsController started");
        }

        [AllowAnonymous]
        [HttpGet]
        public Task<IActionResult> GetStringsAsync()
        {
            var request = new GetStringsRequest();
            return HandleResult<GetStringsRequest, StatusCodeResponse>(request);
        }

        [HttpPost]
        public Task<IActionResult> AddStringAsync([FromBody] AddStringRequest request)
        {
            return HandleResult<AddStringRequest, StatusCodeResponse>(request);
        }

        [HttpPut]
        public Task<IActionResult> ModifyStringAsync([FromBody] ModifyStringRequest request)
        {
            return HandleResult<ModifyStringRequest, StatusCodeResponse>(request);
        }

        [HttpDelete]
        public Task<IActionResult> RemoveStringAsync([FromQuery] RemoveStringRequest request)
        {
            return HandleResult<RemoveStringRequest, StatusCodeResponse>(request);
        }
    }
}

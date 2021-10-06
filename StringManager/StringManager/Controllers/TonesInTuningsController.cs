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
    public class TonesInTuningsController : ApiControllerBase<TonesInTuningsController>
    {
        public TonesInTuningsController(IMediator mediator, ILogger<TonesInTuningsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("TonesInTuningsController started");
        }

        [HttpPost]
        public Task<IActionResult> AddToneInTuningAsync([FromBody] AddToneInTuningRequest request)
        {
            return HandleResult<AddToneInTuningRequest, StatusCodeResponse>(request);
        }

        [HttpPut]
        public Task<IActionResult> ModifyToneInTuningAsync([FromBody] ModifyToneInTuningRequest request)
        {
            return HandleResult<ModifyToneInTuningRequest, StatusCodeResponse>(request);
        }

        [HttpDelete]
        public Task<IActionResult> RemoveToneInTuningAsync([FromQuery] RemoveToneInTuningRequest request)
        {
            return HandleResult<RemoveToneInTuningRequest, StatusCodeResponse>(request);
        }
    }
}

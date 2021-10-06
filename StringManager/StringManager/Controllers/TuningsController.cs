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
    [ApiController]
    [Route("[controller]")]
    public class TuningsController : ApiControllerBase<TuningsController>
    {
        public TuningsController(IMediator mediator, ILogger<TuningsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("TuningsController started");
        }

        [AllowAnonymous]
        [HttpGet]
        public Task<IActionResult> GetTuningsAsync()
        {
            var request = new GetTuningsRequest();
            return HandleResult<GetTuningsRequest, StatusCodeResponse>(request);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{Id}")]
        public Task<IActionResult> GetTuningAsync([FromQuery] GetTuningRequest request)
        {
            return HandleResult<GetTuningRequest, StatusCodeResponse>(request);
        }

        [HttpPost]
        public Task<IActionResult> AddTuningAsync([FromBody] AddTuningRequest request)
        {
            return HandleResult<AddTuningRequest, StatusCodeResponse>(request);
        }

        [HttpPut]
        public Task<IActionResult> ModifyTuningAsync([FromBody] ModifyTuningRequest request)
        {
            return HandleResult<ModifyTuningRequest, StatusCodeResponse>(request);
        }

        [HttpDelete]
        public Task<IActionResult> RemoveTuningAsync([FromQuery] RemoveTuningRequest request)
        {
            return HandleResult<RemoveTuningRequest, StatusCodeResponse>(request);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
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
            return HandleResult<GetTuningsRequest, GetTuningsResponse>(request);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{Id}")]
        public Task<IActionResult> GetTuningAsync([FromQuery] GetTuningRequest request)
        {
            return HandleResult<GetTuningRequest, GetTuningResponse>(request);
        }
    }
}

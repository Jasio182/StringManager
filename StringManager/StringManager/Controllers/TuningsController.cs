using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Threading.Tasks;

namespace StringManager.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TuningsController : ApiControllerBase<TuningsController>
    {
        public TuningsController(IMediator mediator, ILogger<TuningsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("TuningsController started");
        }

        [HttpGet]
        public Task<IActionResult> GetTuningsAsync([FromQuery] GetTuningsRequest request)
        {
            return HandleResult<GetTuningsRequest, GetTuningsResponse>(request);
        }

        [HttpGet]
        [Route("{Id}")]
        public Task<IActionResult> GetTuningAsync([FromQuery] GetTuningRequest request)
        {
            return HandleResult<GetTuningRequest, GetTuningResponse>(request);
        }
    }
}

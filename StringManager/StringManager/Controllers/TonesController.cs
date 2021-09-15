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
    public class TonesController : ApiControllerBase<TonesController>
    {
        public TonesController(IMediator mediator, ILogger<TonesController> logger) : base(mediator, logger)
        {
            logger.LogInformation("TonesController started");
        }

        [HttpGet]
        public Task<IActionResult> GetTonesAsync([FromQuery] GetTonesRequest request)
        {
            return HandleResult<GetTonesRequest, GetTonesResponse>(request);
        }
    }
}

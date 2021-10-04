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
    public class TonesController : ApiControllerBase<TonesController>
    {
        public TonesController(IMediator mediator, ILogger<TonesController> logger) : base(mediator, logger)
        {
            logger.LogInformation("TonesController started");
        }

        [AllowAnonymous]
        [HttpGet]
        public Task<IActionResult> GetTonesAsync()
        {
            var request = new GetTonesRequest();
            return HandleResult<GetTonesRequest, StatusCodeResponse>(request);
        }
    }
}

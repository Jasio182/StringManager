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
    public class StringTensionController : ApiControllerBase<StringTensionController>
    {
        public StringTensionController(IMediator mediator, ILogger<StringTensionController> logger) : base(mediator, logger)
        {
            logger.LogInformation("StringSetsController started");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ScaleLenght")]
        public Task<IActionResult> GetScaleLenghtAsync([FromQuery] GetScaleLenghtsRequest request)
        {
            return HandleResult<GetScaleLenghtsRequest, StatusCodeResponse>(request);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("StringsInSize")]
        public Task<IActionResult> GetStringSizeWithCorrepondingTensionAsync([FromQuery] GetStringSizeWithCorrepondingTensionRequest request)
        {
            return HandleResult<GetStringSizeWithCorrepondingTensionRequest, StatusCodeResponse>(request);
        }

        [HttpGet]
        [Route("StringsSets")]
        public Task<IActionResult> GetStringsSetsWithCorrepondingTensionAsync([FromQuery] GetStringsSetsWithCorrepondingTensionRequest request)
        {
            return HandleResult <GetStringsSetsWithCorrepondingTensionRequest, StatusCodeResponse>(request);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("StringTension")]
        public Task<IActionResult> GetStringTensionAsync([FromQuery] GetStringTensionRequest request)
        {
            return HandleResult<GetStringTensionRequest, StatusCodeResponse>(request);
        }
    }
}

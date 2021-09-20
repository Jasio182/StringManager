using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StringTensionController : ApiControllerBase<StringTensionController>
    {
        public StringTensionController(IMediator mediator, ILogger<StringTensionController> logger) : base(mediator, logger)
        {
            logger.LogInformation("StringSetsController started");
        }

        [HttpGet]
        [Route("ScaleLenght")]
        public Task<IActionResult> GetScaleLenghtAsync([FromQuery] GetScaleLenghtsRequest request)
        {
            return HandleResult<GetScaleLenghtsRequest, GetScaleLenghtsResponse>(request);
        }

        [HttpGet]
        [Route("StringsInSize")]
        public Task<IActionResult> GetStringSizeWithCorrepondingTensionAsync([FromQuery] GetStringSizeWithCorrepondingTensionRequest request)
        {
            return HandleResult<GetStringSizeWithCorrepondingTensionRequest, GetStringSizeWithCorrepondingTensionResponse>(request);
        }

        [HttpGet]
        [Route("StringsSets")]
        public Task<IActionResult> GetStringsSetsWithCorrepondingTensionAsync([FromQuery] GetStringsSetsWithCorrepondingTensionRequest request)
        {
            return HandleResult <GetStringsSetsWithCorrepondingTensionRequest, GetStringsSetsWithCorrepondingTensionResponse>(request);
        }

        [HttpGet]
        [Route("StringTension")]
        public Task<IActionResult> GetStringTensionAsync([FromQuery] GetStringTensionRequest request)
        {
            return HandleResult<GetStringTensionRequest, GetStringTensionResponse>(request);
        }
    }
}

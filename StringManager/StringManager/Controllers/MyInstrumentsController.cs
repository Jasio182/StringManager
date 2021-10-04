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
    public class MyInstrumentsController : ApiControllerBase<MyInstrumentsController>
    {
        public MyInstrumentsController(IMediator mediator, ILogger<MyInstrumentsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("MyInstrumentsController started");
        }

        [HttpGet]
        public Task<IActionResult> GetMyInstrumentsAsync([FromQuery] GetMyInstrumentsRequest request)
        {
            return HandleResult<GetMyInstrumentsRequest, StatusCodeResponse>(request);
        }

        [HttpGet]
        [Route("{Id}")]
        public Task<IActionResult> GetMyInstrumentAsync([FromQuery] GetMyInstrumentRequest request)
        {
            return HandleResult<GetMyInstrumentRequest, StatusCodeResponse>(request);
        }

        [HttpPost]
        public Task<IActionResult> AddMyInstrumentAsync([FromBody] AddMyInstrumentRequest request)
        {
            return HandleResult<AddMyInstrumentRequest, StatusCodeResponse>(request);
        }

        [HttpPut]
        public Task<IActionResult> ModifyMyInstrumentAsync([FromBody] ModifyMyInstrumentRequest request)
        {
            return HandleResult<ModifyMyInstrumentRequest, StatusCodeResponse>(request);
        }

        [HttpDelete]
        public Task<IActionResult> RemoveMyInstrumentAsync([FromBody] RemoveMyInstrumentRequest request)
        {
            return HandleResult<RemoveMyInstrumentRequest, StatusCodeResponse>(request);
        }
    }
}

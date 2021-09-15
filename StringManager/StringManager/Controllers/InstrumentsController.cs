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
    public class InstrumentsController : ApiControllerBase<InstrumentsController>
    {

        public InstrumentsController(IMediator mediator, ILogger<InstrumentsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("InstrumentsController started");
        }

        [HttpGet]
        public Task<IActionResult> GetInstrumentsAsync([FromQuery] GetInstrumentsRequest request)
        {
            return HandleResult<GetInstrumentsRequest, GetInstrumentsResponse>(request);
        }

        [HttpPost]
        public Task<IActionResult> AddInstrumentAsync([FromBody] AddInstrumentRequest request)
        {
            return HandleResult<AddInstrumentRequest, AddInstrumentResponse>(request);
        }
    }
}

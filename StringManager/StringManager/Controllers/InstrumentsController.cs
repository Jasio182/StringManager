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
    [Route("[controller]")]
    [ApiController]
    public class InstrumentsController : ApiControllerBase<InstrumentsController>
    {

        public InstrumentsController(IMediator mediator, ILogger<InstrumentsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("InstrumentsController started");
        }

        [HttpGet]
        public Task<IActionResult> GetInstrumentsAsync()
        {
            var request = new GetInstrumentsRequest();
            return HandleResult<GetInstrumentsRequest, GetInstrumentsResponse>(request);
        }

        [HttpPost]
        public Task<IActionResult> AddInstrumentAsync([FromBody] AddInstrumentRequest request)
        {
            return HandleResult<AddInstrumentRequest, AddInstrumentResponse>(request);
        }
    }
}

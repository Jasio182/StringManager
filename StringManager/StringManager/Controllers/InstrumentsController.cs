using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InstrumentsController : ApiControllerBase
    {

        public InstrumentsController(IMediator mediator) : base(mediator) { }

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

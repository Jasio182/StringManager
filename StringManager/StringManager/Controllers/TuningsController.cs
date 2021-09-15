using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StringManager.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TuningsController : ApiControllerBase
    {
        public TuningsController(IMediator mediator) : base(mediator) { }

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

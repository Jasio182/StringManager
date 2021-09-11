using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;
using StringManager.Services.API.Domain.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StringManager.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TuningsController : ControllerBase
    {
        private readonly IMediator mediator;

        public TuningsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetTuningsAsync([FromQuery] GetTuningsRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response.Data);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult> GetTuningAsync([FromQuery] GetTuningRequest request)
        {
            var response = await mediator.Send(request);
            if (response.Data == null)
                return NotFound();
            return Ok(response.Data);
        }
    }
}

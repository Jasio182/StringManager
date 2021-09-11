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
    public class TonesController : ControllerBase
    {
        private readonly IMediator mediator;

        public TonesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetTonesAsync([FromQuery] GetTonesRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response.Data);
        }
    }
}

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
    public class TonesController : ApiControllerBase
    {
        public TonesController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public Task<IActionResult> GetTonesAsync([FromQuery] GetTonesRequest request)
        {
            return HandleResult<GetTonesRequest, GetTonesResponse>(request);
        }
    }
}

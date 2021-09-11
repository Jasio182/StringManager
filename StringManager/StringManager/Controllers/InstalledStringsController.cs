using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain.Requests;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InstalledStringsController : ControllerBase
    {
        private readonly IMediator mediator;

        public InstalledStringsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> AddInstalledStringAsync([FromBody] AddInstalledStringRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response.Data);
        }

        [HttpPut]
        public async Task<ActionResult> ModifyInstalledStringAsync([FromBody] ModifyInstalledStringRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response.Data);
        }
    }
}

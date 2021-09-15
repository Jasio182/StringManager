using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InstalledStringsController : ApiControllerBase
    {
        public InstalledStringsController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public Task<IActionResult> AddInstalledStringAsync([FromBody] AddInstalledStringRequest request)
        {
            return HandleResult<AddInstalledStringRequest, AddInstalledStringResponse>(request);
        }

        [HttpPut]
        public Task<IActionResult> ModifyInstalledStringAsync([FromBody] ModifyInstalledStringRequest request)
        {
            return HandleResult<ModifyInstalledStringRequest, ModifyInstalledStringResponse>(request);
        }
    }
}

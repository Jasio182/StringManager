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
    public class InstalledStringsController : ApiControllerBase<InstalledStringsController>
    {
        public InstalledStringsController(IMediator mediator, ILogger<InstalledStringsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("InstalledStringsController started");
        }

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

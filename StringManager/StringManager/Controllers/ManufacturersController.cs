using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ManufacturersController : ApiControllerBase<ManufacturersController>
    {
        public ManufacturersController(IMediator mediator, ILogger<ManufacturersController> logger) : base(mediator, logger)
        {
            logger.LogInformation("ManufacturersController started");
        }

        [HttpGet]
        [Route("instruments")]
        public Task<IActionResult> GetInstrumentsManufacturersAsync()
        {
            var request = new GetInstrumentsManufacturersRequest();
            return HandleResult<GetInstrumentsManufacturersRequest, StatusCodeResponse>(request);
        }

        [HttpGet]
        [Route("strings")]
        public Task<IActionResult> GetStringsManufacturersAsync()
        {
            var request = new GetStringsManufacturersRequest();
            return HandleResult<GetStringsManufacturersRequest, StatusCodeResponse>(request);
        }

        [HttpPost]
        public Task<IActionResult> AddManufacturerAsync([FromBody] AddManufacturerRequest request)
        {
            return HandleResult<AddManufacturerRequest, StatusCodeResponse>(request);
        }
    }
}

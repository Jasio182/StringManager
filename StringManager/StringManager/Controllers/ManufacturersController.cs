using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
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
        public Task<IActionResult> GetInstrumentsManufacturersAsync([FromQuery] GetInstrumentsManufacturersRequest request)
        {
            return HandleResult<GetInstrumentsManufacturersRequest, GetInstrumentsManufacturersResponse>(request);
        }

        [HttpGet]
        [Route("strings")]
        public Task<IActionResult> GetStringsManufacturersAsync([FromQuery] GetStringsManufacturersRequest request)
        {
            return HandleResult<GetStringsManufacturersRequest, GetStringsManufacturersResponse>(request);
        }

        [HttpPost]
        public Task<IActionResult> AddManufacturerAsync([FromBody] AddManufacturerRequest request)
        {
            return HandleResult<AddManufacturerRequest, AddManufacturerResponse>(request);
        }
    }
}

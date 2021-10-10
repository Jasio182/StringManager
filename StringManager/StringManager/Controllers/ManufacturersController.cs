using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Gets list of Manufacturer items that have relation with Instrument item 
        /// </summary>
        /// <returns>A list of Manufacturer items</returns>
        /// <response code="200">Gets a list of Manufacturer items</response>
        /// <response code="401">User is not authorized to get Manufacturer item</response> 
        /// <response code="500">An exception has been thrown during getting a list of Manufacturer item</response> 
        [HttpGet]
        [Route("instruments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetInstrumentsManufacturersAsync()
        {
            var request = new GetInstrumentsManufacturersRequest();
            return HandleResult<GetInstrumentsManufacturersRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Gets list of Manufacturer items that have relation with String item
        /// </summary>
        /// <returns>A list of Manufacturer items</returns>
        /// <response code="200">Gets a list of Manufacturer items</response>
        /// <response code="401">User is not authorized to get Manufacturer item</response> 
        /// <response code="500">An exception has been thrown during getting a list of Manufacturer items</response> 
        [HttpGet]
        [Route("strings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetStringsManufacturersAsync()
        {
            var request = new GetStringsManufacturersRequest();
            return HandleResult<GetStringsManufacturersRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Adds Manufacturer item
        /// </summary>
        /// <returns>A newly created Manufacturer item</returns>
        /// <response code="200">Successfuly added Manufacturer item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to add Manufacturer item</response> 
        /// <response code="500">An exception has been thrown during adding a specific Manufacturer item</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AddManufacturerAsync([FromBody] AddManufacturerRequest request)
        {
            return HandleResult<AddManufacturerRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Updates specific Manufacturer item
        /// </summary>
        /// <response code="204">Successfuly modified Manufacturer item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to modify Manufacturer item</response> 
        /// <response code="404">The specific Manufacturer item has not been found</response>
        /// <response code="500">An exception has been thrown during modification of specific Manufacturer item</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> ModifyManufacturerAsync([FromBody] ModifyManufacturerRequest request)
        {
            return HandleResult<ModifyManufacturerRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Deletes specific Manufacturer item
        /// </summary>
        /// <param name="request">Id of Manufacturer item to delete</param>
        /// <response code="204">Successfuly deleted Manufacturer item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to delete Manufacturer item</response> 
        /// <response code="404">The specific Manufacturer item has not been found</response>
        /// <response code="500">An exception has been thrown during deletion of specific Manufacturer item</response> 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RemoveManufacturerAsync([FromQuery] RemoveManufacturerRequest request)
        {
            return HandleResult<RemoveManufacturerRequest, StatusCodeResponse>(request);
        }
    }
}

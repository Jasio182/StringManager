using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Core.Models;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Collections.Generic;
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
        [ProducesResponseType(typeof(ModelResult<List<Manufacturer>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<List<Manufacturer>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<List<Manufacturer>>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetInstrumentsManufacturersAsync()
        {
            var request = new GetInstrumentsManufacturersRequest();
            return HandleResult<GetInstrumentsManufacturersRequest, StatusCodeResponse<List<Manufacturer>>, List<Manufacturer>>(request);
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
        [ProducesResponseType(typeof(ModelResult<List<Manufacturer>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<List<Manufacturer>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<List<Manufacturer>>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetStringsManufacturersAsync()
        {
            var request = new GetStringsManufacturersRequest();
            return HandleResult<GetStringsManufacturersRequest, StatusCodeResponse<List<Manufacturer>>, List<Manufacturer>>(request);
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
        [ProducesResponseType(typeof(ModelResult<Manufacturer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<Manufacturer>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<Manufacturer>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<Manufacturer>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AddManufacturerAsync([FromBody] AddManufacturerRequest request)
        {
            return HandleResult<AddManufacturerRequest, StatusCodeResponse<Manufacturer>, Manufacturer>(request);
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
        [ProducesResponseType(typeof(ModelResult<Manufacturer>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<Manufacturer>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<Manufacturer>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ModelResult<Manufacturer>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> ModifyManufacturerAsync([FromBody] ModifyManufacturerRequest request)
        {
            return HandleResult<ModifyManufacturerRequest, StatusCodeResponse<Manufacturer>, Manufacturer>(request);
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
        [ProducesResponseType(typeof(ModelResult<Manufacturer>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<Manufacturer>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<Manufacturer>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ModelResult<Manufacturer>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RemoveManufacturerAsync([FromQuery] RemoveManufacturerRequest request)
        {
            return HandleResult<RemoveManufacturerRequest, StatusCodeResponse<Manufacturer>, Manufacturer>(request);
        }
    }
}

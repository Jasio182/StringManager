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
    public class MyInstrumentsController : ApiControllerBase<MyInstrumentsController>
    {
        public MyInstrumentsController(IMediator mediator, ILogger<MyInstrumentsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("MyInstrumentsController started");
        }

        /// <summary>
        /// Gets list of MyInstrument items
        /// </summary>
        /// <returns>A list of MyInstrument items</returns>
        /// <response code="200">Gets a list of MyInstrument items</response>
        /// <response code="401">User is not authorized to get MyInstrument item</response> 
        /// <response code="500">An exception has been thrown during getting a specific MyInstrument item</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetMyInstrumentsAsync([FromQuery] GetMyInstrumentsRequest request)
        {
            return HandleResult<GetMyInstrumentsRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Gets MyInstrument item
        /// </summary>
        /// <param name="request">Id of MyInstrument item to get</param>
        /// <returns>A MyInstrument item</returns>
        /// <response code="200">Gets a list of MyInstrument items</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to add MyInstrument item</response> 
        /// <response code="500">An exception has been thrown during getting a list of MyInstrument item</response> 
        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetMyInstrumentAsync([FromQuery] GetMyInstrumentRequest request)
        {
            return HandleResult<GetMyInstrumentRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Adds MyInstrument item
        /// </summary>
        /// <returns>A newly created MyInstrument item</returns>
        /// <response code="200">Successfuly added MyInstrument item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to add MyInstrument item</response> 
        /// <response code="500">An exception has been thrown during adding a specific MyInstrument item</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AddMyInstrumentAsync([FromBody] AddMyInstrumentRequest request)
        {
            return HandleResult<AddMyInstrumentRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Updates specific MyInstrument item
        /// </summary>
        /// <response code="204">Successfuly modified MyInstrument item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to modify MyInstrument item</response> 
        /// <response code="404">The specific MyInstrument item has not been found</response>
        /// <response code="500">An exception has been thrown during modification of specific MyInstrument item</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> ModifyMyInstrumentAsync([FromBody] ModifyMyInstrumentRequest request)
        {
            return HandleResult<ModifyMyInstrumentRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Deletes specific MyInstrument item
        /// </summary>
        /// <param name="request">Id of MyInstrument item to delete</param>
        /// <response code="204">Successfuly deleted MyInstrument item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to delete MyInstrument item</response> 
        /// <response code="404">The specific MyInstrument item has not been found</response>
        /// <response code="500">An exception has been thrown during deletion of specific MyInstrument item</response> 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RemoveMyInstrumentAsync([FromQuery] RemoveMyInstrumentRequest request)
        {
            return HandleResult<RemoveMyInstrumentRequest, StatusCodeResponse>(request);
        }
    }
}

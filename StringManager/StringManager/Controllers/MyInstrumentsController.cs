using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Core.MediatorRequestsAndResponses;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;
using System.Collections.Generic;
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
        [ProducesResponseType(typeof(ModelResult<List<MyInstrumentList>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<List<MyInstrumentList>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<List<MyInstrumentList>>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetMyInstrumentsAsync([FromQuery] int? requestUserId)
        {
            var request = new GetMyInstrumentsRequest() { RequestUserId = requestUserId };
            return HandleResult<GetMyInstrumentsRequest, StatusCodeResponse<List<MyInstrumentList>>, List<MyInstrumentList>> (request);
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
        [ProducesResponseType(typeof(ModelResult<MyInstrument>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<MyInstrument>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<MyInstrument>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<MyInstrument>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetMyInstrumentAsync([FromQuery] GetMyInstrumentRequest request)
        {
            return HandleResult<GetMyInstrumentRequest, StatusCodeResponse<MyInstrument>, MyInstrument>(request);
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
        [ProducesResponseType(typeof(ModelResult<MyInstrument>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<MyInstrument>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<MyInstrument>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<MyInstrument>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AddMyInstrumentAsync([FromBody] AddMyInstrumentRequest request)
        {
            return HandleResult<AddMyInstrumentRequest, StatusCodeResponse<MyInstrument>, MyInstrument>(request);
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
        [ProducesResponseType(typeof(ModelResult<MyInstrument>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<MyInstrument>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<MyInstrument>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ModelResult<MyInstrument>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> ModifyMyInstrumentAsync([FromBody] ModifyMyInstrumentRequest request)
        {
            return HandleResult<ModifyMyInstrumentRequest, StatusCodeResponse<MyInstrument>, MyInstrument>(request);
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
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ModelResult<MyInstrument>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<MyInstrument>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<MyInstrument>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ModelResult<MyInstrument>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RemoveMyInstrumentAsync([FromQuery] RemoveMyInstrumentRequest request)
        {
            return HandleResult<RemoveMyInstrumentRequest, StatusCodeResponse<MyInstrument>, MyInstrument > (request);
        }
    }
}

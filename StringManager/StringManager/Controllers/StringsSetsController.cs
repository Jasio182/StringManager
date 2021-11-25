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
    public class StringsSetsController : ApiControllerBase<StringsSetsController>
    {
        public StringsSetsController(IMediator mediator, ILogger<StringsSetsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("StringSetsController started");
        }

        /// <summary>
        /// Gets list of StringsSet items
        /// </summary>
        /// <returns>A list of StringsSet items</returns>
        /// <response code="200">Gets a list of StringsSet items</response>
        /// <response code="401">User is not authorized to get list of StringsSet items</response> 
        /// <response code="500">An exception has been thrown during getting a list of StringsSet item</response> 
        [HttpGet]
        [ProducesResponseType(typeof(StatusCodeResponse<List<StringsSet>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse<List<StringsSet>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse<List<StringsSet>>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetStringsSetsAsync([FromQuery] GetStringsSetsRequest request)
        {
            return HandleResult<GetStringsSetsRequest, StatusCodeResponse<List<StringsSet>>, List<StringsSet>>(request);
        }

        /// <summary>
        /// Gets StringsSet item
        /// </summary>
        /// <param name="request">Id of StringsSet item to get</param>
        /// <returns>A StringsSet item</returns>
        /// <response code="200">Gets a list of StringsSet items</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to get StringsSet item</response> 
        /// <response code="500">An exception has been thrown during getting a specific StringsSet item</response> 
        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(typeof(StatusCodeResponse<StringsSet>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse<StringsSet>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse<StringsSet>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse<StringsSet>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetStringsSetAsync([FromQuery] GetStringsSetRequest request)
        {
            return HandleResult<GetStringsSetRequest, StatusCodeResponse<StringsSet>, StringsSet> (request);
        }

        /// <summary>
        /// Adds StringsSet item
        /// </summary>
        /// <returns>A newly created StringsSet item</returns>
        /// <response code="200">Successfuly added StringsSet item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to add StringsSet item</response> 
        /// <response code="500">An exception has been thrown during adding a specific StringsSet item</response> 
        [HttpPost]
        [ProducesResponseType(typeof(StatusCodeResponse<StringsSet>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse<StringsSet>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse<StringsSet>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse<StringsSet>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AddStringsSetAsync([FromBody] AddStringsSetRequest request)
        {
            return HandleResult<AddStringsSetRequest, StatusCodeResponse<StringsSet>, StringsSet>(request);
        }

        /// <summary>
        /// Updates specific StringsSet item
        /// </summary>
        /// <response code="204">Successfuly modified StringsSet item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to modify StringsSet item</response> 
        /// <response code="404">The specific StringsSet item has not been found</response>
        /// <response code="500">An exception has been thrown during modification of specific StringsSet item</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(StatusCodeResponse<StringsSet>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse<StringsSet>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse<StringsSet>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(StatusCodeResponse<StringsSet>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> ModifyStringsSetAsync([FromBody] ModifyStringsSetRequest request)
        {
            return HandleResult<ModifyStringsSetRequest, StatusCodeResponse<StringsSet>, StringsSet>(request);
        }

        /// <summary>
        /// Deletes specific StringsSet item
        /// </summary>
        /// <param name="request">Id of StringsSet item to delete</param>
        /// <response code="204">Successfuly deleted StringsSet item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to delete StringsSet item</response> 
        /// <response code="404">The specific StringsSet item has not been found</response>
        /// <response code="500">An exception has been thrown during deletion of specific StringsSet item</response> 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(StatusCodeResponse<StringsSet>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse<StringsSet>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse<StringsSet>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(StatusCodeResponse<StringsSet>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RemoveStringsSetAsync([FromQuery] RemoveStringsSetRequest request)
        {
            return HandleResult<RemoveStringsSetRequest, StatusCodeResponse<StringsSet>, StringsSet>(request);
        }
    }
}

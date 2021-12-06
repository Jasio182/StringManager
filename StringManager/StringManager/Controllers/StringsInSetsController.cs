using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Core.Models;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class StringsInSetsController : ApiControllerBase<StringsInSetsController>
    {
        public StringsInSetsController(IMediator mediator, ILogger<StringsInSetsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("StringsInSetsController started");
        }

        /// <summary>
        /// Adds StringInSet item
        /// </summary>
        /// <returns>A newly created StringInSet item</returns>
        /// <response code="200">Successfuly added StringInSet item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to add StringInSet item</response> 
        /// <response code="500">An exception has been thrown during adding of specific StringInSet item</response> 
        [HttpPost]
        [ProducesResponseType(typeof(ModelResult<StringInSet>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<StringInSet>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<StringInSet>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<StringInSet>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AddStringInSetAsync([FromBody] AddStringInSetRequest request)
        {
            return HandleResult<AddStringInSetRequest, StatusCodeResponse<StringInSet>, StringInSet>(request);
        }

        /// <summary>
        /// Updates specific StringInSet item
        /// </summary>
        /// <response code="204">Successfuly modified StringInSet item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to modify StringInSet item</response> 
        /// <response code="404">The specific StringInSet item has not been found</response>
        /// <response code="500">An exception has been thrown during modification of specific StringInSet item</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ModelResult<StringInSet>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<StringInSet>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<StringInSet>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ModelResult<StringInSet>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> ModifyStringInSetAsync([FromBody] ModifyStringInSetRequest request)
        {
            return HandleResult<ModifyStringInSetRequest, StatusCodeResponse<StringInSet>, StringInSet>(request);
        }

        /// <summary>
        /// Deletes specific StringInSet item
        /// </summary>
        /// <param name="request">Id of StringInSet item to delete</param>
        /// <response code="204">Successfuly deleted StringInSet item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to delete StringInSet item</response> 
        /// <response code="404">The specific StringInSet item has not been found</response>
        /// <response code="500">An exception has been thrown during deletion of specific StringInSet item</response> 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ModelResult<StringInSet>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<StringInSet>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<StringInSet>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ModelResult<StringInSet>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RemoveStringInSetAsync([FromQuery] RemoveStringInSetRequest request)
        {
            return HandleResult<RemoveStringInSetRequest, StatusCodeResponse<StringInSet>, StringInSet>(request);
        }
    }
}

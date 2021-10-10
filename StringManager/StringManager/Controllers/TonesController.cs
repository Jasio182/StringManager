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
    [ApiController]
    [Route("[controller]")]
    public class TonesController : ApiControllerBase<TonesController>
    {
        public TonesController(IMediator mediator, ILogger<TonesController> logger) : base(mediator, logger)
        {
            logger.LogInformation("TonesController started");
        }

        /// <summary>
        /// Gets list of Tone items
        /// </summary>
        /// <returns>A list of Tone items</returns>
        /// <response code="200">Gets a list of Tone items</response>
        /// <response code="500">An exception has been thrown during getting a list of Tone item</response> 
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetTonesAsync()
        {
            var request = new GetTonesRequest();
            return HandleResult<GetTonesRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Adds Tone item
        /// </summary>
        /// <returns>A newly created Tone item</returns>
        /// <response code="200">Successfuly added Tone item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to add Tone item</response> 
        /// <response code="500">An exception has been thrown during adding a specific Tone item</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AddToneAsync([FromBody] AddToneRequest request)
        {
            return HandleResult<AddToneRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Updates specific Tone item
        /// </summary>
        /// <response code="204">Successfuly modified Tone item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to modify Tone item</response> 
        /// <response code="404">The specific Tone item has not been found</response>
        /// <response code="500">An exception has been thrown during modification of specific Tone item</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> ModifyToneAsync([FromBody] ModifyToneRequest request)
        {
            return HandleResult<ModifyToneRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Deletes specific Tone item
        /// </summary>
        /// <param name="request">Id of Tone item to delete</param>
        /// <response code="204">Successfuly deleted Tone item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to delete Tone item</response> 
        /// <response code="404">The specific Tone item has not been found</response>
        /// <response code="500">An exception has been thrown during deletion of specific Tone item</response> 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RemoveToneAsync([FromQuery] RemoveToneRequest request)
        {
            return HandleResult<RemoveToneRequest, StatusCodeResponse>(request);
        }
    }
}

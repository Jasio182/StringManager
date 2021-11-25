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
    public class TonesInTuningsController : ApiControllerBase<TonesInTuningsController>
    {
        public TonesInTuningsController(IMediator mediator, ILogger<TonesInTuningsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("TonesInTuningsController started");
        }

        /// <summary>
        /// Adds ToneInTuning item
        /// </summary>
        /// <returns>A newly created ToneInTuning item</returns>
        /// <response code="200">Successfuly added ToneInTuning item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to add ToneInTuning item</response> 
        /// <response code="500">An exception has been thrown during adding a specific ToneInTuning item</response> 
        [HttpPost]
        [ProducesResponseType(typeof(StatusCodeResponse<ToneInTuning>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse<ToneInTuning>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse<ToneInTuning>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse<ToneInTuning>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AddToneInTuningAsync([FromBody] AddToneInTuningRequest request)
        {
            return HandleResult<AddToneInTuningRequest, StatusCodeResponse<ToneInTuning>, ToneInTuning>(request);
        }

        /// <summary>
        /// Updates specific ToneInTuning item
        /// </summary>
        /// <response code="204">Successfuly modified ToneInTuning item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to modify ToneInTuning item</response> 
        /// <response code="404">The specific ToneInTuning item has not been found</response>
        /// <response code="500">An exception has been thrown during modification of specific ToneInTuning item</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(StatusCodeResponse<ToneInTuning>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse<ToneInTuning>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse<ToneInTuning>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(StatusCodeResponse<ToneInTuning>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> ModifyToneInTuningAsync([FromBody] ModifyToneInTuningRequest request)
        {
            return HandleResult<ModifyToneInTuningRequest, StatusCodeResponse<ToneInTuning>, ToneInTuning>(request);
        }

        /// <summary>
        /// Deletes specific ToneInTuning item
        /// </summary>
        /// <param name="request">Id of ToneInTuning item to delete</param>
        /// <response code="204">Successfuly deleted ToneInTuning item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to delete ToneInTuning item</response> 
        /// <response code="404">The specific ToneInTuning item has not been found</response>
        /// <response code="500">An exception has been thrown during deletion of specific ToneInTuning item</response> 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(StatusCodeResponse<ToneInTuning>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse<ToneInTuning>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse<ToneInTuning>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(StatusCodeResponse<ToneInTuning>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RemoveToneInTuningAsync([FromQuery] RemoveToneInTuningRequest request)
        {
            return HandleResult<RemoveToneInTuningRequest, StatusCodeResponse<ToneInTuning>, ToneInTuning>(request);
        }
    }
}

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
    [ApiController]
    [Route("[controller]")]
    public class TuningsController : ApiControllerBase<TuningsController>
    {
        public TuningsController(IMediator mediator, ILogger<TuningsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("TuningsController started");
        }

        /// <summary>
        /// Gets list of Tuning items
        /// </summary>
        /// <returns>A list of Tuning items</returns>
        /// <response code="200">Gets a list of Tuning items</response>
        /// <response code="500">An exception has been thrown during getting a list of specific Tuning item</response> 
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ModelResult<List<TuningList>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<List<TuningList>>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetTuningsAsync()
        {
            var request = new GetTuningsRequest();
            return HandleResult<GetTuningsRequest, StatusCodeResponse<List<TuningList>>, List<TuningList>>(request);
        }

        /// <summary>
        /// Gets Tuning item
        /// </summary>
        /// <param name="request">Id of Tuning item to get</param>
        /// <returns>A Tuning item</returns>
        /// <response code="200">Gets a list of Tuning items</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="500">An exception has been thrown during getting a specific Tuning item</response> 
        [AllowAnonymous]
        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(typeof(ModelResult<Tuning>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<Tuning>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<Tuning>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetTuningAsync([FromQuery] GetTuningRequest request)
        {
            return HandleResult<GetTuningRequest, StatusCodeResponse<Tuning>, Tuning>(request);
        }

        /// <summary>
        /// Adds Tuning item
        /// </summary>
        /// <returns>A newly created Tuning item</returns>
        /// <response code="200">Successfuly added Tuning item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to add Tuning item</response> 
        /// <response code="500">An exception has been thrown during modification of specific Tuning item</response> 
        [HttpPost]
        [ProducesResponseType(typeof(ModelResult<Tuning>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<Tuning>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<Tuning>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<Tuning>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AddTuningAsync([FromBody] AddTuningRequest request)
        {
            return HandleResult<AddTuningRequest, StatusCodeResponse<Tuning>, Tuning>(request);
        }

        /// <summary>
        /// Updates specific Tuning item
        /// </summary>
        /// <response code="204">Successfuly modified Tuning item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to modify Tuning item</response> 
        /// <response code="404">The specific Tuning item has not been found</response>
        /// <response code="500">An exception has been thrown during modification of specific Tuning item</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ModelResult<Tuning>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<Tuning>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<Tuning>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ModelResult<Tuning>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> ModifyTuningAsync([FromBody] ModifyTuningRequest request)
        {
            return HandleResult<ModifyTuningRequest, StatusCodeResponse<Tuning>, Tuning>(request);
        }

        /// <summary>
        /// Deletes specific Tuning item
        /// </summary>
        /// <param name="request">Id of Tuning item to delete</param>
        /// <response code="204">Successfuly deleted Tuning item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to delete Tuning item</response> 
        /// <response code="404">The specific Tuning item has not been found</response>
        /// <response code="500">An exception has been thrown during deletion of specific Tuning item</response> 
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ModelResult<Tuning>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<Tuning>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<Tuning>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ModelResult<Tuning>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RemoveTuningAsync([FromQuery] RemoveTuningRequest request)
        {
            return HandleResult<RemoveTuningRequest, StatusCodeResponse<Tuning>, Tuning> (request);
        }
    }
}

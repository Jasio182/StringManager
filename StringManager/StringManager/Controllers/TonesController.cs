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
        [ProducesResponseType(typeof(ModelResult<List<Tone>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<List<Tone>>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetTonesAsync()
        {
            var request = new GetTonesRequest();
            return HandleResult<GetTonesRequest, StatusCodeResponse<List<Tone>>, List<Tone>>(request);
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
        [ProducesResponseType(typeof(ModelResult<Tone>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<Tone>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<Tone>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<Tone>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AddToneAsync([FromBody] AddToneRequest request)
        {
            return HandleResult<AddToneRequest, StatusCodeResponse<Tone>, Tone>(request);
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
        [ProducesResponseType(typeof(ModelResult<Tone>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<Tone>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<Tone>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ModelResult<Tone>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> ModifyToneAsync([FromBody] ModifyToneRequest request)
        {
            return HandleResult<ModifyToneRequest, StatusCodeResponse<Tone>, Tone>(request);
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
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ModelResult<Tone>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<Tone>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<Tone>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ModelResult<Tone>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RemoveToneAsync([FromQuery] RemoveToneRequest request)
        {
            return HandleResult<RemoveToneRequest, StatusCodeResponse<Tone>, Tone>(request);
        }
    }
}

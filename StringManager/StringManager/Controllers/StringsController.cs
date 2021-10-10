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
    public class StringsController : ApiControllerBase<StringsController>
    {
        public StringsController(IMediator mediator, ILogger<StringsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("StringsController started");
        }

        /// <summary>
        /// Gets list of String items
        /// </summary>
        /// <returns>A list of String items</returns>
        /// <response code="200">Gets a list of String items</response>
        /// <response code="500">An exception has been thrown during getting a list of String item</response> 
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetStringsAsync()
        {
            var request = new GetStringsRequest();
            return HandleResult<GetStringsRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Adds String item
        /// </summary>
        /// <returns>A newly created String item</returns>
        /// <response code="200">Successfuly added String item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to add String item</response> 
        /// <response code="500">An exception has been thrown during adding a specific String item</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AddStringAsync([FromBody] AddStringRequest request)
        {
            return HandleResult<AddStringRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Updates specific String item
        /// </summary>
        /// <response code="204">Successfuly modified String item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to modify String item</response> 
        /// <response code="404">The specific String item has not been found</response>
        /// <response code="500">An exception has been thrown during modification of specific String item</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> ModifyStringAsync([FromBody] ModifyStringRequest request)
        {
            return HandleResult<ModifyStringRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Deletes specific String item
        /// </summary>
        /// <param name="request">Id of String item to delete</param>
        /// <response code="204">Successfuly deleted String item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to delete String item</response> 
        /// <response code="404">The specific String item has not been found</response>
        /// <response code="500">An exception has been thrown during deletion of specific String item</response> 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RemoveStringAsync([FromQuery] RemoveStringRequest request)
        {
            return HandleResult<RemoveStringRequest, StatusCodeResponse>(request);
        }
    }
}

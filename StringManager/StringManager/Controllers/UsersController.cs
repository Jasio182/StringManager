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
    public class UsersController : ApiControllerBase<UsersController>
    {
        public UsersController(IMediator mediator, ILogger<UsersController> logger) : base(mediator, logger)
        {
            logger.LogInformation("UsersController started");
        }

        /// <summary>
        /// Gets list of User items
        /// </summary>
        /// <returns>A list of User items</returns>
        /// <response code="200">Gets a list of User items</response>
        /// <response code="401">User is not authorized to add InstalledString item</response> 
        /// <response code="500">An exception has been thrown during modification of specific InstalledString item</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetUsersAsync([FromQuery] GetUsersRequest request)
        {
            return HandleResult<GetUsersRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Gets User item
        /// </summary>
        /// <returns>A User item</returns>
        /// <response code="200">Gets a list of User items</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to add InstalledString item</response> 
        /// <response code="500">An exception has been thrown during modification of specific InstalledString item</response> 
        [HttpGet]
        [Route("single")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetUserAsync()
        {
            var request = new GetUserRequest();
            return HandleResult<GetUserRequest, StatusCodeResponse>(request);
        }


        /// <summary>
        /// Adds User item
        /// </summary>
        /// <returns>A newly created User item</returns>
        /// <response code="200">Successfuly added User item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to add User item</response> 
        /// <response code="500">An exception has been thrown during modification of specific User item</response> 
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AddUserAsync([FromBody] AddUserRequest request)
        {
            return HandleResult<AddUserRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Updates specific User item
        /// </summary>
        /// <response code="204">Successfuly modified User item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to modify User item</response> 
        /// <response code="404">The specific User item has not been found</response>
        /// <response code="500">An exception has been thrown during modification of specific InstalledString item</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> ModifyUserAsync([FromBody] ModifyUserRequest request)
        {
            return HandleResult<ModifyUserRequest, StatusCodeResponse>(request);
        }
    }
}

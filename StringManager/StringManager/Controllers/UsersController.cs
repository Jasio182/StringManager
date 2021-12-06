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
        /// <response code="401">User is not authorized to get User item</response> 
        /// <response code="500">An exception has been thrown during getting a list of User items</response> 
        [HttpGet]
        [ProducesResponseType(typeof(ModelResult<List<User>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<List<User>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<List<User>>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetUsersAsync([FromQuery] GetUsersRequest request)
        {
            return HandleResult<GetUsersRequest, StatusCodeResponse<List<User>>, List<User>> (request);
        }

        /// <summary>
        /// Gets User item
        /// </summary>
        /// <returns>A User item</returns>
        /// <response code="200">Gets a list of User items</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to get User item</response> 
        /// <response code="500">An exception has been thrown during getting a specific User item</response> 
        [HttpGet]
        [Route("single")]
        [ProducesResponseType(typeof(ModelResult<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<User>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<User>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<User>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetUserAsync()
        {
            var request = new GetUserRequest();
            return HandleResult<GetUserRequest, StatusCodeResponse<User>, User>(request);
        }


        /// <summary>
        /// Adds User item
        /// </summary>
        /// <returns>A newly created User item</returns>
        /// <response code="200">Successfuly added User item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to add User item</response> 
        /// <response code="500">An exception has been thrown during adding a specific User item</response> 
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(ModelResult<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<User>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<User>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<User>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AddUserAsync([FromBody] AddUserRequest request)
        {
            return HandleResult<AddUserRequest, StatusCodeResponse<User>, User>(request);
        }

        /// <summary>
        /// Updates specific User item
        /// </summary>
        /// <response code="204">Successfuly modified User item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to modify User item</response> 
        /// <response code="404">The specific User item has not been found</response>
        /// <response code="500">An exception has been thrown during modification of specific User item</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ModelResult<User>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<User>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<User>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ModelResult<User>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> ModifyUserAsync([FromBody] ModifyUserRequest request)
        {
            return HandleResult<ModifyUserRequest, StatusCodeResponse<User>, User>(request);
        }
    }
}

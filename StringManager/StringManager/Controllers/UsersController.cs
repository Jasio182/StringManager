using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public Task<IActionResult> GetUsersAsync([FromQuery] GetUsersRequest request)
        {
            return HandleResult<GetUsersRequest, StatusCodeResponse>(request);
        }

        [HttpGet]
        [Route("single")]
        public Task<IActionResult> GetUserAsync()
        {
            var request = new GetUserRequest();
            return HandleResult<GetUserRequest, StatusCodeResponse>(request);
        }

        [AllowAnonymous]
        [HttpPost]
        public Task<IActionResult> AddUserAsync([FromBody] AddUserRequest request)
        {
            return HandleResult<AddUserRequest, StatusCodeResponse>(request);
        }

        [HttpPut]
        public Task<IActionResult> ModifyUserAsync([FromBody] ModifyUserRequest request)
        {
            return HandleResult<ModifyUserRequest, StatusCodeResponse>(request);
        }
    }
}

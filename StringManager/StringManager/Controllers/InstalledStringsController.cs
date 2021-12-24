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
    public class InstalledStringsController : ApiControllerBase<InstalledStringsController>
    {
        public InstalledStringsController(IMediator mediator, ILogger<InstalledStringsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("InstalledStringsController started");
        }

        /// <summary>
        /// Adds InstalledString item
        /// </summary>
        /// <returns>A newly created InstalledString item</returns>
        /// <response code="200">Successfuly added InstalledString item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to add InstalledString item</response> 
        /// <response code="500">An exception has been thrown during modification of specific InstalledString item</response> 
        [HttpPost]
        [ProducesResponseType(typeof(ModelResult<InstalledString>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<InstalledString>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<InstalledString>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<InstalledString>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AddInstalledStringAsync([FromBody] AddInstalledStringRequest request)
        {
            return HandleResult<AddInstalledStringRequest, StatusCodeResponse<InstalledString>, InstalledString>(request);
        }

        /// <summary>
        /// Updates specific InstalledString item
        /// </summary>
        /// <response code="204">Successfuly modified InstalledString item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to modify InstalledString item</response> 
        /// <response code="404">The specific InstalledString item has not been found</response>
        /// <response code="500">An exception has been thrown during modification of specific InstalledString item</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ModelResult<InstalledString>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<InstalledString>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<InstalledString>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ModelResult<InstalledString>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> ModifyInstalledStringAsync([FromBody] ModifyInstalledStringRequest request)
        {
            return HandleResult<ModifyInstalledStringRequest, StatusCodeResponse<InstalledString>, InstalledString>(request);
        }

        /// <summary>
        /// Deletes specific InstalledString item
        /// </summary>
        /// <param name="request">Id of InstalledString item to delete</param>
        /// <response code="204">Successfuly deleted InstalledString item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to delete InstalledString item</response> 
        /// <response code="404">The specific InstalledString item has not been found</response>
        /// <response code="500">An exception has been thrown during deletion of specific InstalledString item</response> 
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ModelResult<InstalledString>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<InstalledString>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<InstalledString>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ModelResult<InstalledString>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RemoveInstalledStringAsync([FromQuery] RemoveInstalledStringRequest request)
        {
            return HandleResult<RemoveInstalledStringRequest, StatusCodeResponse<InstalledString>, InstalledString>(request);
        }
    }
}

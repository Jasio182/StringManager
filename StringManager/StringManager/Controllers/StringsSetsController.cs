﻿using MediatR;
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
    public class StringsSetsController : ApiControllerBase<StringsSetsController>
    {
        public StringsSetsController(IMediator mediator, ILogger<StringsSetsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("StringSetsController started");
        }

        /// <summary>
        /// Gets list of StringsSet items
        /// </summary>
        /// <returns>A list of StringsSet items</returns>
        /// <response code="200">Gets a list of StringsSet items</response>
        /// <response code="401">User is not authorized to add InstalledString item</response> 
        /// <response code="500">An exception has been thrown during modification of specific InstalledString item</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetStringsSetsAsync([FromQuery] GetStringsSetsRequest request)
        {
            return HandleResult<GetStringsSetsRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Gets StringsSet item
        /// </summary>
        /// <param name="request">Id of StringsSet item to get</param>
        /// <returns>A StringsSet item</returns>
        /// <response code="200">Gets a list of StringsSet items</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to add InstalledString item</response> 
        /// <response code="500">An exception has been thrown during modification of specific InstalledString item</response> 
        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetStringsSetAsync([FromQuery] GetStringsSetRequest request)
        {
            return HandleResult<GetStringsSetRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Adds StringsSet item
        /// </summary>
        /// <returns>A newly created StringsSet item</returns>
        /// <response code="200">Successfuly added StringsSet item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to add StringsSet item</response> 
        /// <response code="500">An exception has been thrown during modification of specific StringsSet item</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AddStringsSetAsync([FromBody] AddStringsSetRequest request)
        {
            return HandleResult<AddStringsSetRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Updates specific StringsSet item
        /// </summary>
        /// <response code="204">Successfuly modified StringsSet item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to modify StringsSet item</response> 
        /// <response code="404">The specific StringsSet item has not been found</response>
        /// <response code="500">An exception has been thrown during modification of specific InstalledString item</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> ModifyStringsSetAsync([FromBody] ModifyStringsSetRequest request)
        {
            return HandleResult<ModifyStringsSetRequest, StatusCodeResponse>(request);
        }

        /// <summary>
        /// Deletes specific StringsSet item
        /// </summary>
        /// <param name="request">Id of StringsSet item to delete</param>
        /// <response code="204">Successfuly deleted StringsSet item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to delete StringsSet item</response> 
        /// <response code="404">The specific StringsSet item has not been found</response>
        /// <response code="500">An exception has been thrown during deletion of specific StringsSet item</response> 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RemoveStringsSetAsync([FromQuery] RemoveStringsSetRequest request)
        {
            return HandleResult<RemoveStringsSetRequest, StatusCodeResponse>(request);
        }
    }
}
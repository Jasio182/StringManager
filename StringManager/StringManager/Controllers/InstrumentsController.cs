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
    public class InstrumentsController : ApiControllerBase<InstrumentsController>
    {
        public InstrumentsController(IMediator mediator, ILogger<InstrumentsController> logger) : base(mediator, logger)
        {
            logger.LogInformation("InstrumentsController started");
        }

        /// <summary>
        /// Gets list of Instrument items
        /// </summary>
        /// <returns>A list of Instrument items</returns>
        /// <response code="200">Gets a list of Instrument items</response>
        /// <response code="401">User is not authorized to get Instrument item</response> 
        /// <response code="500">An exception has been thrown during getting a specific Instrument item</response> 
        [HttpGet]
        [ProducesResponseType(typeof(ModelResult<List<Instrument>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<List<Instrument>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<List<Instrument>>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetInstrumentsAsync()
        {
            var request = new GetInstrumentsRequest();
            return HandleResult<GetInstrumentsRequest, StatusCodeResponse<List<Instrument>>, List<Instrument>>(request);
        }

        /// <summary>
        /// Adds Instrument item
        /// </summary>
        /// <returns>A newly created Instrument item</returns>
        /// <response code="200">Successfuly added Instrument item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to add Instrument item</response> 
        /// <response code="500">An exception has been thrown during adding a specific Instrument item</response> 
        [HttpPost]
        [ProducesResponseType(typeof(ModelResult<Instrument>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelResult<Instrument>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<Instrument>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<Instrument>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AddInstrumentAsync([FromBody] AddInstrumentRequest request)
        {
            return HandleResult<AddInstrumentRequest, StatusCodeResponse<Instrument>, Instrument>(request);
        }

        /// <summary>
        /// Updates specific Instrument item
        /// </summary>
        /// <response code="204">Successfuly modified Instrument item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to modify Instrument item</response> 
        /// <response code="404">The specific Instrument item has not been found</response>
        /// <response code="500">An exception has been thrown during modification of specific Instrument item</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ModelResult<Instrument>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<Instrument>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<Instrument>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ModelResult<Instrument>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> ModifyInstrumentAsync([FromBody] ModifyInstrumentRequest request)
        {
            return HandleResult<ModifyInstrumentRequest, StatusCodeResponse<Instrument>, Instrument>(request);
        }

        /// <summary>
        /// Deletes specific Instrument item
        /// </summary>
        /// <param name="request">Id of Instrument item to delete</param>
        /// <response code="204">Successfuly deleted Instrument item</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to delete Instrument item</response> 
        /// <response code="404">The specific Instrument item has not been found</response>
        /// <response code="500">An exception has been thrown during deletion of specific Instrument item</response> 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ModelResult<Instrument>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelResult<Instrument>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ModelResult<Instrument>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ModelResult<Instrument>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RemoveInstrumentAsync([FromQuery] RemoveInstrumentRequest request)
        {
            return HandleResult<RemoveInstrumentRequest, StatusCodeResponse<Instrument>, Instrument>(request);
        }
    }
}

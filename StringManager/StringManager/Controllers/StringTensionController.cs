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
    public class StringTensionController : ApiControllerBase<StringTensionController>
    {
        public StringTensionController(IMediator mediator, ILogger<StringTensionController> logger) : base(mediator, logger)
        {
            logger.LogInformation("StringSetsController started");
        }

        /// <summary>
        /// Gets an array of scale lenghts for Instrument item
        /// </summary>
        /// <param name="request">Id of Instrument to calculate scale lenghts for</param>
        /// <returns>An array of scale lenghts</returns>
        /// <response code="200">Gets an array of scale lenghts for Instrument items</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="500">An exception has been thrown during calculating scale lenghts for specific Instrument item</response> 
        [AllowAnonymous]
        [HttpGet]
        [Route("ScaleLenght")]
        [ProducesResponseType(typeof(StatusCodeResponse<int[]>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse<int[]>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse<int[]>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetScaleLenghtAsync([FromQuery] GetScaleLenghtsRequest request)
        {
            return HandleResult<GetScaleLenghtsRequest, StatusCodeResponse<int[]>, int[]>(request);
        }

        /// <summary>
        /// Gets String size with closest tension for different Tuning
        /// </summary>
        /// <returns>A String size</returns>
        /// <response code="200">Gets String size with closest tension for different Tuning</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="500">An exception has been thrown during calculating of String size with closest tension for different Tuning</response> 
        [AllowAnonymous]
        [HttpGet]
        [Route("StringsInSize")]
        [ProducesResponseType(typeof(StatusCodeResponse<List<String>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse<List<String>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse<List<String>>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetStringSizeWithCorrepondingTensionAsync([FromQuery] GetStringSizeWithCorrepondingTensionRequest request)
        {
            return HandleResult<GetStringSizeWithCorrepondingTensionRequest, StatusCodeResponse<List<String>>, List<String>>(request);
        }

        /// <summary>
        /// Gets a StringsSet item with the closest tension for different tuning
        /// </summary>
        /// <returns>A StringsSet item</returns>
        /// <response code="200">Gets a StringsSet item with the closest tension for different tuning</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="401">User is not authorized to get a StringsSet item with the closest tension for different tuning</response> 
        /// <response code="500">An exception has been thrown during calculating of a StringsSet item with the closest tension</response> 
        [HttpGet]
        [Route("StringsSets")]
        [ProducesResponseType(typeof(StatusCodeResponse<List<StringsSet>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse<List<StringsSet>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse<List<StringsSet>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(StatusCodeResponse<List<StringsSet>>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetStringsSetsWithCorrepondingTensionAsync([FromQuery] GetStringsSetsWithCorrepondingTensionRequest request)
        {
            return HandleResult <GetStringsSetsWithCorrepondingTensionRequest, StatusCodeResponse<List<StringsSet>>, List<StringsSet>>(request);
        }

        /// <summary>
        /// Gets tension for String tuned to specific Tone and with given scale lenght
        /// </summary>
        /// <returns>Tension for String</returns>
        /// <response code="200">Gets tension for String tuned to specific Tone and with given scale lenght</response>
        /// <response code="400">Data in request is not valid</response>
        /// <response code="500">An exception has been thrown during calculating of tension for String</response> 
        [AllowAnonymous]
        [HttpGet]
        [Route("StringTension")]
        [ProducesResponseType(typeof(StatusCodeResponse<double?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResponse<double?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StatusCodeResponse<double?>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetStringTensionAsync([FromQuery] GetStringTensionRequest request)
        {
            return HandleResult<GetStringTensionRequest, StatusCodeResponse<double?>, double?>(request);
        }
    }
}

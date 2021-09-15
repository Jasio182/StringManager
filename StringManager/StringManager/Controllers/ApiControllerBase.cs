using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain;
using StringManager.Services.API.ErrorHandling;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        private readonly IMediator mediator;

        public ApiControllerBase(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected async Task<IActionResult> HandleResult<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
            where TResponse : ErrorResponseBase
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState
                    .Where(entry => entry.Value.Errors.Any())
                    .Select(entry => new { property = entry.Key, entry.Value.Errors }));
            }
            var response = await mediator.Send(request);
            if(response.Error != null)
            {
                return ErrorResponse(response.Error);
            }
            return Ok(response);
        }

        private IActionResult ErrorResponse(ErrorModel errorModel)
        {
            var statusCode = GetStatusCode(errorModel.Error);
            return StatusCode((int)statusCode, errorModel);
        }

        private static HttpStatusCode GetStatusCode(string errorType)
        {
            switch(errorType)
            {
                case ErrorType.NotFound:
                    return HttpStatusCode.NotFound;
                case ErrorType.InternalServerError:
                    return HttpStatusCode.InternalServerError;
                case ErrorType.BadRequest:
                default:
                    return HttpStatusCode.BadRequest;
            };
              
        }
    }
}

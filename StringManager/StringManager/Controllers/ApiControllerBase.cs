using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.ErrorHandling;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    public abstract class ApiControllerBase<T> : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<T> logger;

        public ApiControllerBase(IMediator mediator, ILogger<T> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        protected async Task<IActionResult> HandleResult<TRequest, TResponse>(TRequest request)
            where TRequest : RequestBase<TResponse>
            where TResponse : ErrorResponseBase
        {
            if(!ModelState.IsValid)
            {
                logger.LogInformation("ModelState is invalid");
                return BadRequest(ModelState
                    .Where(entry => entry.Value.Errors.Any())
                    .Select(entry => new { property = entry.Key, entry.Value.Errors }));
            }
            request.UserId = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var tempUserId) ? tempUserId : null;
            request.AccountType = System.Enum.TryParse<Core.Enums.AccountType>(User.FindFirstValue(ClaimTypes.Role), out var tempAccountType) ? tempAccountType : null;
            var response = await mediator.Send(request);
            if(response.Error != null)
            {
                logger.LogInformation("An error encountered during handling a request");
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
                case ErrorType.Unauthorized:
                    return HttpStatusCode.Unauthorized;
                case ErrorType.BadRequest:
                default:
                    return HttpStatusCode.BadRequest;
            };
              
        }
    }
}

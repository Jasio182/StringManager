using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Core.Models;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    public abstract class ApiControllerBase<TController> : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<TController> logger;

        public ApiControllerBase(IMediator mediator, ILogger<TController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        protected async Task<IActionResult> HandleResult<TRequest, TResponse, TModel>(TRequest request)
            where TRequest : RequestBase<TModel>
            where TResponse : StatusCodeResponse<TModel>
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Where(entry => entry.Value.Errors.Any())
                                .Select(entry => new { property = entry.Key, entry.Value.Errors });
                    var error = "ModelState is invalid: " + ModelState;
                    logger.LogInformation(error);
                    return new ModelActionResult<object>(400, null, error);
                }
                request.UserId = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var tempUserId) ? tempUserId : null;
                request.AccountType = System.Enum.TryParse<Core.Enums.AccountType>(User.FindFirstValue(ClaimTypes.Role), out var tempAccountType) ? tempAccountType : null;
                var response = await mediator.Send(request);
                if (response.Result == null)
                {
                    var error = "An error encountered during handling a request";
                    logger.LogInformation(error);
                    return new ModelActionResult<object>(500, null, error);
                }
                return response.Result;
            }
            catch(Exception e)
            {
                var error = "An error encountered during preparation to send an request via controller " + typeof(TModel).Name;
                logger.LogInformation(error);
                return new ModelActionResult<object>(500, null, error);
            }
        }
    }
}

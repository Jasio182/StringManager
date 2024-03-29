﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StringManager.Core.MediatorRequestsAndResponses;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;
using System;
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
                    var serializableModelState = new SerializableError(ModelState);
                    var modelStateJson = JsonConvert.SerializeObject(serializableModelState);
                    var error = "ModelState is invalid: " + modelStateJson.ToString();
                    logger.LogInformation(error);
                    return new ModelActionResult<object>(400, null, modelStateJson);
                }
                request.UserId = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var tempUserId) ? tempUserId : null;
                request.AccountType = Enum.TryParse<Core.Enums.AccountType>(User.FindFirstValue(ClaimTypes.Role), out var tempAccountType) ? tempAccountType : null;
                var response = await mediator.Send(request);
                return response.Result;
            }
            catch(Exception e)
            {
                var error = "An error occured during preparation to send an request via controller: " + typeof(TController).Name;
                logger.LogInformation(error +". Exception: "+ e);
                return new ModelActionResult<object>(500, null, error);
            }
        }
    }
}

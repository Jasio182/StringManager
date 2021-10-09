﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddToneHandler : IRequestHandler<AddToneRequest, StatusCodeResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<AddToneHandler> logger;

        public AddToneHandler(IMapper mapper,
                              ICommandExecutor commandExecutor,
                              ILogger<AddToneHandler> logger)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(AddToneRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError(request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to add a new Tone");
                    return new StatusCodeResponse()
                    {
                        Result = new UnauthorizedResult()
                    };
                }
                var toneToAdd = mapper.Map<Tone>(request);
                var command = new AddToneCommand()
                {
                    Parameter = toneToAdd
                };
                var addedTone = await commandExecutor.Execute(command);
                var mappedAddedTone = mapper.Map<Core.Models.Tone>(addedTone);
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedAddedTone)
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new StatusCodeResponse()
                {
                    Result = new StatusCodeResult((int)HttpStatusCode.InternalServerError)
                };
            }
        }
    }
}
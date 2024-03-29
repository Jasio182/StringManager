﻿using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.Core.MediatorRequestsAndResponses;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class ModifyTuningHandler : IRequestHandler<ModifyTuningRequest, StatusCodeResponse<Tuning>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyTuningHandler> logger;

        public ModifyTuningHandler(IQueryExecutor queryExecutor,
                                   ICommandExecutor commandExecutor,
                                   ILogger<ModifyTuningHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<Tuning>> Handle(ModifyTuningRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to modify a Tuning";
                    logger.LogError(error);
                    return new StatusCodeResponse<Tuning>()
                    {
                        Result = new ModelActionResult<Tuning>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var query = new GetTuningQuery()
                {
                    Id = request.Id
                };
                var tuningFromDb = await queryExecutor.Execute(query);
                if (tuningFromDb == null)
                {
                    string error = "Tuning of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<Tuning>()
                    {
                        Result = new ModelActionResult<Tuning>((int)HttpStatusCode.NotFound, null, error)
                    };
                }
                var tuningToUpdate = tuningFromDb;
                if (request.Name != null)
                    tuningToUpdate.Name = request.Name;
                if (request.NumberOfStrings != null)
                    tuningToUpdate.NumberOfStrings = (int)request.NumberOfStrings;
                var command = new ModifyTuningCommand()
                {
                    Parameter = tuningToUpdate
                };
                await commandExecutor.Execute(command);
                return new StatusCodeResponse<Tuning>()
                {
                    Result = new ModelActionResult<Tuning>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing modyfication of a Tuning";
                logger.LogError(e, error + "; exeception: " + e + " message: " + e.Message);
                return new StatusCodeResponse<Tuning>()
                {
                    Result = new ModelActionResult<Tuning>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

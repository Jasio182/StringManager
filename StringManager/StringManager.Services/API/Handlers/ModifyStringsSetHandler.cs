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
    public class ModifyStringsSetHandler : IRequestHandler<ModifyStringsSetRequest, StatusCodeResponse<StringsSet>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyStringsSetHandler> logger;

        public ModifyStringsSetHandler(IQueryExecutor queryExecutor,
                                       ICommandExecutor commandExecutor,
                                       ILogger<ModifyStringsSetHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<StringsSet>> Handle(ModifyStringsSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to modify a StringsSet";
                    logger.LogError(error);
                    return new StatusCodeResponse<StringsSet>()
                    {
                        Result = new ModelActionResult<StringsSet>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var stringsSetQuery = new GetStringsSetQuery()
                {
                    Id = request.Id
                };
                var stringsSetFromDb = await queryExecutor.Execute(stringsSetQuery);
                if (stringsSetFromDb == null)
                {
                    string error = "StringsSet of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<StringsSet>()
                    {
                        Result = new ModelActionResult<StringsSet>((int)HttpStatusCode.NotFound, null, error)
                    };
                }
                var stringsSetToUpdate = stringsSetFromDb;
                if (request.Name != null)
                    stringsSetToUpdate.Name = request.Name;
                if (request.NumberOfStrings != null)
                    stringsSetToUpdate.NumberOfStrings = (int)request.NumberOfStrings;
                var command = new ModifyStringsSetCommand()
                {
                    Parameter = stringsSetToUpdate
                };
                await commandExecutor.Execute(command);
                return new StatusCodeResponse<StringsSet>()
                {
                    Result = new ModelActionResult<StringsSet>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing modyfication of a StringsSet";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<StringsSet>()
                {
                    Result = new ModelActionResult<StringsSet>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

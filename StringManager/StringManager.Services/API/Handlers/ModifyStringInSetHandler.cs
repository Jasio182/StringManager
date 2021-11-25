using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class ModifyStringInSetHandler : IRequestHandler<ModifyStringInSetRequest, StatusCodeResponse<StringInSet>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyStringInSetHandler> logger;

        public ModifyStringInSetHandler(IQueryExecutor queryExecutor,
                                        ICommandExecutor commandExecutor,
                                        ILogger<ModifyStringInSetHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<StringInSet>> Handle(ModifyStringInSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to modify a StringInSet";
                    logger.LogError(error);
                    return new StatusCodeResponse<StringInSet>()
                    {
                        Result = new ModelActionResult<StringInSet>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var stringInSetQuery = new GetStringInSetQuery()
                {
                    Id = request.Id
                };
                var stringInSetFromDb = await queryExecutor.Execute(stringInSetQuery);
                if (stringInSetFromDb == null)
                {
                    string error = "StringInSet of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<StringInSet>()
                    {
                        Result = new ModelActionResult<StringInSet>((int)HttpStatusCode.NotFound, null, error)
                    };
                }
                var stringInSetToUpdate = stringInSetFromDb;
                if (request.StringId != null)
                {
                    var queryString = new GetStringQuery()
                    {
                        Id = (int)request.StringId
                    };
                    var stringFromDb = await queryExecutor.Execute(queryString);
                    if (stringFromDb == null)
                    {
                        string error = "String of given Id: " + request.StringId + " has not been found";
                        logger.LogError(error);
                        return new StatusCodeResponse<StringInSet>()
                        {
                            Result = new ModelActionResult<StringInSet>((int)HttpStatusCode.BadRequest, null, error)
                        };
                    }
                    stringInSetToUpdate.String = stringFromDb;
                }
                if (request.StringsSetId != null)
                {
                    var queryStringsSet = new GetStringsSetQuery()
                    {
                        Id = (int)request.StringsSetId
                    };
                    var stringsSetFromDb = await queryExecutor.Execute(queryStringsSet);
                    if (stringsSetFromDb == null)
                    {
                        string error = "StringsSet of given Id: " + request.StringsSetId + " has not been found";
                        logger.LogError(error);
                        return new StatusCodeResponse<StringInSet>()
                        {
                            Result = new ModelActionResult<StringInSet>((int)HttpStatusCode.BadRequest, null, error)
                        };
                    }
                    stringInSetToUpdate.StringsSet = stringsSetFromDb;
                }
                if (request.Position != null)
                    stringInSetToUpdate.Position = (int)request.Position;
                var command = new ModifyStringInSetCommand()
                {
                    Parameter = stringInSetToUpdate
                };
                await commandExecutor.Execute(command);
                return new StatusCodeResponse<StringInSet>()
                {
                    Result = new ModelActionResult<StringInSet>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing modyfication of a StringInSet; exeception:" + e + " message: " + e.Message;
                logger.LogError(e, error);
                return new StatusCodeResponse<StringInSet>()
                {
                    Result = new ModelActionResult<StringInSet>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

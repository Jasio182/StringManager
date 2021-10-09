using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class ModifyStringInSetHandler : IRequestHandler<ModifyStringInSetRequest, StatusCodeResponse>
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

        public async Task<StatusCodeResponse> Handle(ModifyStringInSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError(request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to modify a StringInSet");
                    return new StatusCodeResponse()
                    {
                        Result = new UnauthorizedResult()
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
                    return new StatusCodeResponse()
                    {
                        Result = new NotFoundObjectResult(error)
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
                        return new StatusCodeResponse()
                        {
                            Result = new BadRequestObjectResult(error)
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
                        return new StatusCodeResponse()
                        {
                            Result = new BadRequestObjectResult(error)
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
                return new StatusCodeResponse()
                {
                    Result = new NoContentResult()
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

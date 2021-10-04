using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddStringInSetHandler : IRequestHandler<AddStringInSetRequest, StatusCodeResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<AddStringInSetHandler> logger;

        public AddStringInSetHandler(IQueryExecutor queryExecutor,
                                     IMapper mapper,
                                     ICommandExecutor commandExecutor,
                                     ILogger<AddStringInSetHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(AddStringInSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError(request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to add a new StringInSet");
                    return new StatusCodeResponse()
                    {
                        Result = new UnauthorizedResult()
                    };
                }
                var queryStringsSet = new GetStringsSetQuery()
                {
                    Id = request.StringsSetId
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
                var queryString = new GetStringQuery()
                {
                    Id = request.StringId
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
                var stringInSetToAdd = mapper.Map<StringInSet>(
                    new System.Tuple<AddStringInSetRequest, StringsSet, String>(request, stringsSetFromDb, stringFromDb));
                var command = new AddStringInSetCommand()
                {
                    Parameter = stringInSetToAdd
                };
                var addedStringInSet = await commandExecutor.Execute(command);
                var mappedAddedStringInSet = mapper.Map<Core.Models.StringInSet>(addedStringInSet);
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedAddedStringInSet)
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

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class ModifyStringInSetHandler : IRequestHandler<ModifyStringInSetRequest, ModifyStringInSetResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyStringInSetHandler> logger;

        public ModifyStringInSetHandler(IQueryExecutor queryExecutor,
                                        IMapper mapper,
                                        ICommandExecutor commandExecutor,
                                        ILogger<ModifyStringInSetHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<ModifyStringInSetResponse> Handle(ModifyStringInSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to modify string");
                    return new ModifyStringInSetResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var stringInSetQuery = new GetStringInSetQuery()
                {
                    Id = request.Id
                };
                var stringInSetFromDb = await queryExecutor.Execute(stringInSetQuery);
                if (stringInSetFromDb == null)
                {
                    logger.LogError("StringInSet of given Id of " + request.Id + " has not been found");
                    return new ModifyStringInSetResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
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
                        logger.LogError("String of given Id of " + request.StringId + " has not been found");
                        return new ModifyStringInSetResponse()
                        {
                            Error = new ErrorModel(ErrorType.BadRequest)
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
                        logger.LogError("StringsSetId of given Id of " + request.StringsSetId + " has not been found");
                        return new ModifyStringInSetResponse()
                        {
                            Error = new ErrorModel(ErrorType.BadRequest)
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
                var modifiedStringInSet = await commandExecutor.Execute(command);
                var mappedModifiedStringInSet = mapper.Map<Core.Models.StringInSet>(modifiedStringInSet);
                return new ModifyStringInSetResponse()
                {
                    Data = mappedModifiedStringInSet
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new ModifyStringInSetResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

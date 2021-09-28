using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddStringInSetHandler : IRequestHandler<AddStringInSetRequest, AddStringInSetResponse>
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

        public async Task<AddStringInSetResponse> Handle(AddStringInSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to add string in set");
                    return new AddStringInSetResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var queryStringsSet = new GetStringsSetQuery()
                {
                    Id = request.StringsSetId
                };
                var stringsSetFromDb = await queryExecutor.Execute(queryStringsSet);
                if (stringsSetFromDb == null)
                {
                    logger.LogError("String of given Id of " + request.StringsSetId + " has not been found");
                    return new AddStringInSetResponse()
                    {
                        Error = new ErrorModel(ErrorType.BadRequest)
                    };
                }
                var queryString = new GetStringQuery()
                {
                    Id = request.StringId
                };
                var stringFromDb = await queryExecutor.Execute(queryString);
                if (stringFromDb == null)
                {
                    logger.LogError("String of given Id of " + request.StringId + " has not been found");
                    return new AddStringInSetResponse()
                    {
                        Error = new ErrorModel(ErrorType.BadRequest)
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
                return new AddStringInSetResponse()
                {
                    Data = mappedAddedStringInSet
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new AddStringInSetResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

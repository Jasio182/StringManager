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
    public class ModifyStringsSetHandler : IRequestHandler<ModifyStringsSetRequest, ModifyStringsSetResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyStringsSetHandler> logger;

        public ModifyStringsSetHandler(IQueryExecutor queryExecutor,
                                       IMapper mapper,
                                       ICommandExecutor commandExecutor,
                                       ILogger<ModifyStringsSetHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<ModifyStringsSetResponse> Handle(ModifyStringsSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to modify string");
                    return new ModifyStringsSetResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var stringsSetQuery = new GetStringsSetQuery()
                {
                    Id = request.Id
                };
                var stringsSetFromDb = await queryExecutor.Execute(stringsSetQuery);
                if (stringsSetFromDb == null)
                {
                    logger.LogError("StringInSet of given Id of " + request.Id + " has not been found");
                    return new ModifyStringsSetResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
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
                var modifiedStringsSet = await commandExecutor.Execute(command);
                var mappedModifiedStringsSet = mapper.Map<Core.Models.StringsSet>(modifiedStringsSet);
                return new ModifyStringsSetResponse()
                {
                    Data = mappedModifiedStringsSet
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new ModifyStringsSetResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

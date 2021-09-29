using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class RemoveStringsSetHandler : IRequestHandler<RemoveStringsSetRequest, RemoveStringsSetResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveStringsSetHandler> logger;

        public RemoveStringsSetHandler(IMapper mapper,
                                       ICommandExecutor commandExecutor,
                                       ILogger<RemoveStringsSetHandler> logger)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<RemoveStringsSetResponse> Handle(RemoveStringsSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to remove string");
                    return new RemoveStringsSetResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var command = new RemoveStringsSetCommand()
                {
                    Parameter = request.Id
                };
                var removedStringsSetFromDb = await commandExecutor.Execute(command);
                if (removedStringsSetFromDb == null)
                {
                    logger.LogError("String of given Id of " + request.Id + " has not been found");
                    return new RemoveStringsSetResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
                    };
                }
                var mappedRemovedStringsSet = mapper.Map<Core.Models.StringsSet>(removedStringsSetFromDb);
                return new RemoveStringsSetResponse()
                {
                    Data = mappedRemovedStringsSet
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new RemoveStringsSetResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

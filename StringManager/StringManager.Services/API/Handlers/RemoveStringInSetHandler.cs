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
    public class RemoveStringInSetHandler : IRequestHandler<RemoveStringInSetRequest, RemoveStringInSetResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveStringInSetHandler> logger;

        public RemoveStringInSetHandler(IMapper mapper,
                                        ICommandExecutor commandExecutor,
                                        ILogger<RemoveStringInSetHandler> logger)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<RemoveStringInSetResponse> Handle(RemoveStringInSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to remove string");
                    return new RemoveStringInSetResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var command = new RemoveStringInSetCommand()
                {
                    Parameter = request.Id
                };
                var removedStringInSetFromDb = await commandExecutor.Execute(command);
                if (removedStringInSetFromDb == null)
                {
                    logger.LogError("String of given Id of " + request.Id + " has not been found");
                    return new RemoveStringInSetResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
                    };
                }
                var mappedRemovedStringInSet = mapper.Map<Core.Models.StringInSet>(removedStringInSetFromDb);
                return new RemoveStringInSetResponse()
                {
                    Data = mappedRemovedStringInSet
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new RemoveStringInSetResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

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
    public class RemoveStringHandler : IRequestHandler<RemoveStringRequest, RemoveStringResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveStringHandler> logger;

        public RemoveStringHandler(IMapper mapper,
                                   ICommandExecutor commandExecutor,
                                   ILogger<RemoveStringHandler> logger)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<RemoveStringResponse> Handle(RemoveStringRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to remove string");
                    return new RemoveStringResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var command = new RemoveStringCommand()
                {
                    Parameter = request.Id
                };
                var removedStringFromDb = await commandExecutor.Execute(command);
                if (removedStringFromDb == null)
                {
                    logger.LogError("String of given Id of " + request.Id + " has not been found");
                    return new RemoveStringResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
                    };
                }
                var mappedRemovedString = mapper.Map<Core.Models.String>(removedStringFromDb);
                return new RemoveStringResponse()
                {
                    Data = mappedRemovedString
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new RemoveStringResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

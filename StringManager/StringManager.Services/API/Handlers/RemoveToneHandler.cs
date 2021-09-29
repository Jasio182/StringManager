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
    public class RemoveToneHandler : IRequestHandler<RemoveToneRequest, RemoveToneResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveToneHandler> logger;

        public RemoveToneHandler(IMapper mapper,
                                 ICommandExecutor commandExecutor,
                                 ILogger<RemoveToneHandler> logger)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<RemoveToneResponse> Handle(RemoveToneRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to remove tone");
                    return new RemoveToneResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var command = new RemoveToneCommand()
                {
                    Parameter = request.Id
                };
                var removedToneFromDb = await commandExecutor.Execute(command);
                if (removedToneFromDb == null)
                {
                    logger.LogError("String of given Id of " + request.Id + " has not been found");
                    return new RemoveToneResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
                    };
                }
                var mappedRemovedTone = mapper.Map<Core.Models.Tone>(removedToneFromDb);
                return new RemoveToneResponse()
                {
                    Data = mappedRemovedTone
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new RemoveToneResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

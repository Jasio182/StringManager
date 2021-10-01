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
    public class RemoveToneInTuningHandler : IRequestHandler<RemoveToneInTuningRequest, RemoveToneInTuningResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveToneInTuningHandler> logger;

        public RemoveToneInTuningHandler(IMapper mapper,
                                         ICommandExecutor commandExecutor,
                                         ILogger<RemoveToneInTuningHandler> logger)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<RemoveToneInTuningResponse> Handle(RemoveToneInTuningRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to remove tone in tuning");
                    return new RemoveToneInTuningResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var command = new RemoveToneInTuningCommand()
                {
                    Parameter = request.Id
                };
                var removedToneInTuningFromDb = await commandExecutor.Execute(command);
                if (removedToneInTuningFromDb == null)
                {
                    logger.LogError("ToneInTuning of given Id of " + request.Id + " has not been found");
                    return new RemoveToneInTuningResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
                    };
                }
                var mappedRemovedToneInTuning = mapper.Map<Core.Models.ToneInTuning>(removedToneInTuningFromDb);
                return new RemoveToneInTuningResponse()
                {
                    Data = mappedRemovedToneInTuning
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new RemoveToneInTuningResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

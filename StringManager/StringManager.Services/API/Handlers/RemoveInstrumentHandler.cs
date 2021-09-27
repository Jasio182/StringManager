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
    public class RemoveInstrumentHandler : IRequestHandler<RemoveInstrumentRequest, RemoveInstrumentResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveInstrumentHandler> logger;

        public RemoveInstrumentHandler(IMapper mapper,
                                       ICommandExecutor commandExecutor,
                                       ILogger<RemoveInstrumentHandler> logger)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<RemoveInstrumentResponse> Handle(RemoveInstrumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to remove an instrument");
                    return new RemoveInstrumentResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var command = new RemoveInstrumentCommand()
                {
                    Parameter = request.Id
                };
                var removedInstrumentFromDb = await commandExecutor.Execute(command);
                if (removedInstrumentFromDb == null)
                {
                    logger.LogError("Instrument of given Id of " + request.Id + " has not been found");
                    return new RemoveInstrumentResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
                    };
                }
                var mappedRemovedInstrument = mapper.Map<Core.Models.Instrument>(removedInstrumentFromDb);
                return new RemoveInstrumentResponse()
                {
                    Data = mappedRemovedInstrument
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new RemoveInstrumentResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

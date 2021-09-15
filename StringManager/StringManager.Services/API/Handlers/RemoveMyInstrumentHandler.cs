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
    public class RemoveMyInstrumentHandler : IRequestHandler<RemoveMyInstrumentRequest, RemoveMyInstrumentResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveMyInstrumentHandler> logger;

        public RemoveMyInstrumentHandler(IMapper mapper,
                                         ICommandExecutor commandExecutor,
                                         ILogger<RemoveMyInstrumentHandler> logger)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<RemoveMyInstrumentResponse> Handle(RemoveMyInstrumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var command = new RemoveMyInstrumentCommand()
                {
                    Parameter = request.Id
                };
                var removedMyInstrumentFromDb = await commandExecutor.Execute(command);
                if (removedMyInstrumentFromDb == null)
                {
                    logger.LogError("MyInstrument of given Id of " + request.Id + " has not been found");
                    return new RemoveMyInstrumentResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
                    };
                }
                var mappedRemovedMyInstrument = mapper.Map<Core.Models.MyInstrument>(removedMyInstrumentFromDb);
                return new RemoveMyInstrumentResponse()
                {
                    Data = mappedRemovedMyInstrument
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new RemoveMyInstrumentResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

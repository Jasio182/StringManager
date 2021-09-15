using AutoMapper;
using MediatR;
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

        public RemoveMyInstrumentHandler(IMapper mapper, ICommandExecutor commandExecutor)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
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
            catch (System.Exception)
            {
                return new RemoveMyInstrumentResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

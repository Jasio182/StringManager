using AutoMapper;
using MediatR;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class RemovedMyInstrumentHandler : IRequestHandler<RemovedMyInstrumentRequest, RemovedMyInstrumentResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;

        public RemovedMyInstrumentHandler(IMapper mapper, ICommandExecutor commandExecutor)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
        }

        public async Task<RemovedMyInstrumentResponse> Handle(RemovedMyInstrumentRequest request, CancellationToken cancellationToken)
        {
            var command = new RemoveMyInstrumentCommand()
            {
                Parameter = request.Id
            };
            var removedMyInstrumentFromDb = await commandExecutor.Execute(command);
            var mappedRemovedMyInstrument = mapper.Map<Core.Models.MyInstrument>(removedMyInstrumentFromDb);
            return new RemovedMyInstrumentResponse()
            {
                Data = mappedRemovedMyInstrument
            };
        }
    }
}

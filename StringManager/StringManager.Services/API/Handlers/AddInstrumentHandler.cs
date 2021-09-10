using AutoMapper;
using MediatR;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddInstrumentHandler : IRequestHandler<AddInstrumentRequest, AddInstrumentResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;

        public AddInstrumentHandler(IQueryExecutor queryExecutor, IMapper mapper, ICommandExecutor commandExecutor)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
        }

        public async Task<AddInstrumentResponse> Handle(AddInstrumentRequest request, CancellationToken cancellationToken)
        {
            var queryManufacturer = new GetManufacturerQuery()
            {
                Id = request.ManufacturerId
            };
            var manufacturerFromDb = await queryExecutor.Execute(queryManufacturer);
            var instrumentToAdd = new Instrument()
            {
                Model = request.Model,
                NumberOfStrings = request.NumberOfStrings,
                ScaleLenghtBass = request.ScaleLenghtBass,
                ScaleLenghtTreble = request.ScaleLenghtTreble,
                Manufacturer = manufacturerFromDb
            };
            var command = new AddInstrumentCommand()
            {
                Parameter = instrumentToAdd
            };
            var addedInstrument = await commandExecutor.Execute(command);
            var mappedAddedInstrument = mapper.Map<Core.Models.Instrument>(addedInstrument);
            return new AddInstrumentResponse()
            {
                Data = mappedAddedInstrument
            };
        }
    }
}

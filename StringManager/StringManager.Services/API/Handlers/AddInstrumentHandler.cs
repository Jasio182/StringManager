using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddInstrumentHandler : IRequestHandler<AddInstrumentRequest, AddInstrumentResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<AddInstrumentHandler> logger;

        public AddInstrumentHandler(IQueryExecutor queryExecutor,
                                    IMapper mapper,
                                    ICommandExecutor commandExecutor,
                                    ILogger<AddInstrumentHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<AddInstrumentResponse> Handle(AddInstrumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var queryManufacturer = new GetManufacturerQuery()
                {
                    Id = request.ManufacturerId
                };
                var manufacturerFromDb = await queryExecutor.Execute(queryManufacturer);
                if (manufacturerFromDb == null)
                {
                    logger.LogError("Manufacturer of given Id of " + request.ManufacturerId + " has not been found");
                    return new AddInstrumentResponse()
                    {
                        Error = new ErrorModel(ErrorType.BadRequest)
                    };
                }
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
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new AddInstrumentResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }

        }
    }
}

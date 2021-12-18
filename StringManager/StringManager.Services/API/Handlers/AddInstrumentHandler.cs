using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddInstrumentHandler : IRequestHandler<AddInstrumentRequest, StatusCodeResponse<Core.Models.Instrument>>
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

        public async Task<StatusCodeResponse<Core.Models.Instrument>> Handle(AddInstrumentRequest request, CancellationToken cancellationToken)
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
                    string error = "Manufacturer of given Id: " + request.ManufacturerId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<Core.Models.Instrument>()
                    {
                        Result = new Core.Models.ModelActionResult<Core.Models.Instrument>((int)HttpStatusCode.BadRequest, null, error)
                    };
                }
                var instrumentToAdd = mapper.Map<Instrument>(
                    new System.Tuple<AddInstrumentRequest, Manufacturer>(request, manufacturerFromDb));
                var command = new AddInstrumentCommand()
                {
                    Parameter = instrumentToAdd
                };
                var addedInstrument = await commandExecutor.Execute(command);
                var mappedAddedInstrument = mapper.Map<Core.Models.Instrument>(addedInstrument);
                return new StatusCodeResponse<Core.Models.Instrument>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.Instrument>((int)HttpStatusCode.OK, mappedAddedInstrument)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing adding new Instrument item";
                logger.LogError(e, error + "exeception: " + e + " message: " + e.Message);
                return new StatusCodeResponse<Core.Models.Instrument>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.Instrument>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }

        }
    }
}

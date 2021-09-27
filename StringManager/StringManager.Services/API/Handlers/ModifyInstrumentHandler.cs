using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class ModifyInstrumentHandler : IRequestHandler<ModifyInstrumentRequest, ModifyInstrumentResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyInstrumentHandler> logger;

        public ModifyInstrumentHandler(IQueryExecutor queryExecutor,
                                       IMapper mapper,
                                       ICommandExecutor commandExecutor,
                                       ILogger<ModifyInstrumentHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<ModifyInstrumentResponse> Handle(ModifyInstrumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to modify an instrument");
                    return new ModifyInstrumentResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var instrumentQuery = new GetInstrumentQuery()
                {
                    Id = request.Id
                };
                var instrumentFromDb = await queryExecutor.Execute(instrumentQuery);
                if (instrumentFromDb == null)
                {
                    logger.LogError("Instrument of given Id of " + request.Id + " has not been found");
                    return new ModifyInstrumentResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
                    };
                }
                var instrumentToUpdate = instrumentFromDb;
                if (request.ManufacturerId != null)
                {
                    var manufacturerQuery = new GetManufacturerQuery()
                    {
                        Id = (int)request.ManufacturerId
                    };
                    var manufacturerFromDb = await queryExecutor.Execute(manufacturerQuery);
                    if (manufacturerFromDb == null)
                    {
                        logger.LogError("Manufacturer of given Id of " + request.ManufacturerId + " has not been found");
                        return new ModifyInstrumentResponse()
                        {
                            Error = new ErrorModel(ErrorType.BadRequest)
                        };
                    }
                    instrumentToUpdate.Manufacturer = manufacturerFromDb;
                }
                if (request.Model != null)
                    instrumentToUpdate.Model = request.Model;
                if (request.NumberOfStrings != null)
                    instrumentToUpdate.NumberOfStrings = (int)request.NumberOfStrings;
                if (request.ScaleLenghtBass != null)
                    instrumentToUpdate.ScaleLenghtBass = (int)request.ScaleLenghtBass;
                if (request.ScaleLenghtTreble != null)
                    instrumentToUpdate.ScaleLenghtTreble = (int)request.ScaleLenghtTreble;
                var command = new ModifyInstrumentCommand()
                {
                    Parameter = instrumentToUpdate
                };
                var modifiedInstrument = commandExecutor.Execute(command);
                var mappedModifiedInstrument = mapper.Map<Core.Models.Instrument>(modifiedInstrument);
                return new ModifyInstrumentResponse()
                {
                    Data = mappedModifiedInstrument
                };
            }
            catch(System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new ModifyInstrumentResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

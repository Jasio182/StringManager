using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class ModifyInstrumentHandler : IRequestHandler<ModifyInstrumentRequest, StatusCodeResponse<Instrument>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyInstrumentHandler> logger;

        public ModifyInstrumentHandler(IQueryExecutor queryExecutor,
                                       ICommandExecutor commandExecutor,
                                       ILogger<ModifyInstrumentHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<Instrument>> Handle(ModifyInstrumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to modify an Instrument";
                    logger.LogError(error);
                    return new StatusCodeResponse<Instrument>()
                    {
                        Result = new ModelActionResult<Instrument>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var instrumentQuery = new GetInstrumentQuery()
                {
                    Id = request.Id
                };
                var instrumentFromDb = await queryExecutor.Execute(instrumentQuery);
                if (instrumentFromDb == null)
                {
                    string error = "instrument of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<Instrument>()
                    {
                        Result = new ModelActionResult<Instrument>((int)HttpStatusCode.NotFound, null, error)
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
                        string error = "Manufacturer of given Id: " + request.ManufacturerId + " has not been found";
                        logger.LogError(error);
                        return new StatusCodeResponse<Instrument>()
                        {
                            Result = new ModelActionResult<Instrument>((int)HttpStatusCode.BadRequest, null, error)
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
                await commandExecutor.Execute(command);
                return new StatusCodeResponse<Instrument>()
                {
                    Result = new ModelActionResult<Instrument>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing modyfication of an Instrument; exeception:" + e + " message: " + e.Message;
                logger.LogError(e, error);
                return new StatusCodeResponse<Instrument>()
                {
                    Result = new ModelActionResult<Instrument>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

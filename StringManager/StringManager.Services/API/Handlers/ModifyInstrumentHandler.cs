using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class ModifyInstrumentHandler : IRequestHandler<ModifyInstrumentRequest, StatusCodeResponse>
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

        public async Task<StatusCodeResponse> Handle(ModifyInstrumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError(request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to modify an Instrument");
                    return new StatusCodeResponse()
                    {
                        Result = new UnauthorizedResult()
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
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
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
                        return new StatusCodeResponse()
                        {
                            Result = new BadRequestObjectResult(error)
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
                return new StatusCodeResponse()
                {
                    Result = new NoContentResult()
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new StatusCodeResponse()
                {
                    Result = new StatusCodeResult((int)HttpStatusCode.InternalServerError)
                };
            }
        }
    }
}

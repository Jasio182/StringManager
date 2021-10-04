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
    public class ModifyStringHandler : IRequestHandler<ModifyStringRequest, StatusCodeResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyStringHandler> logger;

        public ModifyStringHandler(IQueryExecutor queryExecutor,
                                   ICommandExecutor commandExecutor,
                                   ILogger<ModifyStringHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(ModifyStringRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError(request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to modify a String");
                    return new StatusCodeResponse()
                    {
                        Result = new UnauthorizedResult()
                    };
                }
                var stringQuery = new GetStringQuery()
                {
                    Id = request.Id
                };
                var stringFromDb = await queryExecutor.Execute(stringQuery);
                if (stringFromDb == null)
                {
                    string error = "String of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
                    };
                }
                var stringToUpdate = stringFromDb;
                if (request.ManufacturerId != null)
                {
                    var queryManufacturer = new GetManufacturerQuery()
                    {
                        Id = (int)request.ManufacturerId
                    };
                    var manufacturerFromDb = await queryExecutor.Execute(queryManufacturer);
                    if (manufacturerFromDb == null)
                    {
                        string error = "Manufacturer of given Id: " + request.ManufacturerId + " has not been found";
                        logger.LogError(error);
                        return new StatusCodeResponse()
                        {
                            Result = new BadRequestObjectResult(error)
                        };
                    }
                    stringToUpdate.Manufacturer = manufacturerFromDb;
                }
                if (request.NumberOfDaysGood != null)
                    stringToUpdate.NumberOfDaysGood = (int)request.NumberOfDaysGood;
                if (request.Size != null)
                    stringToUpdate.Size = (int)request.Size;
                if (request.SpecificWeight != null)
                    stringToUpdate.SpecificWeight = (double)request.SpecificWeight;
                if (request.StringType != null)
                    stringToUpdate.StringType = (Core.Enums.StringType)request.StringType;
                var command = new ModifyStringCommand()
                {
                    Parameter = stringToUpdate
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

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public class AddStringHandler : IRequestHandler<AddStringRequest, StatusCodeResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<AddStringHandler> logger;

        public AddStringHandler(IQueryExecutor queryExecutor,
                                IMapper mapper,
                                ICommandExecutor commandExecutor,
                                ILogger<AddStringHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(AddStringRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError(request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to add a new String");
                    return new StatusCodeResponse()
                    {
                        Result = new UnauthorizedResult()
                    };
                }
                var queryManufacturer = new GetManufacturerQuery()
                {
                    Id = request.ManufacturerId
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
                var stringToAdd = mapper.Map<String>(
                    new System.Tuple<AddStringRequest, Manufacturer>(request, manufacturerFromDb));
                var command = new AddStringCommand()
                {
                    Parameter = stringToAdd
                };
                var addedString = await commandExecutor.Execute(command);
                var mappedAddedString = mapper.Map<Core.Models.String>(addedString);
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedAddedString)
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

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
    public class AddStringHandler : IRequestHandler<AddStringRequest, AddStringResponse>
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

        public async Task<AddStringResponse> Handle(AddStringRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to add string");
                    return new AddStringResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var queryManufacturer = new GetManufacturerQuery()
                {
                    Id = request.ManufacturerId
                };
                var manufacturerFromDb = await queryExecutor.Execute(queryManufacturer);
                if (manufacturerFromDb == null)
                {
                    logger.LogError("Manufacturer of given Id of " + request.ManufacturerId + " has not been found");
                    return new AddStringResponse()
                    {
                        Error = new ErrorModel(ErrorType.BadRequest)
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
                return new AddStringResponse()
                {
                    Data = mappedAddedString
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new AddStringResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

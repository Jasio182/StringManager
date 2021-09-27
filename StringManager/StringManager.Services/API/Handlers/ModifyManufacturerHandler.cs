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
    public class ModifyManufacturerHandler : IRequestHandler<ModifyManufacturerRequest, ModifyManufacturerResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyManufacturerHandler> logger;

        public ModifyManufacturerHandler(IQueryExecutor queryExecutor,
                                         IMapper mapper,
                                         ICommandExecutor commandExecutor,
                                         ILogger<ModifyManufacturerHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<ModifyManufacturerResponse> Handle(ModifyManufacturerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to modify a manufacturer");
                    return new ModifyManufacturerResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var manufacturerQuery = new GetManufacturerQuery()
                {
                    Id = request.Id
                };
                var manufacturerFromDb = await queryExecutor.Execute(manufacturerQuery);
                if (manufacturerFromDb == null)
                {
                    logger.LogError("Manufacturer of given Id of " + request.Id + " has not been found");
                    return new ModifyManufacturerResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
                    };
                }
                var manufacturerToUpdate = manufacturerFromDb;
                manufacturerToUpdate.Name = request.Name;
                var command = new ModifyManufacturerCommand()
                {
                    Parameter = manufacturerToUpdate
                };
                var modifiedManufacturer = await commandExecutor.Execute(command);
                var mappedModifiedManufacturer = mapper.Map<Core.Models.Manufacturer>(modifiedManufacturer);
                return new ModifyManufacturerResponse()
                {
                    Data = mappedModifiedManufacturer
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new ModifyManufacturerResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

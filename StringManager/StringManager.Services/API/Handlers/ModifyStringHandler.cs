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
    public class ModifyStringHandler : IRequestHandler<ModifyStringRequest, ModifyStringResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyStringHandler> logger;

        public ModifyStringHandler(IQueryExecutor queryExecutor,
                                   IMapper mapper,
                                   ICommandExecutor commandExecutor,
                                   ILogger<ModifyStringHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<ModifyStringResponse> Handle(ModifyStringRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to modify string");
                    return new ModifyStringResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var stringQuery = new GetStringQuery()
                {
                    Id = request.Id
                };
                var stringFromDb = await queryExecutor.Execute(stringQuery);
                if (stringFromDb == null)
                {
                    logger.LogError("String of given Id of " + request.Id + " has not been found");
                    return new ModifyStringResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
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
                        logger.LogError("Manufacturer of given Id of " + request.ManufacturerId + " has not been found");
                        return new ModifyStringResponse()
                        {
                            Error = new ErrorModel(ErrorType.BadRequest)
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
                var modifiedString = await commandExecutor.Execute(command);
                var mappedModifiedString = mapper.Map<Core.Models.String>(modifiedString);
                return new ModifyStringResponse()
                {
                    Data = mappedModifiedString
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new ModifyStringResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

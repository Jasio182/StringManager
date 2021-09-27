using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class RemoveManufacturerHandler : IRequestHandler<RemoveManufacturerRequest, RemoveManufacturerResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveManufacturerHandler> logger;

        public RemoveManufacturerHandler(IMapper mapper,
                                         ICommandExecutor commandExecutor,
                                         ILogger<RemoveManufacturerHandler> logger)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<RemoveManufacturerResponse> Handle(RemoveManufacturerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to remove a manufacturer");
                    return new RemoveManufacturerResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var command = new RemoveManufacturerCommand()
                {
                    Parameter = request.Id
                };
                var removedManufacturerFromDb = await commandExecutor.Execute(command);
                if (removedManufacturerFromDb == null)
                {
                    logger.LogError("Manufacturer of given Id of " + request.Id + " has not been found");
                    return new RemoveManufacturerResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
                    };
                }
                var mappedRemovedManufacturer = mapper.Map<Core.Models.Manufacturer>(removedManufacturerFromDb);
                return new RemoveManufacturerResponse()
                {
                    Data = mappedRemovedManufacturer
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new RemoveManufacturerResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

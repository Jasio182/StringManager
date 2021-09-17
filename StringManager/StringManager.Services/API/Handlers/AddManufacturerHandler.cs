using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddManufacturerHandler : IRequestHandler<AddManufacturerRequest, AddManufacturerResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<AddManufacturerHandler> logger;

        public AddManufacturerHandler(IMapper mapper,
                                      ICommandExecutor commandExecutor,
                                      ILogger<AddManufacturerHandler> logger)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<AddManufacturerResponse> Handle(AddManufacturerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var manufacturerToAdd = mapper.Map<Manufacturer>(request);
                var command = new AddManufacturerCommand()
                {
                    Parameter = manufacturerToAdd
                };
                var addedManufacturer = await commandExecutor.Execute(command);
                var mappedAddedManufacturer = mapper.Map<Core.Models.Manufacturer>(addedManufacturer);
                return new AddManufacturerResponse()
                {
                    Data = mappedAddedManufacturer
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new AddManufacturerResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

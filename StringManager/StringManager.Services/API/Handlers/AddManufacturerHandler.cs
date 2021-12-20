using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddManufacturerHandler : IRequestHandler<AddManufacturerRequest, StatusCodeResponse<Core.Models.Manufacturer>>
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

        public async Task<StatusCodeResponse<Core.Models.Manufacturer>> Handle(AddManufacturerRequest request, CancellationToken cancellationToken)
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
                return new StatusCodeResponse<Core.Models.Manufacturer>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.Manufacturer>((int)HttpStatusCode.OK, mappedAddedManufacturer)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing adding new Manufacturer item";
                logger.LogError(e, error+ "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<Core.Models.Manufacturer>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.Manufacturer>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

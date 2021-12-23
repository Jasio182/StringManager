using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class RemoveManufacturerHandler : IRequestHandler<RemoveManufacturerRequest, StatusCodeResponse<Manufacturer>>
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<RemoveManufacturerHandler> logger;

        public RemoveManufacturerHandler(ICommandExecutor commandExecutor,
                                         ILogger<RemoveManufacturerHandler> logger)
        {
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<Manufacturer>> Handle(RemoveManufacturerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to remove an Instrument";
                    logger.LogError(error);
                    return new StatusCodeResponse<Manufacturer>()
                    {
                        Result = new ModelActionResult<Manufacturer>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var command = new RemoveManufacturerCommand()
                {
                    Parameter = request.Id
                };
                var removedManufacturerFromDb = await commandExecutor.Execute(command);
                if (removedManufacturerFromDb == null)
                {
                    string error = "Manufacturer of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<Manufacturer>()
                    {
                        Result = new ModelActionResult<Manufacturer>((int)HttpStatusCode.NotFound, null, error)
                    };
                }
                return new StatusCodeResponse<Manufacturer>()
                {
                    Result = new ModelActionResult<Manufacturer>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing deletion of a Manufacturer";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<Manufacturer>()
                {
                    Result = new ModelActionResult<Manufacturer>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

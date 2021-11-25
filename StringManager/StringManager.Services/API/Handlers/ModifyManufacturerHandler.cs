using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.Core.Models;
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
    public class ModifyManufacturerHandler : IRequestHandler<ModifyManufacturerRequest, StatusCodeResponse<Manufacturer>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyManufacturerHandler> logger;

        public ModifyManufacturerHandler(IQueryExecutor queryExecutor,
                                         ICommandExecutor commandExecutor,
                                         ILogger<ModifyManufacturerHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<Manufacturer>> Handle(ModifyManufacturerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to modify a Manufacturer";
                    logger.LogError(error);
                    return new StatusCodeResponse<Manufacturer>()
                    {
                        Result = new ModelActionResult<Manufacturer>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var manufacturerQuery = new GetManufacturerQuery()
                {
                    Id = request.Id
                };
                var manufacturerFromDb = await queryExecutor.Execute(manufacturerQuery);
                if (manufacturerFromDb == null)
                {
                    string error = "Manufacturer of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<Manufacturer>()
                    {
                        Result = new ModelActionResult<Manufacturer>((int)HttpStatusCode.NotFound, null, error)
                    };
                }
                var manufacturerToUpdate = manufacturerFromDb;
                manufacturerToUpdate.Name = request.Name;
                var command = new ModifyManufacturerCommand()
                {
                    Parameter = manufacturerToUpdate
                };
                await commandExecutor.Execute(command);
                return new StatusCodeResponse<Manufacturer>()
                {
                    Result = new ModelActionResult<Manufacturer>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing modyfication of a Manufacturer; exeception:" + e + " message: " + e.Message;
                logger.LogError(e, error);
                return new StatusCodeResponse<Manufacturer>()
                {
                    Result = new ModelActionResult<Manufacturer>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

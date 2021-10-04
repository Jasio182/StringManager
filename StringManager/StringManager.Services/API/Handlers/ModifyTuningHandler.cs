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
    public class ModifyTuningHandler : IRequestHandler<ModifyTuningRequest, StatusCodeResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyTuningHandler> logger;

        public ModifyTuningHandler(IQueryExecutor queryExecutor,
                                   ICommandExecutor commandExecutor,
                                   ILogger<ModifyTuningHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(ModifyTuningRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError(request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to modify a Tuning");
                    return new StatusCodeResponse()
                    {
                        Result = new UnauthorizedResult()
                    };
                }
                var query = new GetTuningQuery()
                {
                    Id = request.Id
                };
                var tuningFromDb = await queryExecutor.Execute(query);
                if (tuningFromDb == null)
                {
                    string error = "Tuning of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
                    };
                }
                var tuningToUpdate = tuningFromDb;
                if (request.Name != null)
                    tuningToUpdate.Name = request.Name;
                if (request.NumberOfStrings != null)
                    tuningToUpdate.NumberOfStrings = (int)request.NumberOfStrings;
                var command = new ModifyTuningCommand()
                {
                    Parameter = tuningToUpdate
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

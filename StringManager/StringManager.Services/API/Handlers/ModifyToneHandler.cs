using AutoMapper;
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
    public class ModifyToneHandler : IRequestHandler<ModifyToneRequest, StatusCodeResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyToneHandler> logger;

        public ModifyToneHandler(IQueryExecutor queryExecutor,
                                 ICommandExecutor commandExecutor,
                                 ILogger<ModifyToneHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(ModifyToneRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError(request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to modify a Tone");
                    return new StatusCodeResponse()
                    {
                        Result = new UnauthorizedResult()
                    };
                }
                var query = new GetToneQuery()
                {
                    Id = request.Id
                };
                var toneFromDb = await queryExecutor.Execute(query);
                if (toneFromDb == null)
                {
                    string error = "Tone of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
                    };
                }
                var toneToUpdate = toneFromDb;
                if (request.Name != null)
                    toneToUpdate.Name = request.Name;
                if (request.Frequency != null)
                    toneToUpdate.Frequency = (int)request.Frequency;
                if (request.WaveLenght != null)
                    toneToUpdate.WaveLenght = (int)request.WaveLenght;
                var command = new ModifyToneCommand()
                {
                    Parameter = toneToUpdate
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

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
    public class ModifyToneInTuningHandler : IRequestHandler<ModifyToneInTuningRequest, StatusCodeResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyToneInTuningHandler> logger;

        public ModifyToneInTuningHandler(IQueryExecutor queryExecutor,
                                         ICommandExecutor commandExecutor,
                                         ILogger<ModifyToneInTuningHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(ModifyToneInTuningRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError(request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to modify a ToneInTuning");
                    return new StatusCodeResponse()
                    {
                        Result = new UnauthorizedResult()
                    };
                }
                var query = new GetToneInTuningQuery()
                {
                    Id = request.Id
                };
                var toneInTuningFromDb = await queryExecutor.Execute(query);
                if (toneInTuningFromDb == null)
                {
                    string error = "ToneInTuning of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
                    };
                }
                var toneInTuningToUpdate = toneInTuningFromDb;
                if (request.ToneId != null)
                {
                    var queryTone = new GetToneQuery()
                    {
                        Id = (int)request.ToneId
                    };
                    var toneFromDb = await queryExecutor.Execute(queryTone);
                    if (toneFromDb == null)
                    {
                        string error = "Tone of given Id: " + request.ToneId + " has not been found";
                        logger.LogError(error);
                        return new StatusCodeResponse()
                        {
                            Result = new BadRequestObjectResult(error)
                        };
                    }
                    toneInTuningToUpdate.Tone = toneFromDb;
                }
                if (request.TuningId != null)
                {
                    var queryTuning = new GetTuningQuery()
                    {
                        Id = (int)request.TuningId
                    };
                    var tuningFromDb = await queryExecutor.Execute(queryTuning);
                    if (tuningFromDb == null)
                    {
                        string error = "Tuning of given Id: " + request.TuningId + " has not been found";
                        logger.LogError(error);
                        return new StatusCodeResponse()
                        {
                            Result = new BadRequestObjectResult(error)
                        };
                    }
                    toneInTuningToUpdate.Tuning = tuningFromDb;
                }
                if (request.Position != null)
                    toneInTuningToUpdate.Position = (int)request.Position;
                var command = new ModifyToneInTuningCommand()
                {
                    Parameter = toneInTuningToUpdate
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

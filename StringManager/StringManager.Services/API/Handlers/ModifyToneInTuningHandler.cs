using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.Core.MediatorRequestsAndResponses;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class ModifyToneInTuningHandler : IRequestHandler<ModifyToneInTuningRequest, StatusCodeResponse<ToneInTuning>>
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

        public async Task<StatusCodeResponse<ToneInTuning>> Handle(ModifyToneInTuningRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to modify a ToneInTuning";
                    logger.LogError(error);
                    return new StatusCodeResponse<ToneInTuning>()
                    {
                        Result = new ModelActionResult<ToneInTuning>((int)HttpStatusCode.Unauthorized, null, error)
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
                    return new StatusCodeResponse<ToneInTuning>()
                    {
                        Result = new ModelActionResult<ToneInTuning>((int)HttpStatusCode.NotFound, null, error)
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
                        return new StatusCodeResponse<ToneInTuning>()
                        {
                            Result = new ModelActionResult<ToneInTuning>((int)HttpStatusCode.BadRequest, null, error)
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
                        return new StatusCodeResponse<ToneInTuning>()
                        {
                            Result = new ModelActionResult<ToneInTuning>((int)HttpStatusCode.BadRequest, null, error)
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
                return new StatusCodeResponse<ToneInTuning>()
                {
                    Result = new ModelActionResult<ToneInTuning>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing modyfication of a ToneInTuning";
                logger.LogError(e, error+ "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<ToneInTuning>()
                {
                    Result = new ModelActionResult<ToneInTuning>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

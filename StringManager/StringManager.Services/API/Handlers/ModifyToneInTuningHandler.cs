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
    public class ModifyToneInTuningHandler : IRequestHandler<ModifyToneInTuningRequest, ModifyToneInTuningResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyToneInTuningHandler> logger;

        public ModifyToneInTuningHandler(IQueryExecutor queryExecutor,
                                         IMapper mapper,
                                         ICommandExecutor commandExecutor,
                                         ILogger<ModifyToneInTuningHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<ModifyToneInTuningResponse> Handle(ModifyToneInTuningRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId + " tried to modify a tone in tuning");
                    return new ModifyToneInTuningResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var query = new GetToneInTuningQuery()
                {
                    Id = request.Id
                };
                var toneInTuningFromDb = await queryExecutor.Execute(query);
                if (toneInTuningFromDb == null)
                {
                    logger.LogError("User of given Id of " + query.Id + " has not been found");
                    return new ModifyToneInTuningResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
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
                        logger.LogError("Tone of given Id of " + request.ToneId + " has not been found");
                        return new ModifyToneInTuningResponse()
                        {
                            Error = new ErrorModel(ErrorType.BadRequest)
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
                        logger.LogError("Tuning of given Id of " + request.TuningId + " has not been found");
                        return new ModifyToneInTuningResponse()
                        {
                            Error = new ErrorModel(ErrorType.BadRequest)
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
                var modifiedToneInTuning = await commandExecutor.Execute(command);
                var mappedModifiedToneInTuning = mapper.Map<Core.Models.ToneInTuning>(modifiedToneInTuning);
                return new ModifyToneInTuningResponse()
                {
                    Data = mappedModifiedToneInTuning
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new ModifyToneInTuningResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

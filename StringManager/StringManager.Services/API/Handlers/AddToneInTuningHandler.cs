using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using AutoMapper;
using StringManager.Services.API.ErrorHandling;
using StringManager.Services.API.Domain;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;

namespace StringManager.Services.API.Handlers
{
    public class AddToneInTuningHandler : IRequestHandler<AddToneInTuningRequest, AddToneInTuningResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<AddToneInTuningHandler> logger;

        public AddToneInTuningHandler(IQueryExecutor queryExecutor,
                                      IMapper mapper,
                                      ICommandExecutor commandExecutor,
                                      ILogger<AddToneInTuningHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<AddToneInTuningResponse> Handle(AddToneInTuningRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to add tone in tuning");
                    return new AddToneInTuningResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var queryTone = new GetToneQuery()
                {
                    Id = request.ToneId
                };
                var toneFromDb = await queryExecutor.Execute(queryTone);
                if (toneFromDb == null)
                {
                    logger.LogError("Tone of given Id of " + request.ToneId + " has not been found");
                    return new AddToneInTuningResponse()
                    {
                        Error = new ErrorModel(ErrorType.BadRequest)
                    };
                }
                var queryTuning = new GetTuningQuery()
                {
                    Id = request.TuningId
                };
                var tuningFromDb = await queryExecutor.Execute(queryTuning);
                if (tuningFromDb == null)
                {
                    logger.LogError("Tuning of given Id of " + request.TuningId + " has not been found");
                    return new AddToneInTuningResponse()
                    {
                        Error = new ErrorModel(ErrorType.BadRequest)
                    };
                }
                var toneInTuningToAdd = mapper.Map<ToneInTuning>(
                    new System.Tuple<AddToneInTuningRequest, Tone, Tuning>(request, toneFromDb, tuningFromDb));
                var command = new AddToneInTuningCommand()
                {
                    Parameter = toneInTuningToAdd
                };
                var addedToneInTuning = await commandExecutor.Execute(command);
                var mappedAddedToneInTuning = mapper.Map<Core.Models.ToneInTuning>(addedToneInTuning);
                return new AddToneInTuningResponse()
                {
                    Data = mappedAddedToneInTuning
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new AddToneInTuningResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

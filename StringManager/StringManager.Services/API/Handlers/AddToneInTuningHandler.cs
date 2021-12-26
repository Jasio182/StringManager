using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using AutoMapper;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using System.Net;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.MediatorRequestsAndResponses;

namespace StringManager.Services.API.Handlers
{
    public class AddToneInTuningHandler : IRequestHandler<AddToneInTuningRequest, StatusCodeResponse<Core.Models.ToneInTuning>>
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

        public async Task<StatusCodeResponse<Core.Models.ToneInTuning>> Handle(AddToneInTuningRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to add a new ToneInTuning";
                    logger.LogError(error);
                    return new StatusCodeResponse<Core.Models.ToneInTuning>()
                    {
                        Result = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var queryTone = new GetToneQuery()
                {
                    Id = request.ToneId
                };
                var toneFromDb = await queryExecutor.Execute(queryTone);
                if (toneFromDb == null)
                {
                    string error = "Tone of given Id: " + request.ToneId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<Core.Models.ToneInTuning>()
                    {
                        Result = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.BadRequest, null, error)
                    };
                }
                var queryTuning = new GetTuningQuery()
                {
                    Id = request.TuningId
                };
                var tuningFromDb = await queryExecutor.Execute(queryTuning);
                if (tuningFromDb == null)
                {
                    string error = "Tuning of given Id: " + request.TuningId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<Core.Models.ToneInTuning>()
                    {
                        Result = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.BadRequest, null, error)
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
                return new StatusCodeResponse<Core.Models.ToneInTuning>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.OK, mappedAddedToneInTuning)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing adding new ToneInTuning item";
                logger.LogError(e, error+ "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<Core.Models.ToneInTuning>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.ToneInTuning>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

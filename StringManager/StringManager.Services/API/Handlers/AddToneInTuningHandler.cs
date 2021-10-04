using StringManager.Services.API.Domain.Requests;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using AutoMapper;
using StringManager.Services.API.Domain;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace StringManager.Services.API.Handlers
{
    public class AddToneInTuningHandler : IRequestHandler<AddToneInTuningRequest, StatusCodeResponse>
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

        public async Task<StatusCodeResponse> Handle(AddToneInTuningRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError(request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to add a new ToneInTuning");
                    return new StatusCodeResponse()
                    {
                        Result = new UnauthorizedResult()
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
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
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
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
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
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedAddedToneInTuning)
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

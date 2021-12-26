using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public class ModifyToneHandler : IRequestHandler<ModifyToneRequest, StatusCodeResponse<Tone>>
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

        public async Task<StatusCodeResponse<Tone>> Handle(ModifyToneRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to modify a Tone";
                    logger.LogError(error);
                    return new StatusCodeResponse<Tone>()
                    {
                        Result = new ModelActionResult<Tone>((int)HttpStatusCode.Unauthorized, null, error)
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
                    return new StatusCodeResponse<Tone>()
                    {
                        Result = new ModelActionResult<Tone>((int)HttpStatusCode.NotFound, null, error)
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
                return new StatusCodeResponse<Tone>()
                {
                    Result = new ModelActionResult<Tone>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing modyfication of a Tone";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<Tone>()
                {
                    Result = new ModelActionResult<Tone>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

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
    public class ModifyInstalledStringHandler : IRequestHandler<ModifyInstalledStringRequest, StatusCodeResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyInstalledStringHandler> logger;

        public ModifyInstalledStringHandler(IQueryExecutor queryExecutor,
                                            ICommandExecutor commandExecutor,
                                            ILogger<ModifyInstalledStringHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(ModifyInstalledStringRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var installedStringQuery = new GetInstalledStringQuery()
                {
                    Id = request.Id,
                    UserId = (int)request.UserId,
                    AccountType = (Core.Enums.AccountType)request.AccountType
                };
                var installedStringFromDb = await queryExecutor.Execute(installedStringQuery);
                if (installedStringFromDb == null)
                {
                    string error = "InstalledString of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse()
                    {
                        Result = new BadRequestObjectResult(error)
                    };
                }
                var installedStringToUpdate = installedStringFromDb;
                if (request.StringId != null)
                {
                    var stringQuery = new GetStringQuery()
                    {
                        Id = (int)request.StringId
                    };
                    var stringFromDb = await queryExecutor.Execute(stringQuery);
                    if (stringFromDb == null)
                    {
                        string error = "String of given Id: " + request.StringId + " has not been found";
                        logger.LogError(error);
                        return new StatusCodeResponse()
                        {
                            Result = new BadRequestObjectResult(error)
                        };
                    }
                    installedStringToUpdate.String = stringFromDb;
                }
                if (request.ToneId != null)
                {
                    var toneQuery = new GetToneQuery()
                    {
                        Id = (int)request.ToneId
                    };
                    var toneFromDb = await queryExecutor.Execute(toneQuery);
                    if (toneFromDb == null)
                    {
                        string error = "Tone of given Id: " + request.ToneId + " has not been found";
                        logger.LogError(error);
                        return new StatusCodeResponse()
                        {
                            Result = new BadRequestObjectResult(error)
                        };
                    }
                    installedStringToUpdate.Tone = toneFromDb;
                }
                var command = new ModifyInstalledStringCommand()
                {
                    Parameter = installedStringToUpdate
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

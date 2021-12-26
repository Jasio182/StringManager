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
    public class ModifyInstalledStringHandler : IRequestHandler<ModifyInstalledStringRequest, StatusCodeResponse<InstalledString>>
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

        public async Task<StatusCodeResponse<InstalledString>> Handle(ModifyInstalledStringRequest request, CancellationToken cancellationToken)
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
                    return new StatusCodeResponse<InstalledString>()
                    {
                        Result = new ModelActionResult<InstalledString>((int)HttpStatusCode.NotFound, null, error)
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
                        return new StatusCodeResponse<InstalledString>()
                        {
                            Result = new ModelActionResult<InstalledString>((int)HttpStatusCode.BadRequest, null, error)
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
                        return new StatusCodeResponse<InstalledString>()
                        {
                            Result = new ModelActionResult<InstalledString>((int)HttpStatusCode.BadRequest, null, error)
                        };
                    }
                    installedStringToUpdate.Tone = toneFromDb;
                }
                var command = new ModifyInstalledStringCommand()
                {
                    Parameter = installedStringToUpdate
                };
                await commandExecutor.Execute(command);
                return new StatusCodeResponse<InstalledString>()
                {
                    Result = new ModelActionResult<InstalledString>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing modyfication of a InstalledString";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<InstalledString>()
                {
                    Result = new ModelActionResult<InstalledString>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

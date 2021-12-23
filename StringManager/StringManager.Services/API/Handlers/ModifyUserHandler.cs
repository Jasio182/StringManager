using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.Core.Models;
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
    public class ModifyUserHandler : IRequestHandler<ModifyUserRequest, StatusCodeResponse<User>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyUserHandler> logger;

        public ModifyUserHandler(IQueryExecutor queryExecutor,
                                 ICommandExecutor commandExecutor,
                                 ILogger<ModifyUserHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<User>> Handle(ModifyUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if ((request.AccountTypeToUpdate != null || request.Id != null) && request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to modify an User";
                    logger.LogError(error);
                    return new StatusCodeResponse<User>()
                    {
                        Result = new ModelActionResult<User>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var query = new GetUserByIdQuery();
                if (request.Id != null)
                {
                    query.Id = (int)request.Id;
                }
                else
                {
                    query.Id = (int)request.UserId;
                }
                var userFromDb = await queryExecutor.Execute(query);
                if (userFromDb == null)
                {
                    string error = "User of given Id: " + request.Id + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<User>()
                    {
                        Result = new ModelActionResult<User>((int)HttpStatusCode.NotFound, null, error)
                    };
                }
                var userToUpdate = userFromDb;
                if (request.Username != null)
                    userToUpdate.Username = request.Username;
                if (request.Password != null)
                    userToUpdate.Password = request.Password;
                if (request.Email != null)
                    userToUpdate.Email = request.Email;
                if (request.PlayStyle != null)
                    userToUpdate.PlayStyle = (Core.Enums.PlayStyle)request.PlayStyle;
                if (request.AccountTypeToUpdate != null)
                    userToUpdate.AccountType = (Core.Enums.AccountType)request.AccountTypeToUpdate;
                if (request.DailyMaintanance != null)
                    userToUpdate.DailyMaintanance = (Core.Enums.GuitarDailyMaintanance)request.DailyMaintanance;
                var command = new ModifyUserCommand()
                {
                    Parameter = userToUpdate
                };
                await commandExecutor.Execute(command);
                return new StatusCodeResponse<User>()
                {
                    Result = new ModelActionResult<User>((int)HttpStatusCode.NoContent, null)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing modyfication of an User";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<User>()
                {
                    Result = new ModelActionResult<User>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

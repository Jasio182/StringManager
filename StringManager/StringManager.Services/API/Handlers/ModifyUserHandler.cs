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
    public class ModifyUserHandler : IRequestHandler<ModifyUserRequest, ModifyUserResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyUserHandler> logger;

        public ModifyUserHandler(IQueryExecutor queryExecutor,
                                 IMapper mapper,
                                 ICommandExecutor commandExecutor,
                                 ILogger<ModifyUserHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<ModifyUserResponse> Handle(ModifyUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if ((request.AccountTypeToUpdate != null || request.Id != null) && request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId + " tried to modify wrong data");
                    return new ModifyUserResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
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
                    logger.LogError("User of given Id of " + query.Id + " has not been found");
                    return new ModifyUserResponse()
                    {
                        Error = new ErrorModel(ErrorType.NotFound)
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
                var modifiedUser = await commandExecutor.Execute(command);
                var mappedModifiedUser = mapper.Map<Core.Models.User>(modifiedUser);
                return new ModifyUserResponse()
                {
                    Data = mappedModifiedUser
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new ModifyUserResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

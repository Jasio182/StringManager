using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddUserHandler : IRequestHandler<AddUserRequest, AddUserResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<AddUserHandler> logger;

        public AddUserHandler(IMapper mapper,
                              ICommandExecutor commandExecutor,
                              ILogger<AddUserHandler> logger)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<AddUserResponse> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountTypeToAdd != null && request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to add admin user");
                    return new AddUserResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                else
                    request.AccountTypeToAdd = Core.Enums.AccountType.User;
                var userToAdd = mapper.Map<User>(request);
                var command = new AddUserCommand()
                {
                    Parameter = userToAdd
                };
                var addedUser = await commandExecutor.Execute(command);
                var mappedAddedUser = mapper.Map<Core.Models.User>(addedUser);
                return new AddUserResponse()
                {
                    Data = mappedAddedUser
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new AddUserResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

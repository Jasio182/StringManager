using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.InternalClasses;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddUserHandler : IRequestHandler<AddUserRequest, StatusCodeResponse>
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

        public async Task<StatusCodeResponse> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountTypeToAdd == Core.Enums.AccountType.Admin && request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError(request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to add a new Admin User");
                    return new StatusCodeResponse()
                    {
                        Result = new UnauthorizedResult()
                    };
                }
                else
                    request.AccountTypeToAdd = Core.Enums.AccountType.User;
                request.Password = PasswordHashing.HashPassword(request.Password);
                var userToAdd = mapper.Map<User>(request);
                var command = new AddUserCommand()
                {
                    Parameter = userToAdd
                };
                var addedUser = await commandExecutor.Execute(command);
                var mappedAddedUser = mapper.Map<Core.Models.User>(addedUser);
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedAddedUser)
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

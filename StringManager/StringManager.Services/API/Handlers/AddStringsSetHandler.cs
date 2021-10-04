using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddStringsSetHandler : IRequestHandler<AddStringsSetRequest, StatusCodeResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<AddStringsSetHandler> logger;

        public AddStringsSetHandler(IMapper mapper,
                                    ICommandExecutor commandExecutor,
                                    ILogger<AddStringsSetHandler> logger)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(AddStringsSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError(request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to add a new StringsSet");
                    return new StatusCodeResponse()
                    {
                        Result = new UnauthorizedResult()
                    };
                }
                var stringsSetToAdd = mapper.Map<StringsSet>(request);
                var command = new AddStringsSetCommand()
                {
                    Parameter = stringsSetToAdd
                };
                var addedStringsSet = await commandExecutor.Execute(command);
                var mappedAddedStringsSet = mapper.Map<Core.Models.StringsSet>(addedStringsSet);
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedAddedStringsSet)
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

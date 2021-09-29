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
    public class AddStringsSetHandler : IRequestHandler<AddStringsSetRequest, AddStringsSetResponse>
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

        public async Task<AddStringsSetResponse> Handle(AddStringsSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to add string set");
                    return new AddStringsSetResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var stringsSetToAdd = mapper.Map<StringsSet>(request);
                var command = new AddStringsSetCommand()
                {
                    Parameter = stringsSetToAdd
                };
                var addedStringsSet = await commandExecutor.Execute(command);
                var mappedAddedStringsSet = mapper.Map<Core.Models.StringsSet>(addedStringsSet);
                return new AddStringsSetResponse()
                {
                    Data = mappedAddedStringsSet
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new AddStringsSetResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

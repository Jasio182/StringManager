using AutoMapper;
using MediatR;
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
    public class AddStringsSetHandler : IRequestHandler<AddStringsSetRequest, StatusCodeResponse<Core.Models.StringsSet>>
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

        public async Task<StatusCodeResponse<Core.Models.StringsSet>> Handle(AddStringsSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to add a new StringsSet";
                    logger.LogError(error);
                    return new StatusCodeResponse<Core.Models.StringsSet>()
                    {
                        Result = new Core.Models.ModelActionResult<Core.Models.StringsSet>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var stringsSetToAdd = mapper.Map<StringsSet>(request);
                var command = new AddStringsSetCommand()
                {
                    Parameter = stringsSetToAdd
                };
                var addedStringsSet = await commandExecutor.Execute(command);
                var mappedAddedStringsSet = mapper.Map<Core.Models.StringsSet>(addedStringsSet);
                return new StatusCodeResponse<Core.Models.StringsSet>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.StringsSet>((int)HttpStatusCode.OK, mappedAddedStringsSet)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing adding new StringsSet item; exeception:" + e + " message: " + e.Message;
                logger.LogError(e, error);
                return new StatusCodeResponse<Core.Models.StringsSet>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.StringsSet>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

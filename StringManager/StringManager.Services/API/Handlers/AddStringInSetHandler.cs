using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.Core.MediatorRequestsAndResponses;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class AddStringInSetHandler : IRequestHandler<AddStringInSetRequest, StatusCodeResponse<Core.Models.StringInSet>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<AddStringInSetHandler> logger;

        public AddStringInSetHandler(IQueryExecutor queryExecutor,
                                     IMapper mapper,
                                     ICommandExecutor commandExecutor,
                                     ILogger<AddStringInSetHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<Core.Models.StringInSet>> Handle(AddStringInSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to add a new StringInSet";
                    logger.LogError(error);
                    return new StatusCodeResponse<Core.Models.StringInSet>()
                    {
                        Result = new Core.Models.ModelActionResult<Core.Models.StringInSet>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var queryStringsSet = new GetStringsSetQuery()
                {
                    Id = request.StringsSetId
                };
                var stringsSetFromDb = await queryExecutor.Execute(queryStringsSet);
                if (stringsSetFromDb == null)
                {
                    string error = "StringsSet of given Id: " + request.StringsSetId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<Core.Models.StringInSet>()
                    {
                        Result = new Core.Models.ModelActionResult<Core.Models.StringInSet>((int)HttpStatusCode.BadRequest, null, error)
                    };
                }
                var queryString = new GetStringQuery()
                {
                    Id = request.StringId
                };
                var stringFromDb = await queryExecutor.Execute(queryString);
                if (stringFromDb == null)
                {
                    string error = "String of given Id: " + request.StringId + " has not been found";
                    logger.LogError(error);
                    return new StatusCodeResponse<Core.Models.StringInSet>()
                    {
                        Result = new Core.Models.ModelActionResult<Core.Models.StringInSet>((int)HttpStatusCode.BadRequest, null, error)
                    };
                }
                var stringInSetToAdd = mapper.Map<StringInSet>(
                    new System.Tuple<AddStringInSetRequest, StringsSet, String>(request, stringsSetFromDb, stringFromDb));
                var command = new AddStringInSetCommand()
                {
                    Parameter = stringInSetToAdd
                };
                var addedStringInSet = await commandExecutor.Execute(command);
                var mappedAddedStringInSet = mapper.Map<Core.Models.StringInSet>(addedStringInSet);
                return new StatusCodeResponse<Core.Models.StringInSet>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.StringInSet>((int)HttpStatusCode.OK, mappedAddedStringInSet)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing adding new StringInSet item";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<Core.Models.StringInSet>()
                {
                    Result = new Core.Models.ModelActionResult<Core.Models.StringInSet>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

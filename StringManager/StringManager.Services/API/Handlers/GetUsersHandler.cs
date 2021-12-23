using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, StatusCodeResponse<List<User>>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetUsersHandler> logger;

        public GetUsersHandler(IQueryExecutor queryExecutor,
                               IMapper mapper,
                               ILogger<GetUsersHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<List<User>>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to get list of all User items";
                    logger.LogError(error);
                    return new StatusCodeResponse<List<User>>()
                    {
                        Result = new ModelActionResult<List<User>>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var query = new GetUsersQuery()
                {
                    Type = request.Type
                };
                var usersFromDb = await queryExecutor.Execute(query);
                var mappedUsersFromDb = mapper.Map<List<User>>(usersFromDb);
                return new StatusCodeResponse<List<User>>()
                {
                    Result = new ModelActionResult<List<User>>((int)HttpStatusCode.OK, mappedUsersFromDb)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing getting list of User items";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<List<User>>()
                {
                    Result = new ModelActionResult<List<User>>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

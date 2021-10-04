using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, StatusCodeResponse>
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

        public async Task<StatusCodeResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError(request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to get list of Users");
                    return new StatusCodeResponse()
                    {
                        Result = new UnauthorizedResult()
                    };
                }
                var query = new GetUsersQuery()
                {
                    Type = request.Type
                };
                var usersFromDb = await queryExecutor.Execute(query);
                var mappedUsersFromDb = mapper.Map<List<Core.Models.User>>(usersFromDb);
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedUsersFromDb)
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

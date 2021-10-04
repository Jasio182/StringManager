using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, StatusCodeResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetUserHandler> logger;

        public GetUserHandler(IQueryExecutor queryExecutor,
                              IMapper mapper,
                              ILogger<GetUserHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetUserByIdQuery()
                {
                    Id = (int)request.UserId
                };
                var userFromDb = await queryExecutor.Execute(query);
                var mappedUserFromDb = mapper.Map<Core.Models.User>(userFromDb);
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedUserFromDb)
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

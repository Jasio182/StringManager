using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.Core.MediatorRequestsAndResponses;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, StatusCodeResponse<User>>
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

        public async Task<StatusCodeResponse<User>> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetUserByIdQuery()
                {
                    Id = (int)request.UserId
                };
                var userFromDb = await queryExecutor.Execute(query);
                var mappedUserFromDb = mapper.Map<User>(userFromDb);
                return new StatusCodeResponse<User>()
                {
                    Result = new ModelActionResult<User>((int)HttpStatusCode.OK, mappedUserFromDb)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing getting a User item";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<User>()
                {
                    Result = new ModelActionResult<User>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

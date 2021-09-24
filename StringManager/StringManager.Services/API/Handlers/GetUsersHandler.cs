using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using StringManager.Services.API.ErrorHandling;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
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

        public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " +request.UserId + " tried to get list of users");
                    return new GetUsersResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var query = new GetUsersQuery()
                {
                    Type = request.Type
                };
                var usersFromDb = await queryExecutor.Execute(query);
                var mappedUsersFromDb = mapper.Map<List<Core.Models.User>>(usersFromDb);
                return new GetUsersResponse()
                {
                    Data = mappedUsersFromDb
                };
            }
            catch(System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new GetUsersResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

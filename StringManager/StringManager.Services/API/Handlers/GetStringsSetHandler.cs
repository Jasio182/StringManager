using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public class GetStringsSetHandler : IRequestHandler<GetStringsSetRequest, StatusCodeResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetStringsSetHandler> logger;

        public GetStringsSetHandler(IQueryExecutor queryExecutor,
                                    IMapper mapper,
                                    ILogger<GetStringsSetHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(GetStringsSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetStringsSetQuery()
                {
                    Id = request.Id
                };
                var stringsSetFromDb = await queryExecutor.Execute(query);
                var mappedStringsSet = mapper.Map<StringsSet>(stringsSetFromDb);
                mappedStringsSet.StringsInSet = mapper.Map<List<StringInSet>>(stringsSetFromDb.StringsInSet);
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedStringsSet)
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

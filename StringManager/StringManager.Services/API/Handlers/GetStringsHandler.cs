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
    public class GetStringsHandler : IRequestHandler<GetStringsRequest, StatusCodeResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetStringsHandler> logger;

        public GetStringsHandler(IQueryExecutor queryExecutor,
                                 IMapper mapper,
                                 ILogger<GetStringsHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(GetStringsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetStringsQuery();
                var stringsFromDb = await queryExecutor.Execute(query);
                var mappedStrings = mapper.Map<List<String>>(stringsFromDb);
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedStrings)
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

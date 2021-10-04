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
    public class GetInstrumentsHandler : IRequestHandler<GetInstrumentsRequest, StatusCodeResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetInstrumentsHandler> logger;

        public GetInstrumentsHandler(IQueryExecutor queryExecutor,
                                     IMapper mapper,
                                     ILogger<GetInstrumentsHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(GetInstrumentsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetInstrumentsQuery();
                var instrumentsFromDb = await queryExecutor.Execute(query);
                var mappedInstruments = mapper.Map<List<Instrument>>(instrumentsFromDb);
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedInstruments)
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

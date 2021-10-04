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
    public class GetTuningHandler : IRequestHandler<GetTuningRequest, StatusCodeResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetTuningHandler> logger;

        public GetTuningHandler(IQueryExecutor queryExecutor,
                                IMapper mapper,
                                ILogger<GetTuningHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(GetTuningRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetTuningQuery()
                {
                    Id = request.Id
                };
                var tuningFromDb = await queryExecutor.Execute(query);
                var mappedTuningFromDb = mapper.Map<Core.Models.Tuning>(tuningFromDb);
                var mappedTonesInTuning = mapper.Map<List<Core.Models.ToneInTuning>>(tuningFromDb.TonesInTuning);
                mappedTuningFromDb.TonesInTuning = mappedTonesInTuning;
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedTuningFromDb)
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

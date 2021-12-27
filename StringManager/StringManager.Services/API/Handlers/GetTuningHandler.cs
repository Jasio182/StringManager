using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StringManager.Core.MediatorRequestsAndResponses;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Core.Models;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Queries;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class GetTuningHandler : IRequestHandler<GetTuningRequest, StatusCodeResponse<Tuning>>
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

        public async Task<StatusCodeResponse<Tuning>> Handle(GetTuningRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetTuningQuery()
                {
                    Id = request.Id
                };
                var tuningFromDb = await queryExecutor.Execute(query);
                Tuning mappedTuningFromDb = null;
                if (tuningFromDb != null)
                {
                    mappedTuningFromDb = mapper.Map<Tuning>(tuningFromDb);
                    var mappedTonesInTuning = mapper.Map<List<ToneInTuning>>(tuningFromDb.TonesInTuning);
                    mappedTuningFromDb.TonesInTuning = mappedTonesInTuning;
                }
                return new StatusCodeResponse<Tuning>()
                {
                    Result = new ModelActionResult<Tuning>((int)HttpStatusCode.OK, mappedTuningFromDb)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing getting a Tuning item";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<Tuning>()
                {
                    Result = new ModelActionResult<Tuning>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

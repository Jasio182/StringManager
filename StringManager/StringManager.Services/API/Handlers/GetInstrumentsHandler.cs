﻿using AutoMapper;
using MediatR;
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
    public class GetInstrumentsHandler : IRequestHandler<GetInstrumentsRequest, StatusCodeResponse<List<Instrument>>>
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

        public async Task<StatusCodeResponse<List<Instrument>>> Handle(GetInstrumentsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetInstrumentsQuery();
                var instrumentsFromDb = await queryExecutor.Execute(query);
                var mappedInstruments = mapper.Map<List<Instrument>>(instrumentsFromDb);
                return new StatusCodeResponse<List<Instrument>>()
                {
                    Result = new ModelActionResult<List<Instrument>>((int)HttpStatusCode.OK, mappedInstruments)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing getting list of Instrument items";
                logger.LogError(e, error+ "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<List<Instrument>>()
                {
                    Result = new ModelActionResult<List<Instrument>>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

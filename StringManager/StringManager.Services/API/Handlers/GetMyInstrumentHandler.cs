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
    public class GetMyInstrumentHandler : IRequestHandler<GetMyInstrumentRequest, StatusCodeResponse<MyInstrument>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetMyInstrumentHandler> logger;

        public GetMyInstrumentHandler(IQueryExecutor queryExecutor,
                                      IMapper mapper,
                                      ILogger<GetMyInstrumentHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<MyInstrument>> Handle(GetMyInstrumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetMyInstrumentQuery()
                {
                    Id = request.Id,
                    UserId = (int)request.UserId,
                    AccountType = (Core.Enums.AccountType)request.AccountType
                };
                var myInstrumentFromDb = await queryExecutor.Execute(query);
                MyInstrument mappedMyInstrument = null;
                if (myInstrumentFromDb != null)
                {
                    mappedMyInstrument = mapper.Map<MyInstrument>(myInstrumentFromDb);
                    mappedMyInstrument.InstalledStrings = mapper.Map<List<InstalledString>>(myInstrumentFromDb.InstalledStrings);
                }
                return new StatusCodeResponse<MyInstrument>()
                {
                    Result = new ModelActionResult<MyInstrument>((int)HttpStatusCode.OK, mappedMyInstrument)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing getting MyInstrument item";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<MyInstrument>()
                {
                    Result = new ModelActionResult<MyInstrument>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

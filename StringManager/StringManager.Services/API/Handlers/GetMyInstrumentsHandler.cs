﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.Core.Models;
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
    public class GetMyInstrumentsHandler : IRequestHandler<GetMyInstrumentsRequest, GetMyInstrumentsResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetMyInstrumentsHandler> logger;

        public GetMyInstrumentsHandler(IQueryExecutor queryExecutor,
                                       IMapper mapper,
                                       ILogger<GetMyInstrumentsHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<GetMyInstrumentsResponse> Handle(GetMyInstrumentsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.RequestUserId == null && request.AccountType != Core.Enums.AccountType.Admin)
                    request.RequestUserId = request.UserId;
                else if(request.RequestUserId != null && request.AccountType != Core.Enums.AccountType.Admin)
                {
                    logger.LogError("Non admin user with Id: " + request.UserId ?? "_unregistered_" + " tried to get myinstruments for user of ID " + request.RequestUserId);
                    return new GetMyInstrumentsResponse()
                    {
                        Error = new ErrorModel(ErrorType.Unauthorized)
                    };
                }
                var query = new GetMyInstrumentsQuery()
                {
                    UserId = request.RequestUserId
                };
                var myInstrumentsFromDb = await queryExecutor.Execute(query);
                var mappedMyInstruments = mapper.Map<List<MyInstrumentList>>(myInstrumentsFromDb);
                return new GetMyInstrumentsResponse()
                {
                    Data = mappedMyInstruments
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new GetMyInstrumentsResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

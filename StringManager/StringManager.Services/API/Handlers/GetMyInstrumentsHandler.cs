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
    public class GetMyInstrumentsHandler : IRequestHandler<GetMyInstrumentsRequest, StatusCodeResponse<List<MyInstrumentList>>>
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

        public async Task<StatusCodeResponse<List<MyInstrumentList>>> Handle(GetMyInstrumentsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.RequestUserId == null && request.AccountType != Core.Enums.AccountType.Admin)
                    request.RequestUserId = request.UserId;
                else if(request.RequestUserId != null && request.AccountType != Core.Enums.AccountType.Admin)
                {
                    var error = request.UserId == null ? "NonAdmin User of Id: " + request.UserId : "Unregistered user" + " tried to get all MyInstruments of a user: " + request.RequestUserId;
                    logger.LogError(error);
                    return new StatusCodeResponse<List<MyInstrumentList>>()
                    {
                        Result = new ModelActionResult<List<MyInstrumentList>>((int)HttpStatusCode.Unauthorized, null, error)
                    };
                }
                var query = new GetMyInstrumentsQuery()
                {
                    UserId = request.RequestUserId
                };
                var myInstrumentsFromDb = await queryExecutor.Execute(query);
                var mappedMyInstruments = mapper.Map<List<MyInstrumentList>>(myInstrumentsFromDb);
                return new StatusCodeResponse<List<MyInstrumentList>>()
                {
                    Result = new ModelActionResult<List<MyInstrumentList>>((int)HttpStatusCode.OK, mappedMyInstruments)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing getting list of MyInstrument items; exeception:" + e + " message: " + e.Message;
                logger.LogError(e, error);
                return new StatusCodeResponse<List<MyInstrumentList>>()
                {
                    Result = new ModelActionResult<List<MyInstrumentList>>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

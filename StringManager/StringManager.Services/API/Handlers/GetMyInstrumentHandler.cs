using AutoMapper;
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
    public class GetMyInstrumentHandler : IRequestHandler<GetMyInstrumentRequest, GetMyInstrumentResponse>
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

        public async Task<GetMyInstrumentResponse> Handle(GetMyInstrumentRequest request, CancellationToken cancellationToken)
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
                var mappedMyInstrument = mapper.Map<MyInstrument>(myInstrumentFromDb);
                mappedMyInstrument.InstalledStrings = mapper.Map<List<InstalledString>>(myInstrumentFromDb.InstalledStrings);
                return new GetMyInstrumentResponse()
                {
                    Data = mappedMyInstrument
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new GetMyInstrumentResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

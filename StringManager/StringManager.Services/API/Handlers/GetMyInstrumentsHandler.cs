using AutoMapper;
using MediatR;
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

        public GetMyInstrumentsHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
        }

        public async Task<GetMyInstrumentsResponse> Handle(GetMyInstrumentsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetMyInstrumentsQuery()
                {
                    UserId = request.UserId
                };
                var myInstrumentsFromDb = await queryExecutor.Execute(query);
                var mappedMyInstruments = mapper.Map<List<MyInstrumentList>>(myInstrumentsFromDb);
                return new GetMyInstrumentsResponse()
                {
                    Data = mappedMyInstruments
                };
            }
            catch (System.Exception)
            {
                return new GetMyInstrumentsResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

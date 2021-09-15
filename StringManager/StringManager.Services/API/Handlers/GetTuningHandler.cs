using AutoMapper;
using MediatR;
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
    public class GetTuningHandler : IRequestHandler<GetTuningRequest, GetTuningResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;

        public GetTuningHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
        }

        public async Task<GetTuningResponse> Handle(GetTuningRequest request, CancellationToken cancellationToken)
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
                return new GetTuningResponse()
                {
                    Data = mappedTuningFromDb
                };
            }
            catch (System.Exception)
            {
                return new GetTuningResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

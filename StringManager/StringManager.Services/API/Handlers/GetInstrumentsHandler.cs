﻿using AutoMapper;
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
    public class GetInstrumentsHandler : IRequestHandler<GetInstrumentsRequest, GetInstrumentsResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;

        public GetInstrumentsHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
        }

        public async Task<GetInstrumentsResponse> Handle(GetInstrumentsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetInstrumentsQuery();
                var instrumentsFromDb = await queryExecutor.Execute(query);
                var mappedInstruments = mapper.Map<List<Instrument>>(instrumentsFromDb);
                return new GetInstrumentsResponse()
                {
                    Data = mappedInstruments
                };
            }
            catch (System.Exception)
            {
                return new GetInstrumentsResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

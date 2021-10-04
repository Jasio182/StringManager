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
    class GetInstrumentsManufacturersHandler : IRequestHandler<GetInstrumentsManufacturersRequest, StatusCodeResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetInstrumentsManufacturersHandler> logger;

        public GetInstrumentsManufacturersHandler(IQueryExecutor queryExecutor,
                                                  IMapper mapper,
                                                  ILogger<GetInstrumentsManufacturersHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse> Handle(GetInstrumentsManufacturersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetInstrumentsManufacturersQuery();
                var instrumentsManufacturersFromDb = await queryExecutor.Execute(query);
                var mappedInstrumentsManufacturers = mapper.Map<List<Manufacturer>>(instrumentsManufacturersFromDb);
                return new StatusCodeResponse()
                {
                    Result = new OkObjectResult(mappedInstrumentsManufacturers)
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

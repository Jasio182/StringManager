using AutoMapper;
using MediatR;
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
    class GetInstrumentsManufacturersHandler : IRequestHandler<GetInstrumentsManufacturersRequest, StatusCodeResponse<List<Manufacturer>>>
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

        public async Task<StatusCodeResponse<List<Manufacturer>>> Handle(GetInstrumentsManufacturersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetInstrumentsManufacturersQuery();
                var instrumentsManufacturersFromDb = await queryExecutor.Execute(query);
                var mappedInstrumentsManufacturers = mapper.Map<List<Manufacturer>>(instrumentsManufacturersFromDb);
                return new StatusCodeResponse<List<Manufacturer>>()
                {
                    Result = new ModelActionResult<List<Manufacturer>>((int)HttpStatusCode.OK, mappedInstrumentsManufacturers)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing getting list of Manufacturer items that have connected Instrument items to it; exeception:" + e + " message: " + e.Message;
                logger.LogError(e, error);
                return new StatusCodeResponse<List<Manufacturer>>()
                {
                    Result = new ModelActionResult<List<Manufacturer>>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

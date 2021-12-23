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
    public class GetStringsManufacturersHandler : IRequestHandler<GetStringsManufacturersRequest, StatusCodeResponse<List<Manufacturer>>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetStringsManufacturersHandler> logger;

        public GetStringsManufacturersHandler(IQueryExecutor queryExecutor,
                                              IMapper mapper,
                                              ILogger<GetStringsManufacturersHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<List<Manufacturer>>> Handle(GetStringsManufacturersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetStringsManufacturersQuery();
                var stringsManufacturersFromDb = await queryExecutor.Execute(query);
                var mappedStringsManufacturers = mapper.Map<List<Manufacturer>>(stringsManufacturersFromDb);
                return new StatusCodeResponse<List<Manufacturer>>()
                {
                    Result = new ModelActionResult<List<Manufacturer>>((int)HttpStatusCode.OK, mappedStringsManufacturers)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing getting list of Manufacturer items that have connected String items to it";
                logger.LogError(e, error + "; exeception:" + e + " message: " + e.Message);
                return new StatusCodeResponse<List<Manufacturer>>()
                {
                    Result = new ModelActionResult<List<Manufacturer>>((int)HttpStatusCode.InternalServerError, null, error)
                };
            }
        }
    }
}

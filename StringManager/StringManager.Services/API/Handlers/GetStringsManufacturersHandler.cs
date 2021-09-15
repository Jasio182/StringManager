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
    public class GetStringsManufacturersHandler : IRequestHandler<GetStringsManufacturersRequest, GetStringsManufacturersResponse>
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

        public async Task<GetStringsManufacturersResponse> Handle(GetStringsManufacturersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetStringsManufacturersQuery();
                var stringsManufacturersFromDb = await queryExecutor.Execute(query);
                var mappedStringsManufacturers = mapper.Map<List<Manufacturer>>(stringsManufacturersFromDb);
                return new GetStringsManufacturersResponse()
                {
                    Data = mappedStringsManufacturers
                };
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "Exception has occured");
                return new GetStringsManufacturersResponse()
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }
        }
    }
}

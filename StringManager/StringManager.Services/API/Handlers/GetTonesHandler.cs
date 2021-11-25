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
    class GetTonesHandler : IRequestHandler<GetTonesRequest, StatusCodeResponse<List<Tone>>>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ILogger<GetTonesHandler> logger;

        public GetTonesHandler(IQueryExecutor queryExecutor,
                               IMapper mapper,
                               ILogger<GetTonesHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<StatusCodeResponse<List<Tone>>> Handle(GetTonesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetTonesQuery();
                var tonesFromDb = await queryExecutor.Execute(query);
                var mappedTones = mapper.Map<List<Tone>>(tonesFromDb);
                return new StatusCodeResponse<List<Tone>>()
                {
                    Result = new ModelActionResult<List<Tone>>((int)HttpStatusCode.OK, mappedTones)
                };
            }
            catch (System.Exception e)
            {
                var error = "Exception has occured during proccesing getting list of Tone items; exeception:" + e + " message: " + e.Message;
                logger.LogError(e, error);
                return new StatusCodeResponse<List<Tone>>()
                {
                    Result = new ModelActionResult<List<Tone>>((int)HttpStatusCode.OK, null, error)
                };
            }
        }
    }
}

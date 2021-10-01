using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StringManager.DataAccess.CQRS;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class ModifyToneInTuningHandler : IRequestHandler<ModifyToneInTuningRequest, ModifyToneInTuningResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly ILogger<ModifyToneInTuningHandler> logger;

        public ModifyToneInTuningHandler(IQueryExecutor queryExecutor,
                                         IMapper mapper,
                                         ICommandExecutor commandExecutor,
                                         ILogger<ModifyToneInTuningHandler> logger)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        public Task<ModifyToneInTuningResponse> Handle(ModifyToneInTuningRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}

using AutoMapper;
using MediatR;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    public class ModifyInstalledStringHandler : IRequestHandler<ModifyInstalledStringRequest, ModifyInstalledStringResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;

        public ModifyInstalledStringHandler(IQueryExecutor queryExecutor, IMapper mapper, ICommandExecutor commandExecutor)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
        }

        public async Task<ModifyInstalledStringResponse> Handle(ModifyInstalledStringRequest request, CancellationToken cancellationToken)
        {
            var installedStringQuery = new GetInstalledStringQuery()
            {
                Id = request.Id
            };
            var installedStringFromDb = await queryExecutor.Execute(installedStringQuery);
            var stringQuery = new GetStringQuery()
            {
                Id = request.StringId
            };
            var stringFromDb = await queryExecutor.Execute(stringQuery);
            var toneQuery = new GetToneQuery()
            {
                Id = request.ToneId
            };
            var toneFromDb = await queryExecutor.Execute(toneQuery);
            var installedStringToUpdate = installedStringFromDb;
            installedStringToUpdate.String = stringFromDb;
            installedStringToUpdate.Tone = toneFromDb;
            var command = new ModifyInstalledStringCommand()
            {
                Parameter = installedStringToUpdate
            };
            var modifiedInstalledString = await commandExecutor.Execute(command);
            var mappedModifiedInstalledString = mapper.Map<Core.Models.InstalledString>(modifiedInstalledString);
            return new ModifyInstalledStringResponse()
            {
                Data = mappedModifiedInstalledString
            };
        }
    }
}

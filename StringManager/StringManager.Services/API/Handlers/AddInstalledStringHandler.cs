﻿using AutoMapper;
using MediatR;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    class AddInstalledStringHandler : IRequestHandler<AddInstalledStringRequest, AddInstalledStringResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;

        public AddInstalledStringHandler(IQueryExecutor queryExecutor, IMapper mapper, ICommandExecutor commandExecutor)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
        }

        public async Task<AddInstalledStringResponse> Handle(AddInstalledStringRequest request, CancellationToken cancellationToken)
        {
            var queryString = new GetStringQuery()
            {
                Id = request.StringId
            };
            var stringFromDb = await queryExecutor.Execute(queryString);
            var queryTone = new GetToneQuery()
            {
                Id = request.ToneId
            };
            var toneFromDb = await queryExecutor.Execute(queryTone);
            var queryMyInstrument = new GetMyInstrumentQuery()
            {
                Id = request.MyInstrumentId
            };
            var myInstrumentFromDb = await queryExecutor.Execute(queryMyInstrument);
            var installedStringToAdd = new InstalledString()
            {
                MyInstrument = myInstrumentFromDb,
                String = stringFromDb,
                Tone = toneFromDb,
                Position = request.Position
            };
            var command = new AddInstalledStringCommand()
            {
                Parameter = installedStringToAdd
            };
            var addedInstalledString = await commandExecutor.Execute(command);
            var mappedAddedInstalledString = mapper.Map<Core.Models.InstalledString>(addedInstalledString);
            return new AddInstalledStringResponse()
            {
                Data = mappedAddedInstalledString
            };
        }
    }
}
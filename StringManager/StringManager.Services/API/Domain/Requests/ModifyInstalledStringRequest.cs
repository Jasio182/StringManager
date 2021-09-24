using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class ModifyInstalledStringRequest : RequestBase<ModifyInstalledStringResponse>
    {
        public int Id { get; set; }

        public int? StringId { get; set; }

        public int? ToneId { get; set; }
    }
}

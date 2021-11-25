using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class RemoveToneInTuningRequest : RequestBase<ToneInTuning>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}

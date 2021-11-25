using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class RemoveStringsSetRequest : RequestBase<StringsSet>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetStringsSetRequest : RequestBase<StringsSet>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}

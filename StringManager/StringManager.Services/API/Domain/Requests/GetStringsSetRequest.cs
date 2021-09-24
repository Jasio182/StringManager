using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetStringsSetRequest : RequestBase<GetStringsSetResponse>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}

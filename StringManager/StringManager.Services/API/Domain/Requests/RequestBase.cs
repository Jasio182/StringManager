using MediatR;
using StringManager.Core.Enums;

namespace StringManager.Services.API.Domain.Requests
{
    public abstract class RequestBase<TResponse> : IRequest<TResponse>
    {
        public int? UserId;

        public AccountType? AccountType;
    }
}

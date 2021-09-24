using MediatR;
using StringManager.Core.Enums;

namespace StringManager.Services.API.Domain.Requests
{
    public abstract class RequestBase<TRequest> : IRequest<TRequest>
    {
        public int? UserId;

        public AccountType? AccountType;
    }
}

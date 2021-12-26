using MediatR;
using StringManager.Core.Enums;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public abstract class RequestBase<T> : IRequest<StatusCodeResponse<T>>
    {
        public int? UserId;

        public AccountType? AccountType;
    }
}

using MediatR;
using StringManager.Core.Enums;

namespace StringManager.Services.API.Domain.Requests
{
    public abstract class RequestBase<TRequest> : IRequest<TRequest>
    {
        public int? GetUserId()
        {
            return UserId;
        }

        public void SetUserId(int? userId)
        {
            UserId = userId;
        }

        private int? UserId;

        public AccountType? GetAccountType()
        {
            return AccountType;
        }
        public void SetAccountType(AccountType? accountType)
        {
            AccountType = accountType;
        }

        private AccountType? AccountType;
    }
}

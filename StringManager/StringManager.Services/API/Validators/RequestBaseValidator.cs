using FluentValidation;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.API.Validators
{
    public abstract class RequestBaseValidator<TRequest, T> : AbstractValidator<TRequest>
        where TRequest : RequestBase<T>
    {
        public RequestBaseValidator()
        {
            RuleFor(request => request.AccountType).IsInEnum();
            RuleFor(request => request.UserId).GreaterThan(0);
        }
    }
}

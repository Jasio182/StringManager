using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class RemoveStringInSetRequestValidatorTests
    {
        private RemoveStringInSetRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new RemoveStringInSetRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 12, 11)]
        [TestCase((Core.Enums.AccountType)0, 13, 21)]
        public void RemoveStringInSetRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, int id)
        {
            var testRemoveStringInSetRequest = new RemoveStringInSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Id = id
            };
            var result = validator.TestValidate(testRemoveStringInSetRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, -1)]
        [TestCase((Core.Enums.AccountType)1, 12, -11)]
        [TestCase((Core.Enums.AccountType)0, 13, 0)]
        public void RemoveStringInSetRequestValidator_ShouldHaveIdError(Core.Enums.AccountType? accountType, int? userId, int id)
        {
            var testRemoveStringInSetRequest = new RemoveStringInSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Id = id
            };
            var result = validator.TestValidate(testRemoveStringInSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.Id);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)2, 1, 1)]
        [TestCase((Core.Enums.AccountType)(-1), 12, 11)]
        [TestCase((Core.Enums.AccountType)24, 13, 21)]
        public void RemoveStringInSetRequestValidator_ShouldHaveAccountTypeError(Core.Enums.AccountType? accountType, int? userId, int id)
        {
            var testRemoveStringInSetRequest = new RemoveStringInSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Id = id
            };
            var result = validator.TestValidate(testRemoveStringInSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, 1)]
        [TestCase((Core.Enums.AccountType)1, 0, 11)]
        [TestCase((Core.Enums.AccountType)0, -42, 21)]
        public void RemoveStringInSetRequestValidator_ShouldHaveUserIdError(Core.Enums.AccountType? accountType, int? userId, int id)
        {
            var testRemoveStringInSetRequest = new RemoveStringInSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Id = id
            };
            var result = validator.TestValidate(testRemoveStringInSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }
    }
}

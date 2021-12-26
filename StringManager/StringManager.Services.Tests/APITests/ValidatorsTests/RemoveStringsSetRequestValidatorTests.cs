using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class RemoveStringsSetRequestValidatorTests
    {
        private RemoveStringsSetRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new RemoveStringsSetRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 12, 11)]
        [TestCase((Core.Enums.AccountType)0, 13, 21)]
        public void RemoveStringsSetRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, int id)
        {
            var testRemoveStringsSetRequest = new RemoveStringsSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Id = id
            };
            var result = validator.TestValidate(testRemoveStringsSetRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, -1)]
        [TestCase((Core.Enums.AccountType)1, 12, -11)]
        [TestCase((Core.Enums.AccountType)0, 13, 0)]
        public void RemoveStringsSetRequestValidator_ShouldHaveIdError(Core.Enums.AccountType? accountType, int? userId, int id)
        {
            var testRemoveStringsSetRequest = new RemoveStringsSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Id = id
            };
            var result = validator.TestValidate(testRemoveStringsSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.Id);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)2, 1, 1)]
        [TestCase((Core.Enums.AccountType)(-1), 12, 11)]
        [TestCase((Core.Enums.AccountType)51, 13, 21)]
        public void RemoveStringsSetRequestValidator_ShouldHaveAccountTypeError(Core.Enums.AccountType? accountType, int? userId, int id)
        {
            var testRemoveStringsSetRequest = new RemoveStringsSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Id = id
            };
            var result = validator.TestValidate(testRemoveStringsSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, 1)]
        [TestCase((Core.Enums.AccountType)1, 0, 11)]
        [TestCase((Core.Enums.AccountType)0, -66, 21)]
        public void RemoveStringsSetRequestValidator_ShouldHaveUserIdError(Core.Enums.AccountType? accountType, int? userId, int id)
        {
            var testRemoveStringsSetRequest = new RemoveStringsSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Id = id
            };
            var result = validator.TestValidate(testRemoveStringsSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }
    }
}

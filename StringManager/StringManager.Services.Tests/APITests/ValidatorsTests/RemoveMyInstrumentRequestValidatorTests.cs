using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class RemoveMyInstrumentRequestValidatorTests
    {
        private RemoveMyInstrumentRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new RemoveMyInstrumentRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 12, 11)]
        [TestCase((Core.Enums.AccountType)0, 13, 21)]
        public void RemoveMyInstrumentRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, int id)
        {
            var testRemoveMyInstrumentRequest = new RemoveMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Id = id
            };
            var result = validator.TestValidate(testRemoveMyInstrumentRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, -1)]
        [TestCase((Core.Enums.AccountType)1, 12, -11)]
        [TestCase((Core.Enums.AccountType)0, 13, 0)]
        public void RemoveMyInstrumentRequestValidator_ShouldHaveIdError(Core.Enums.AccountType? accountType, int? userId, int id)
        {
            var testRemoveMyInstrumentRequest = new RemoveMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Id = id
            };
            var result = validator.TestValidate(testRemoveMyInstrumentRequest);
            result.ShouldHaveValidationErrorFor(request => request.Id);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)2, 1, 1)]
        [TestCase((Core.Enums.AccountType)(-1), 12, 11)]
        [TestCase((Core.Enums.AccountType)251, 13, 21)]
        public void RemoveMyInstrumentRequestValidator_ShouldHaveAccountTypeError(Core.Enums.AccountType? accountType, int? userId, int id)
        {
            var testRemoveMyInstrumentRequest = new RemoveMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Id = id
            };
            var result = validator.TestValidate(testRemoveMyInstrumentRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, 1)]
        [TestCase((Core.Enums.AccountType)1, 0, 11)]
        [TestCase((Core.Enums.AccountType)0, -72, 21)]
        public void RemoveMyInstrumentRequestValidator_ShouldHaveUserIdError(Core.Enums.AccountType? accountType, int? userId, int id)
        {
            var testRemoveMyInstrumentRequest = new RemoveMyInstrumentRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Id = id
            };
            var result = validator.TestValidate(testRemoveMyInstrumentRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }
    }
}

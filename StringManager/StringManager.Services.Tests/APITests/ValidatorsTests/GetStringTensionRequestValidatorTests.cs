using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class GetStringTensionRequestValidatorTests
    {
        private GetStringTensionRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new GetStringTensionRequestValidator();
        }

        [Test]
        [TestCase(null, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 21, 16, 632)]
        [TestCase((Core.Enums.AccountType)0, null, 18, 123, 42)]
        public void GetStringTensionRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, int toneId, int scaleLenght, int stringId)
        {
            var testUserRequest = new GetStringTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                StringId = stringId,
                ScaleLenght = scaleLenght,
                ToneId = toneId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase(null, 1, 1, 1, -1)]
        [TestCase((Core.Enums.AccountType)1, 2, 21, 16, -632)]
        [TestCase((Core.Enums.AccountType)0, null, 18, 123, 0)]
        public void GetStringTensionRequestValidator_ShouldHaveStringIdErrors(Core.Enums.AccountType? accountType, int? userId, int toneId, int scaleLenght, int stringId)
        {
            var testUserRequest = new GetStringTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                StringId = stringId,
                ScaleLenght = scaleLenght,
                ToneId = toneId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.StringId);
        }

        [Test]
        [TestCase(null, 1, 1, -1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 21, -16, 632)]
        [TestCase((Core.Enums.AccountType)0, null, 18, 0, 42)]
        public void GetStringTensionRequestValidator_ShouldHaveScaleLenghtErrors(Core.Enums.AccountType? accountType, int? userId, int toneId, int scaleLenght, int stringId)
        {
            var testUserRequest = new GetStringTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                StringId = stringId,
                ScaleLenght = scaleLenght,
                ToneId = toneId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.ScaleLenght);
        }

        [Test]
        [TestCase(null, 1, -1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 0, 16, 632)]
        [TestCase((Core.Enums.AccountType)0, null, -18, 123, 42)]
        public void GetStringTensionRequestValidator_ShouldHaveToneIdErrors(Core.Enums.AccountType? accountType, int? userId, int toneId, int scaleLenght, int stringId)
        {
            var testUserRequest = new GetStringTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                StringId = stringId,
                ScaleLenght = scaleLenght,
                ToneId = toneId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.ToneId);
        }

        [Test]
        [TestCase(null, -1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, -2, 21, 16, 632)]
        [TestCase((Core.Enums.AccountType)0, 0, 18, 123, 42)]
        public void GetStringTensionRequestValidator_ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, int toneId, int scaleLenght, int stringId)
        {
            var testUserRequest = new GetStringTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                StringId = stringId,
                ScaleLenght = scaleLenght,
                ToneId = toneId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)(-1), 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)11, 2, 21, 16, 632)]
        [TestCase((Core.Enums.AccountType)67, null, 18, 123, 42)]
        public void GetStringTensionRequestValidator_ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, int toneId, int scaleLenght, int stringId)
        {
            var testUserRequest = new GetStringTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                StringId = stringId,
                ScaleLenght = scaleLenght,
                ToneId = toneId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

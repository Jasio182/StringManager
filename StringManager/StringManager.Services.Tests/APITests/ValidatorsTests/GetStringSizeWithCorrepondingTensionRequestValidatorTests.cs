using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class GetStringSizeWithCorrepondingTensionRequestValidatorTests
    {
        private GetStringSizeWithCorrepondingTensionRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new GetStringSizeWithCorrepondingTensionRequestValidator();
        }

        [Test]
        [TestCase(null, 1, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 21, 16, 632, 65)]
        [TestCase((Core.Enums.AccountType)0, null, 18, 123, 42, 22)]
        public void ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, int primaryToneId, int scaleLenght, int resultToneId, int stringId)
        {
            var testUserRequest = new GetStringSizeWithCorrepondingTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                PrimaryToneId = primaryToneId,
                ResultToneId = resultToneId,
                ScaleLength = scaleLenght,
                StringId = stringId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase(null, 1, -1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, -21, 16, 632, 65)]
        [TestCase((Core.Enums.AccountType)0, null, 0, 123, 42, 22)]
        public void ShouldHavePrimaryToneIdErrors(Core.Enums.AccountType? accountType, int? userId, int primaryToneId, int scaleLenght, int resultToneId, int stringId)
        {
            var testUserRequest = new GetStringSizeWithCorrepondingTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                PrimaryToneId = primaryToneId,
                ResultToneId = resultToneId,
                ScaleLength = scaleLenght,
                StringId = stringId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.PrimaryToneId);
        }

        [Test]
        [TestCase(null, 1, 1, -1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 21, -16, 632, 65)]
        [TestCase((Core.Enums.AccountType)0, null, 18, 0, 123, 22)]
        public void ShouldHaveResultToneIdErrors(Core.Enums.AccountType? accountType, int? userId, int primaryToneId, int resultToneId, int scaleLenght, int stringId)
        {
            var testUserRequest = new GetStringSizeWithCorrepondingTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                PrimaryToneId = primaryToneId,
                ResultToneId = resultToneId,
                ScaleLength = scaleLenght,
                StringId = stringId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.ResultToneId);
        }

        [Test]
        [TestCase(null, 1, 1, 1, -1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 21, 16, -632, 65)]
        [TestCase((Core.Enums.AccountType)0, null, 18, 123, 0, 22)]
        public void ShouldHaveScaleLengthErrors(Core.Enums.AccountType? accountType, int? userId, int primaryToneId, int resultToneId, int scaleLenght, int stringId)
        {
            var testUserRequest = new GetStringSizeWithCorrepondingTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                PrimaryToneId = primaryToneId,
                ResultToneId = resultToneId,
                ScaleLength = scaleLenght,
                StringId = stringId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.ScaleLength);
        }

        [Test]
        [TestCase(null, 1, 1, 1, 1, -1)]
        [TestCase((Core.Enums.AccountType)1, 2, 21, 16, 632, 0)]
        [TestCase((Core.Enums.AccountType)0, null, 18, 123, 42, -22)]
        public void ShouldHaveStringIdErrors(Core.Enums.AccountType? accountType, int? userId, int primaryToneId, int scaleLenght, int resultToneId, int stringId)
        {
            var testUserRequest = new GetStringSizeWithCorrepondingTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                PrimaryToneId = primaryToneId,
                ResultToneId = resultToneId,
                ScaleLength = scaleLenght,
                StringId = stringId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.StringId);
        }

        [Test]
        [TestCase(null, -1, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 0, 21, 16, 632, 65)]
        [TestCase((Core.Enums.AccountType)0, -2, 18, 123, 42, 22)]
        public void ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, int primaryToneId, int scaleLenght, int resultToneId, int stringId)
        {
            var testUserRequest = new GetStringSizeWithCorrepondingTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                PrimaryToneId = primaryToneId,
                ResultToneId = resultToneId,
                ScaleLength = scaleLenght,
                StringId = stringId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)11, 1, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)2, 2, 21, 16, 632, 65)]
        [TestCase((Core.Enums.AccountType)(-1), null, 18, 123, 42, 22)]
        public void ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, int primaryToneId, int scaleLenght, int resultToneId, int stringId)
        {
            var testUserRequest = new GetStringSizeWithCorrepondingTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                PrimaryToneId = primaryToneId,
                ResultToneId = resultToneId,
                ScaleLength = scaleLenght,
                StringId = stringId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

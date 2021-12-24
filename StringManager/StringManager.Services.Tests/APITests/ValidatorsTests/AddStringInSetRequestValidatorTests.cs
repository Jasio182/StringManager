using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class AddStringInSetRequestValidatorTests
    {
        private AddStringInSetRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new AddStringInSetRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 4, 123, 154)]
        [TestCase((Core.Enums.AccountType)0, 3, 6, 51, 21)]
        public void AddStringInSetRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, int position, int stringId, int stringSetId)
        {
            var testStringInSetRequest = new AddStringInSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                StringId = stringId,
                StringsSetId = stringSetId
            };
            var result = validator.TestValidate(testStringInSetRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, -1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, -4, 123, 154)]
        [TestCase((Core.Enums.AccountType)0, 3, 0, 51, 21)]
        public void AddStringInSetRequestValidator_ShouldHavePositionErrors(Core.Enums.AccountType? accountType, int? userId, int position, int stringId, int stringSetId)
        {
            var testStringInSetRequest = new AddStringInSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                StringId = stringId,
                StringsSetId = stringSetId
            };
            var result = validator.TestValidate(testStringInSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.Position);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, -1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 4, -123, 154)]
        [TestCase((Core.Enums.AccountType)0, 3, 6, 0, 21)]
        public void AddStringInSetRequestValidator_ShouldHaveStringIdErrors(Core.Enums.AccountType? accountType, int? userId, int position, int stringId, int stringSetId)
        {
            var testStringInSetRequest = new AddStringInSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                StringId = stringId,
                StringsSetId = stringSetId
            };
            var result = validator.TestValidate(testStringInSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.StringId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, -1)]
        [TestCase((Core.Enums.AccountType)1, 2, 4, 123, -154)]
        [TestCase((Core.Enums.AccountType)0, 3, 6, 51, 0)]
        public void AddStringInSetRequestValidator_ShouldHaveStringsSetIdErrors(Core.Enums.AccountType? accountType, int? userId, int position, int stringId, int stringSetId)
        {
            var testStringInSetRequest = new AddStringInSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                StringId = stringId,
                StringsSetId = stringSetId
            };
            var result = validator.TestValidate(testStringInSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.StringsSetId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 0, 4, 123, 154)]
        [TestCase((Core.Enums.AccountType)0, null, 6, 51, 21)]
        public void AddStringInSetRequestValidator_ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, int position, int stringId, int stringSetId)
        {
            var testStringInSetRequest = new AddStringInSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                StringId = stringId,
                StringsSetId = stringSetId
            };
            var result = validator.TestValidate(testStringInSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)6, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)2, 2, 4, 123, 154)]
        [TestCase(null, 3, 6, 51, 21)]
        public void AddStringInSetRequestValidator_ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, int position, int stringId, int stringSetId)
        {
            var testStringInSetRequest = new AddStringInSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                StringId = stringId,
                StringsSetId = stringSetId
            };
            var result = validator.TestValidate(testStringInSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

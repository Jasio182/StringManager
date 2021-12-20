using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class ModifyStringInSetRequestValidatorTests
    {
        private ModifyStringInSetRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new ModifyStringInSetRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 2, 4, 123, 154)]
        [TestCase((Core.Enums.AccountType)0, 3, 3, 6, 51, 21)]
        public void ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, int id, int position, int stringId, int stringSetId)
        {
            var testStringInSetRequest = new ModifyStringInSetRequest()
            {
                Id = id,
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
        [TestCase((Core.Enums.AccountType)0, 1, 1, -1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 2, -4, 123, 154)]
        [TestCase((Core.Enums.AccountType)0, 3, 3, 0, 51, 21)]
        public void ShouldHavePositionErrors(Core.Enums.AccountType? accountType, int? userId, int id, int position, int stringId, int stringSetId)
        {
            var testStringInSetRequest = new ModifyStringInSetRequest()
            {
                Id = id,
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
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, -1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 2, 4, -123, 154)]
        [TestCase((Core.Enums.AccountType)0, 3, 3, 6, 0, 21)]
        public void ShouldHaveStringIdErrors(Core.Enums.AccountType? accountType, int? userId, int id, int position, int stringId, int stringSetId)
        {
            var testStringInSetRequest = new ModifyStringInSetRequest()
            {
                Id = id,
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
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 1, -1)]
        [TestCase((Core.Enums.AccountType)1, 2, 2, 4, 123, -154)]
        [TestCase((Core.Enums.AccountType)0, 3, 3, 6, 51, 0)]
        public void ShouldHaveStringsSetIdErrors(Core.Enums.AccountType? accountType, int? userId, int id, int position, int stringId, int stringSetId)
        {
            var testStringInSetRequest = new ModifyStringInSetRequest()
            {
                Id = id,
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
        [TestCase((Core.Enums.AccountType)0, 1, -1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, -2, 4, 123, 154)]
        [TestCase((Core.Enums.AccountType)0, 3, 0, 6, 51, 21)]
        public void ShouldHaveIdErrors(Core.Enums.AccountType? accountType, int? userId, int id, int position, int stringId, int stringSetId)
        {
            var testStringInSetRequest = new ModifyStringInSetRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                Position = position,
                StringId = stringId,
                StringsSetId = stringSetId
            };
            var result = validator.TestValidate(testStringInSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.Id);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 0, 2, 4, 123, 154)]
        [TestCase((Core.Enums.AccountType)0, null, 3, 6, 51, 21)]
        public void ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, int id, int position, int stringId, int stringSetId)
        {
            var testStringInSetRequest = new ModifyStringInSetRequest()
            {
                Id = id,
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
        [TestCase((Core.Enums.AccountType)6, 1, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)(-1), 2, 2, 4, 123, 154)]
        [TestCase(null, 3, 3, 6, 51, 21)]
        public void ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, int id, int position, int stringId, int stringSetId)
        {
            var testStringInSetRequest = new ModifyStringInSetRequest()
            {
                Id = id,
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

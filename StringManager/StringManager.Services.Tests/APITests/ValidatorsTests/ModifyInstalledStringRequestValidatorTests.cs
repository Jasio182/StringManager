using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class ModifyInstalledStringRequestValidatorTests
    {
        private ModifyInstalledStringRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new ModifyInstalledStringRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0,1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1,12, 11, 6, 2)]
        [TestCase((Core.Enums.AccountType)0,13, 21, 55, 3)]
        public void ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int id, int stringId, int toneId, int? userId)
        {
            var testModifyInstalledStringRequest = new ModifyInstalledStringRequest()
            {
                Id = id,
                AccountType = accountType,
                StringId = stringId,
                ToneId = toneId,
                UserId = userId
            };
            var result = validator.TestValidate(testModifyInstalledStringRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, -1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 12, -11, 6, 2)]
        [TestCase((Core.Enums.AccountType)0, 13, -21, 55, 3)]
        public void ShouldHaveStringIdError(Core.Enums.AccountType? accountType, int id, int stringId, int toneId, int? userId)
        {
            var testModifyInstalledStringRequest = new ModifyInstalledStringRequest()
            {
                Id = id,
                AccountType = accountType,
                StringId = stringId,
                ToneId = toneId,
                UserId = userId
            };
            var result = validator.TestValidate(testModifyInstalledStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.StringId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, -1)]
        [TestCase((Core.Enums.AccountType)1, 12, 11, 6, 0)]
        [TestCase((Core.Enums.AccountType)0, 13, 21, 55, null)]
        public void ShouldHaveUserIdError(Core.Enums.AccountType? accountType, int id, int stringId, int toneId, int? userId)
        {
            var testModifyInstalledStringRequest = new ModifyInstalledStringRequest()
            {
                Id = id,
                AccountType = accountType,
                StringId = stringId,
                ToneId = toneId,
                UserId = userId
            };
            var result = validator.TestValidate(testModifyInstalledStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 0, 11, 6, 2)]
        [TestCase((Core.Enums.AccountType)0, -13, 21, 55, 3)]
        public void ShouldIdError(Core.Enums.AccountType? accountType, int id, int stringId, int toneId, int? userId)
        {
            var testModifyInstalledStringRequest = new ModifyInstalledStringRequest()
            {
                Id = id,
                AccountType = accountType,
                StringId = stringId,
                ToneId = toneId,
                UserId = userId
            };
            var result = validator.TestValidate(testModifyInstalledStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.Id);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)3, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)11, 12, 11, 6, 2)]
        [TestCase(null, 13, 21, 55, 3)]
        public void ShouldHaveAccountTypeError(Core.Enums.AccountType? accountType, int id,  int stringId, int toneId, int? userId)
        {
            var testModifyInstalledStringRequest = new ModifyInstalledStringRequest()
            {
                Id = id,
                AccountType = accountType,
                StringId = stringId,
                ToneId = toneId,
                UserId = userId
            };
            var result = validator.TestValidate(testModifyInstalledStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

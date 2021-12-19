using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    public class AddInstalledStringRequestValidatorTests
    {
        private AddInstalledStringRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new AddInstalledStringRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 15, 4, 11, 6, 2)]
        [TestCase((Core.Enums.AccountType)0, 6, 12, 21, 55, 3)]
        public void ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int myInstrumentId, int position, int stringId, int toneId, int? userId)
        {
            var testAddInstalledStringRequest = new AddInstalledStringRequest()
            {
                AccountType = accountType,
                MyInstrumentId = myInstrumentId,
                Position = position,
                StringId = stringId,
                ToneId = toneId,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstalledStringRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, -1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 15, -4, 11, 6, 2)]
        [TestCase((Core.Enums.AccountType)0, 6, -12, 21, 55, 3)]
        public void ShouldHavePositionError(Core.Enums.AccountType? accountType, int myInstrumentId, int position, int stringId, int toneId, int? userId)
        {
            var testAddInstalledStringRequest = new AddInstalledStringRequest()
            {
                AccountType = accountType,
                MyInstrumentId = myInstrumentId,
                Position = position,
                StringId = stringId,
                ToneId = toneId,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstalledStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.Position);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, -15, 4, 11, 6, 2)]
        [TestCase((Core.Enums.AccountType)0, -6, 12, 21, 55, 3)]
        public void ShouldHaveMyInstrumentIdError(Core.Enums.AccountType? accountType, int myInstrumentId, int position, int stringId, int toneId, int? userId)
        {
            var testAddInstalledStringRequest = new AddInstalledStringRequest()
            {
                AccountType = accountType,
                MyInstrumentId = myInstrumentId,
                Position = position,
                StringId = stringId,
                ToneId = toneId,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstalledStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.MyInstrumentId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, -1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 15, 4, -11, 6, 2)]
        [TestCase((Core.Enums.AccountType)0, 6, 12, -21, 55, 3)]
        public void ShouldHaveStringIdError(Core.Enums.AccountType? accountType, int myInstrumentId, int position, int stringId, int toneId, int? userId)
        {
            var testAddInstalledStringRequest = new AddInstalledStringRequest()
            {
                AccountType = accountType,
                MyInstrumentId = myInstrumentId,
                Position = position,
                StringId = stringId,
                ToneId = toneId,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstalledStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.StringId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 1, -1)]
        [TestCase((Core.Enums.AccountType)1, 15, 4, 11, 6, 0)]
        [TestCase((Core.Enums.AccountType)0, 6, 12, 21, 55, null)]
        public void ShouldHaveUserIdError(Core.Enums.AccountType? accountType, int myInstrumentId, int position, int stringId, int toneId, int? userId)
        {
            var testAddInstalledStringRequest = new AddInstalledStringRequest()
            {
                AccountType = accountType,
                MyInstrumentId = myInstrumentId,
                Position = position,
                StringId = stringId,
                ToneId = toneId,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstalledStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)3, 1, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)11, 15, 4, 11, 6, 2)]
        [TestCase(null, 6, 12, 21, 55, 3)]
        public void ShouldHaveAccountTypeError(Core.Enums.AccountType? accountType, int myInstrumentId, int position, int stringId, int toneId, int? userId)
        {
            var testAddInstalledStringRequest = new AddInstalledStringRequest()
            {
                AccountType = accountType,
                MyInstrumentId = myInstrumentId,
                Position = position,
                StringId = stringId,
                ToneId = toneId,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstalledStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

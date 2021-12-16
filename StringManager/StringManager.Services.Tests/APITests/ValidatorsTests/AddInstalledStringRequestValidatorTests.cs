using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    public class AddInstalledStringRequestValidatorTests
    {
        private AddInstalledStringRequestValidator validator;

        public AddInstalledStringRequestValidatorTests()
        {
            validator = new AddInstalledStringRequestValidator();
        }

        [Test]
        [TestCase(1, 1, 1, 1)]
        [TestCase(15, 4, 11, 6)]
        [TestCase(6, 12, 21, 55)]
        public void ShouldNotHaveAnyErrors(int myInstrumentId, int position, int stringId, int toneId)
        {
            var testAddInstalledStringRequest = new AddInstalledStringRequest()
            {
                AccountType = Core.Enums.AccountType.User,
                MyInstrumentId = myInstrumentId,
                Position = position,
                StringId = stringId,
                ToneId = toneId
            };
            var result = validator.TestValidate(testAddInstalledStringRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase(1, -1, 1, 1)]
        [TestCase(15, -4, 11, 6)]
        [TestCase(6, -12, 21, 55)]
        public void ShouldHavePositionError(int myInstrumentId, int position, int stringId, int toneId)
        {
            var testAddInstalledStringRequest = new AddInstalledStringRequest()
            {
                AccountType = Core.Enums.AccountType.User,
                MyInstrumentId = myInstrumentId,
                Position = position,
                StringId = stringId,
                ToneId = toneId
            };
            var result = validator.TestValidate(testAddInstalledStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.Position);
        }

        [Test]
        [TestCase(-1, 1, 1, 1)]
        [TestCase(-15, 4, 11, 6)]
        [TestCase(-6, 12, 21, 55)]
        public void ShouldHaveMyInstrumentIdError(int myInstrumentId, int position, int stringId, int toneId)
        {
            var testAddInstalledStringRequest = new AddInstalledStringRequest()
            {
                AccountType = Core.Enums.AccountType.User,
                MyInstrumentId = myInstrumentId,
                Position = position,
                StringId = stringId,
                ToneId = toneId
            };
            var result = validator.TestValidate(testAddInstalledStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.MyInstrumentId);
        }

        [Test]
        [TestCase(1, 1, -1, 1)]
        [TestCase(15, 4, -11, 6)]
        [TestCase(6, 12, -21, 55)]
        public void ShouldHaveStringIdError(int myInstrumentId, int position, int stringId, int toneId)
        {
            var testAddInstalledStringRequest = new AddInstalledStringRequest()
            {
                AccountType = Core.Enums.AccountType.User,
                MyInstrumentId = myInstrumentId,
                Position = position,
                StringId = stringId,
                ToneId = toneId
            };
            var result = validator.TestValidate(testAddInstalledStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.StringId);
        }

        [Test]
        [TestCase(1, 1, 1, 1, -5)]
        [TestCase(15, 4, 11, 6, -1)]
        [TestCase(6, 12, 21, 55, 0)]
        public void ShouldHaveUserIdError(int myInstrumentId, int position, int stringId, int toneId, int userId)
        {
            var testAddInstalledStringRequest = new AddInstalledStringRequest()
            {
                AccountType = Core.Enums.AccountType.User,
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
        [TestCase(4, 1, 1, 1, 1, 1)]
        [TestCase(-6, 15, 4, 11, 6, 1)]
        [TestCase(-1, 6, 12, 21, 55, 3)]
        public void ShouldHaveAccountTypeError(int accountType, int myInstrumentId, int position, int stringId, int toneId, int userId)
        {
            var testAddInstalledStringRequest = new AddInstalledStringRequest()
            {
                AccountType = (Core.Enums.AccountType)accountType,
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

using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    public class ModifyInstrumentRequestValidatorTests
    {
        private ModifyInstrumentRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new ModifyInstrumentRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 12, 11, 6, 2, 8, 12)]
        [TestCase((Core.Enums.AccountType)0, 13, 21, 55, 3, 9, 555)]
        public void ModifyInstrumentRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, int id, int manufacturerId, int scaleLenghtBass, int scaleLenghtTreble, int numberOfStrings)
        {
            var testModifyInstrumentRequest = new ModifyInstrumentRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testModifyInstrumentRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, -1, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 12, -11, 6, 2, 8, 12)]
        [TestCase((Core.Enums.AccountType)0, 13, 0, 55, 3, 9, 555)]
        public void ModifyInstrumentRequestValidator_ShouldHaveIdError(Core.Enums.AccountType? accountType, int? userId, int id, int manufacturerId, int scaleLenghtBass, int scaleLenghtTreble, int numberOfStrings)
        {
            var testModifyInstrumentRequest = new ModifyInstrumentRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testModifyInstrumentRequest);
            result.ShouldHaveValidationErrorFor(request => request.Id);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, -1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 12, 11, -6, 2, 8, 12)]
        [TestCase((Core.Enums.AccountType)0, 13, 21, 0, 3, 9, 555)]
        public void ModifyInstrumentRequestValidator_ShouldHaveManufacturerIdError(Core.Enums.AccountType? accountType, int? userId, int id, int manufacturerId, int scaleLenghtBass, int scaleLenghtTreble, int numberOfStrings)
        {
            var testModifyInstrumentRequest = new ModifyInstrumentRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testModifyInstrumentRequest);
            result.ShouldHaveValidationErrorFor(request => request.ManufacturerId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, -1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 12, 11, 6, -2, 8, 12)]
        [TestCase((Core.Enums.AccountType)0, 13, 21, 55, 0, 9, 555)]
        public void ModifyInstrumentRequestValidator_ShouldHaveScaleLenghtBassError(Core.Enums.AccountType? accountType, int? userId, int id, int manufacturerId, int scaleLenghtBass, int scaleLenghtTreble, int numberOfStrings)
        {
            var testModifyInstrumentRequest = new ModifyInstrumentRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testModifyInstrumentRequest);
            result.ShouldHaveValidationErrorFor(request => request.ScaleLenghtBass);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 1, -1, 1)]
        [TestCase((Core.Enums.AccountType)1, 12, 11, 6, 2, -8, 12)]
        [TestCase((Core.Enums.AccountType)0, 13, 21, 55, 3, 0, 555)]
        public void ModifyInstrumentRequestValidator_ShouldHaveScaleLenghtTrebleError(Core.Enums.AccountType? accountType, int? userId, int id, int manufacturerId, int scaleLenghtBass, int scaleLenghtTreble, int numberOfStrings)
        {
            var testModifyInstrumentRequest = new ModifyInstrumentRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testModifyInstrumentRequest);
            result.ShouldHaveValidationErrorFor(request => request.ScaleLenghtTreble);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 1, 1, -1)]
        [TestCase((Core.Enums.AccountType)1, 12, 11, 6, 2, 8, -12)]
        [TestCase((Core.Enums.AccountType)0, 13, 21, 55, 3, 9, 0)]
        public void ModifyInstrumentRequestValidator_ShouldHaveNumberOfStringsError(Core.Enums.AccountType? accountType, int? userId, int id, int manufacturerId, int scaleLenghtBass, int scaleLenghtTreble, int numberOfStrings)
        {
            var testModifyInstrumentRequest = new ModifyInstrumentRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testModifyInstrumentRequest);
            result.ShouldHaveValidationErrorFor(request => request.NumberOfStrings);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, 1, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 0, 11, 6, 2, 8, 12)]
        [TestCase((Core.Enums.AccountType)0, null, 21, 55, 3, 9, 555)]
        public void ModifyInstrumentRequestValidator_ShouldHaveUserIdError(Core.Enums.AccountType? accountType, int? userId, int id, int manufacturerId, int scaleLenghtBass, int scaleLenghtTreble, int numberOfStrings)
        {
            var testModifyInstrumentRequest = new ModifyInstrumentRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testModifyInstrumentRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)15, 1, 1, 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)(-1), 12, 11, 6, 2, 8, 12)]
        [TestCase(null, 13, 21, 55, 3, 9, 555)]
        public void ModifyInstrumentRequestValidator_ShouldHaveAccountTypeError(Core.Enums.AccountType? accountType, int? userId, int id, int manufacturerId, int scaleLenghtBass, int scaleLenghtTreble, int numberOfStrings)
        {
            var testModifyInstrumentRequest = new ModifyInstrumentRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testModifyInstrumentRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

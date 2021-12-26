using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class AddInstrumentRequestValidatorTests
    {
        private AddInstrumentRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new AddInstrumentRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 15, "test2", 4, 11, 6, 15)]
        [TestCase((Core.Enums.AccountType)0, 6, "test3", 12, 21, 55, 21)]
        public void AddInstrumentRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int manufacturerId, string model, int numberOfStrings, int scaleLenghtBass, int scaleLenghtTreble, int? userId)
        {
            var testAddInstrumentRequestRequest = new AddInstrumentRequest()
            {
                AccountType = accountType,
                ManufacturerId = manufacturerId,
                Model = model,
                NumberOfStrings = numberOfStrings,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstrumentRequestRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, "test1", 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, -15, "test2", 4, 11, 6, 15)]
        [TestCase((Core.Enums.AccountType)0, -6, "test3", 12, 21, 55, 21)]
        public void AddInstrumentRequestValidator_ShouldHaveManufacturerIdErrors(Core.Enums.AccountType? accountType, int manufacturerId, string model, int numberOfStrings, int scaleLenghtBass, int scaleLenghtTreble, int? userId)
        {
            var testAddInstrumentRequestRequest = new AddInstrumentRequest()
            {
                AccountType = accountType,
                ManufacturerId = manufacturerId,
                Model = model,
                NumberOfStrings = numberOfStrings,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.ManufacturerId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, null, 1, 1, 1, 1)]
        public void AddInstrumentRequestValidator_ShouldHaveModelError(Core.Enums.AccountType? accountType, int manufacturerId, string model, int numberOfStrings, int scaleLenghtBass, int scaleLenghtTreble, int? userId)
        {
            var testAddInstrumentRequestRequest = new AddInstrumentRequest()
            {
                AccountType = accountType,
                ManufacturerId = manufacturerId,
                Model = model,
                NumberOfStrings = numberOfStrings,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.Model);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", -1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 15, "test2", -4, 11, 6, 15)]
        [TestCase((Core.Enums.AccountType)0, 6, "test3", -12, 21, 55, 21)]
        public void AddInstrumentRequestValidator_ShouldHaveNumberOfStringsErrors(Core.Enums.AccountType? accountType, int manufacturerId, string model, int numberOfStrings, int scaleLenghtBass, int scaleLenghtTreble, int? userId)
        {
            var testAddInstrumentRequestRequest = new AddInstrumentRequest()
            {
                AccountType = accountType,
                ManufacturerId = manufacturerId,
                Model = model,
                NumberOfStrings = numberOfStrings,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.NumberOfStrings);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", 1, -1, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 15, "test2", 4, -11, 6, 15)]
        [TestCase((Core.Enums.AccountType)0, 6, "test3", 12, -21, 55, 21)]
        public void AddInstrumentRequestValidator_ShouldHaveScaleLenghtBassErrors(Core.Enums.AccountType? accountType, int manufacturerId, string model, int numberOfStrings, int scaleLenghtBass, int scaleLenghtTreble, int? userId)
        {
            var testAddInstrumentRequestRequest = new AddInstrumentRequest()
            {
                AccountType = accountType,
                ManufacturerId = manufacturerId,
                Model = model,
                NumberOfStrings = numberOfStrings,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.ScaleLenghtBass);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", 1, 1, -1, 1)]
        [TestCase((Core.Enums.AccountType)1, 15, "test2", 4, 11, -6, 15)]
        [TestCase((Core.Enums.AccountType)0, 6, "test3", 12, 21, -55, 21)]
        public void AddInstrumentRequestValidator_ShouldHaveScaleLenghtTrebleErrors(Core.Enums.AccountType? accountType, int manufacturerId, string model, int numberOfStrings, int scaleLenghtBass, int scaleLenghtTreble, int? userId)
        {
            var testAddInstrumentRequestRequest = new AddInstrumentRequest()
            {
                AccountType = accountType,
                ManufacturerId = manufacturerId,
                Model = model,
                NumberOfStrings = numberOfStrings,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.ScaleLenghtTreble);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", 1, 1, 1, -1)]
        [TestCase((Core.Enums.AccountType)1, 15, "test2", 4, 11, 6, null)]
        [TestCase((Core.Enums.AccountType)0, 6, "test3", 12, 21, 55, -21)]
        public void AddInstrumentRequestValidator_ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int manufacturerId, string model, int numberOfStrings, int scaleLenghtBass, int scaleLenghtTreble, int? userId)
        {
            var testAddInstrumentRequestRequest = new AddInstrumentRequest()
            {
                AccountType = accountType,
                ManufacturerId = manufacturerId,
                Model = model,
                NumberOfStrings = numberOfStrings,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }


        [Test]
        [TestCase((Core.Enums.AccountType)5, 1, "test1", 1, 1, 1, 1)]
        [TestCase((Core.Enums.AccountType)8, 15, "test2", 4, 11, 6, 15)]
        [TestCase(null, 6, "test3", 12, 21, 55, 21)]
        public void AddInstrumentRequestValidator_ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int manufacturerId, string model, int numberOfStrings, int scaleLenghtBass, int scaleLenghtTreble, int? userId)
        {
            var testAddInstrumentRequestRequest = new AddInstrumentRequest()
            {
                AccountType = accountType,
                ManufacturerId = manufacturerId,
                Model = model,
                NumberOfStrings = numberOfStrings,
                ScaleLenghtBass = scaleLenghtBass,
                ScaleLenghtTreble = scaleLenghtTreble,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

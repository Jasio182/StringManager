using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class AddManufacturerRequestValidatorTests
    {
        private AddManufacturerRequestValidator validator;
        
        [OneTimeSetUp]
        public void Setup()
        {
            validator = new AddManufacturerRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, "test1", 1)]
        [TestCase((Core.Enums.AccountType)1, "test2", 4)]
        [TestCase((Core.Enums.AccountType)0, "test3", 12)]
        public void AddManufacturerRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, string name, int? userId)
        {
            var testAddInstrumentRequestRequest = new AddManufacturerRequest()
            {
                Name = name,
                AccountType = accountType,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstrumentRequestRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, null, 1)]
        [TestCase((Core.Enums.AccountType)0, "", 1)]
        public void AddManufacturerRequestValidator_ShouldHaveNameError(Core.Enums.AccountType? accountType, string name, int? userId)
        {
            var testAddInstrumentRequestRequest = new AddManufacturerRequest()
            {
                Name = name,
                AccountType = accountType,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.Name);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, "test1", -1)]
        [TestCase((Core.Enums.AccountType)1, "test2", -4)]
        [TestCase((Core.Enums.AccountType)0, "test3", 0)]
        public void AddManufacturerRequestValidator_ShouldHaveUserIdError(Core.Enums.AccountType? accountType, string name, int? userId)
        {
            var testAddInstrumentRequestRequest = new AddManufacturerRequest()
            {
                Name = name,
                AccountType = accountType,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)3, "test1", 1)]
        [TestCase((Core.Enums.AccountType)7, "test2", 4)]
        [TestCase((Core.Enums.AccountType)(-1), "test3", 12)]
        public void AddManufacturerRequestValidator_ShouldHaveAccountTypeError(Core.Enums.AccountType? accountType, string name, int? userId)
        {
            var testAddInstrumentRequestRequest = new AddManufacturerRequest()
            {
                Name = name,
                AccountType = accountType,
                UserId = userId
            };
            var result = validator.TestValidate(testAddInstrumentRequestRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

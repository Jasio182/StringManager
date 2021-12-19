using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class AddTuningRequestValidatorTests
    {
        private AddTuningRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new AddTuningRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", 1)]
        [TestCase((Core.Enums.AccountType)1, 2, "test2", 125)]
        [TestCase((Core.Enums.AccountType)0, 3, "test3", 21)]
        public void ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings)
        {
            var testTuningRequest = new AddTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testTuningRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, null, 1)]
        public void ShouldHaveNameErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings)
        {
            var testTuningRequest = new AddTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.Name);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", -1)]
        [TestCase((Core.Enums.AccountType)1, 2, "test2", -125)]
        [TestCase((Core.Enums.AccountType)0, 3, "test3", 0)]
        public void ShouldHaveNumberOfStringsErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings)
        {
            var testTuningRequest = new AddTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.NumberOfStrings);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, "test1", 1)]
        [TestCase((Core.Enums.AccountType)1, 0, "test2", 125)]
        [TestCase((Core.Enums.AccountType)0, null, "test3", 21)]
        public void ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings)
        {
            var testTuningRequest = new AddTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)6, 1, "test1", 1)]
        [TestCase((Core.Enums.AccountType)(-1), 2, "test2", 125)]
        [TestCase(null, 3, "test3", 21)]
        public void ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings)
        {
            var testTuningRequest = new AddTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

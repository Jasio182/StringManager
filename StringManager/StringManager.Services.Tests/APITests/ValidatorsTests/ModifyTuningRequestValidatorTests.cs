using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class ModifyTuningRequestValidatorTests
    {
        private ModifyTuningRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new ModifyTuningRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, null, 125, 2)]
        [TestCase((Core.Enums.AccountType)0, 3, "test3", 21, 3)]
        public void ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings, int id)
        {
            var testTuningRequest = new ModifyTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings,
                Id = id
            };
            var result = validator.TestValidate(testTuningRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", -1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, "test2", -125, 2)]
        [TestCase((Core.Enums.AccountType)0, 3, "test3", 0, 3)]
        public void ShouldHaveNumberOfStringsErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings, int id)
        {
            var testTuningRequest = new ModifyTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings,
                Id = id
            };
            var result = validator.TestValidate(testTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.NumberOfStrings);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", 1, 0)]
        [TestCase((Core.Enums.AccountType)1, 2, null, 125, -2)]
        [TestCase((Core.Enums.AccountType)0, 3, "test3", 21, -3)]
        public void ShouldHaveIdErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings, int id)
        {
            var testTuningRequest = new ModifyTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings,
                Id = id
            };
            var result = validator.TestValidate(testTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.Id);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, "test1", 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 0, "test2", 125, 2)]
        [TestCase((Core.Enums.AccountType)0, null, "test3", 21, 3)]
        public void ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings, int id)
        {
            var testTuningRequest = new ModifyTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings,
                Id = id
            };
            var result = validator.TestValidate(testTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)6, 1, "test1", 1, 1)]
        [TestCase((Core.Enums.AccountType)(-1), 2, "test2", 125, 2)]
        [TestCase(null, 3, "test3", 21, 3)]
        public void ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings, int id)
        {
            var testTuningRequest = new ModifyTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings,
                Id = id
            };
            var result = validator.TestValidate(testTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class ModifyStringsSetRequestValidatorTests
    {
        private ModifyStringsSetRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new ModifyStringsSetRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, null, 125321, 2)]
        [TestCase((Core.Enums.AccountType)0, 3, "test3", 21, 3)]
        public void ModifyStringsSetRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings, int id)
        {
            var testStringsSetRequest = new ModifyStringsSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings,
                Id = id
            };
            var result = validator.TestValidate(testStringsSetRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", -1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, null, -125321, 2)]
        [TestCase((Core.Enums.AccountType)0, 3, "test3", 0, 3)]
        public void ModifyStringsSetRequestValidator_ShouldHaveNumberOfStringsErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings, int id)
        {
            var testStringsSetRequest = new ModifyStringsSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings,
                Id = id
            };
            var result = validator.TestValidate(testStringsSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.NumberOfStrings);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", 1, 0)]
        [TestCase((Core.Enums.AccountType)1, 2, null, 125321, -2)]
        [TestCase((Core.Enums.AccountType)0, 3, "test3", 21, -3)]
        public void ModifyStringsSetRequestValidator_ShouldHaveIdErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings, int id)
        {
            var testStringsSetRequest = new ModifyStringsSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings,
                Id = id
            };
            var result = validator.TestValidate(testStringsSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.Id);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, "test1", 1, 1)]
        [TestCase((Core.Enums.AccountType)1, null, null, 125321, 2)]
        [TestCase((Core.Enums.AccountType)0, 0, "test3", 21, 3)]
        public void ModifyStringsSetRequestValidator_ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings, int id)
        {
            var testStringsSetRequest = new ModifyStringsSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings,
                Id = id
            };
            var result = validator.TestValidate(testStringsSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)5, 1, "test1", 1, 1)]
        [TestCase((Core.Enums.AccountType)(-1), 2, null, 125321, 2)]
        [TestCase(null, 3, "test3", 21, 3)]
        public void ModifyStringsSetRequestValidator_ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings, int id)
        {
            var testStringsSetRequest = new ModifyStringsSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings,
                Id = id
            };
            var result = validator.TestValidate(testStringsSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

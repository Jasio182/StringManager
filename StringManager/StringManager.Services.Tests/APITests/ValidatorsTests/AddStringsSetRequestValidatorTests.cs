using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class AddStringsSetRequestValidatorTests
    {
        private AddStringsSetRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new AddStringsSetRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", 1)]
        [TestCase((Core.Enums.AccountType)1, 2, "test2", 125321)]
        [TestCase((Core.Enums.AccountType)0, 3, "test3", 21)]
        public void AddStringsSetRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings)
        {
            var testStringsSetRequest = new AddStringsSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testStringsSetRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, null, 1)]
        public void AddStringsSetRequestValidator_ShouldHaveNameErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings)
        {
            var testStringsSetRequest = new AddStringsSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testStringsSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.Name);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", -1)]
        [TestCase((Core.Enums.AccountType)1, 2, "test2", -125321)]
        [TestCase((Core.Enums.AccountType)0, 3, "test3", 0)]
        public void AddStringsSetRequestValidator_ShouldHaveNumberOfStringsErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings)
        {
            var testStringsSetRequest = new AddStringsSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testStringsSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.NumberOfStrings);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, "test1", 1)]
        [TestCase((Core.Enums.AccountType)1, null, "test2", 125321)]
        [TestCase((Core.Enums.AccountType)0, 0, "test3", 21)]
        public void AddStringsSetRequestValidator_ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings)
        {
            var testStringsSetRequest = new AddStringsSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testStringsSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)5, 1, "test1", 1)]
        [TestCase((Core.Enums.AccountType)(-1), 2, "test2", 125321)]
        [TestCase(null, 3, "test3", 21)]
        public void AddStringsSetRequestValidator_ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, string name, int numberOfStrings)
        {
            var testStringsSetRequest = new AddStringsSetRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Name = name,
                NumberOfStrings = numberOfStrings
            };
            var result = validator.TestValidate(testStringsSetRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

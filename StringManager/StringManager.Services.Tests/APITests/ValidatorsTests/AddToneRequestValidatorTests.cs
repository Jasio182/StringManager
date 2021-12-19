using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class AddToneRequestValidatorTests
    {
        private AddToneRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new AddToneRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", 1.1, 1.1)]
        [TestCase((Core.Enums.AccountType)1, 2, "test2", 125.234, 35.16)]
        [TestCase((Core.Enums.AccountType)0, 3, "test3", 21.11, 152.12)]
        public void ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, string name, double frequency, double waveLenght)
        {
            var testToneRequest = new AddToneRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Frequency = frequency,
                Name = name,
                WaveLenght = waveLenght
            };
            var result = validator.TestValidate(testToneRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 3, null, 21.11, 152.12)]
        public void ShouldHaveNameErrors(Core.Enums.AccountType? accountType, int? userId, string name, double frequency, double waveLenght)
        {
            var testToneRequest = new AddToneRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Frequency = frequency,
                Name = name,
                WaveLenght = waveLenght
            };
            var result = validator.TestValidate(testToneRequest);
            result.ShouldHaveValidationErrorFor(request => request.Name);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", -1.1, 1.1)]
        [TestCase((Core.Enums.AccountType)1, 2, "test2", -125.234, 35.16)]
        [TestCase((Core.Enums.AccountType)0, 3, "test3", 0, 152.12)]
        public void ShouldHaveFrequencyErrors(Core.Enums.AccountType? accountType, int? userId, string name, double frequency, double waveLenght)
        {
            var testToneRequest = new AddToneRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Frequency = frequency,
                Name = name,
                WaveLenght = waveLenght
            };
            var result = validator.TestValidate(testToneRequest);
            result.ShouldHaveValidationErrorFor(request => request.Frequency);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, "test1", 1.1, -1.1)]
        [TestCase((Core.Enums.AccountType)1, 2, "test2", 125.234, -35.16)]
        [TestCase((Core.Enums.AccountType)0, 3, "test3", 21.11, 0)]
        public void ShouldHaveWaveLenghtErrors(Core.Enums.AccountType? accountType, int? userId, string name, double frequency, double waveLenght)
        {
            var testToneRequest = new AddToneRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Frequency = frequency,
                Name = name,
                WaveLenght = waveLenght
            };
            var result = validator.TestValidate(testToneRequest);
            result.ShouldHaveValidationErrorFor(request => request.WaveLenght);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, "test1", 1.1, 1.1)]
        [TestCase((Core.Enums.AccountType)1, -2, "test2", 125.234, 35.16)]
        [TestCase((Core.Enums.AccountType)0, 0, "test3", 21.11, 152.12)]
        public void ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, string name, double frequency, double waveLenght)
        {
            var testToneRequest = new AddToneRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Frequency = frequency,
                Name = name,
                WaveLenght = waveLenght
            };
            var result = validator.TestValidate(testToneRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)5, 1, "test1", 1.1, 1.1)]
        [TestCase((Core.Enums.AccountType)(-1), 2, "test2", 125.234, 35.16)]
        [TestCase(null, 3, "test3", 21.11, 152.12)]
        public void ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, string name, double frequency, double waveLenght)
        {
            var testToneRequest = new AddToneRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Frequency = frequency,
                Name = name,
                WaveLenght = waveLenght
            };
            var result = validator.TestValidate(testToneRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

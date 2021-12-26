using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class AddToneInTuningRequestValidatorTests
    {
        private AddToneInTuningRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new AddToneInTuningRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 12)]
        [TestCase((Core.Enums.AccountType)1, 2, 41, 125321, 45)]
        [TestCase((Core.Enums.AccountType)0, 3, 65, 21, 69)]
        public void AddToneInTuningRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, int position, int toneId, int tuningId)
        {
            var testToneInTuningRequest = new AddToneInTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                ToneId = toneId,
                TuningId = tuningId
            };
            var result = validator.TestValidate(testToneInTuningRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, -1, 1, 12)]
        [TestCase((Core.Enums.AccountType)1, 2, -41, 125321, 45)]
        [TestCase((Core.Enums.AccountType)0, 3, 0, 21, 69)]
        public void AddToneInTuningRequestValidator_ShouldHavePositionErrors(Core.Enums.AccountType? accountType, int? userId, int position, int toneId, int tuningId)
        {
            var testToneInTuningRequest = new AddToneInTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                ToneId = toneId,
                TuningId = tuningId
            };
            var result = validator.TestValidate(testToneInTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.Position);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, -1, 12)]
        [TestCase((Core.Enums.AccountType)1, 2, 41, -125321, 45)]
        [TestCase((Core.Enums.AccountType)0, 3, 65, 0, 69)]
        public void AddToneInTuningRequestValidator_ShouldHaveToneIdErrors(Core.Enums.AccountType? accountType, int? userId, int position, int toneId, int tuningId)
        {
            var testToneInTuningRequest = new AddToneInTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                ToneId = toneId,
                TuningId = tuningId
            };
            var result = validator.TestValidate(testToneInTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.ToneId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, -12)]
        [TestCase((Core.Enums.AccountType)1, 2, 41, 125321, -45)]
        [TestCase((Core.Enums.AccountType)0, 3, 65, 21, 0)]
        public void AddToneInTuningRequestValidator_ShouldHaveTuningIdErrors(Core.Enums.AccountType? accountType, int? userId, int position, int toneId, int tuningId)
        {
            var testToneInTuningRequest = new AddToneInTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                ToneId = toneId,
                TuningId = tuningId
            };
            var result = validator.TestValidate(testToneInTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.TuningId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, 1, 1, 12)]
        [TestCase((Core.Enums.AccountType)1, 0, 41, 125321, 45)]
        [TestCase((Core.Enums.AccountType)0, -6, 65, 21, 69)]
        public void AddToneInTuningRequestValidator_ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, int position, int toneId, int tuningId)
        {
            var testToneInTuningRequest = new AddToneInTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                ToneId = toneId,
                TuningId = tuningId
            };
            var result = validator.TestValidate(testToneInTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)(-1), 1, 1, 1, 12)]
        [TestCase((Core.Enums.AccountType)6, 2, 41, 125321, 45)]
        [TestCase((Core.Enums.AccountType)4, 3, 65, 21, 69)]
        public void AddToneInTuningRequestValidator_ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, int position, int toneId, int tuningId)
        {
            var testToneInTuningRequest = new AddToneInTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                ToneId = toneId,
                TuningId = tuningId
            };
            var result = validator.TestValidate(testToneInTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

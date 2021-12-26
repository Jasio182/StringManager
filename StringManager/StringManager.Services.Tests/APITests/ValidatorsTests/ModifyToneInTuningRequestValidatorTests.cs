using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class ModifyToneInTuningRequestValidatorTests
    {
        private ModifyToneInTuningRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new ModifyToneInTuningRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 12, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, null, null, null, 2)]
        [TestCase((Core.Enums.AccountType)0, 3, 65, 21, 69, 3)]
        public void ModifyToneInTuningRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, int? position, int? toneId, int? tuningId, int id)
        {
            var testToneInTuningRequest = new ModifyToneInTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                ToneId = toneId,
                TuningId = tuningId,
                Id = id
            };
            var result = validator.TestValidate(testToneInTuningRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, -1, 1, 12, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, -41, 125321, 45, 2)]
        [TestCase((Core.Enums.AccountType)0, 3, 0, 21, 69, 3)]
        public void ModifyToneInTuningRequestValidator_ShouldHavePositionErrors(Core.Enums.AccountType? accountType, int? userId, int? position, int? toneId, int? tuningId, int id)
        {
            var testToneInTuningRequest = new ModifyToneInTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                ToneId = toneId,
                TuningId = tuningId,
                Id = id
            };
            var result = validator.TestValidate(testToneInTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.Position);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, -1, 12, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 41, -125321, 45, 2)]
        [TestCase((Core.Enums.AccountType)0, 3, 65, 0, 69, 3)]
        public void ModifyToneInTuningRequestValidator_ShouldHaveToneIdErrors(Core.Enums.AccountType? accountType, int? userId, int? position, int? toneId, int? tuningId, int id)
        {
            var testToneInTuningRequest = new ModifyToneInTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                ToneId = toneId,
                TuningId = tuningId,
                Id = id
            };
            var result = validator.TestValidate(testToneInTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.ToneId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, -12, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 41, 125321, -45, 2)]
        [TestCase((Core.Enums.AccountType)0, 3, 65, 21, 0, 3)]
        public void ModifyToneInTuningRequestValidator_ShouldHaveTuningIdErrors(Core.Enums.AccountType? accountType, int? userId, int? position, int? toneId, int? tuningId, int id)
        {
            var testToneInTuningRequest = new ModifyToneInTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                ToneId = toneId,
                TuningId = tuningId,
                Id = id
            };
            var result = validator.TestValidate(testToneInTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.TuningId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 12, 0)]
        [TestCase((Core.Enums.AccountType)1, 2, null, null, null, -5)]
        [TestCase((Core.Enums.AccountType)0, 3, 65, 21, 69, -7)]
        public void ModifyToneInTuningRequestValidator_ShouldHaveIdErrors(Core.Enums.AccountType? accountType, int? userId, int? position, int? toneId, int? tuningId, int id)
        {
            var testToneInTuningRequest = new ModifyToneInTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                ToneId = toneId,
                TuningId = tuningId,
                Id = id
            };
            var result = validator.TestValidate(testToneInTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.Id);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, 1, 1, 12, 1)]
        [TestCase((Core.Enums.AccountType)1, 0, 41, 125321, 45, 2)]
        [TestCase((Core.Enums.AccountType)0, null, 65, 21, 69, 3)]
        public void ModifyToneInTuningRequestValidator_ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, int? position, int? toneId, int? tuningId, int id)
        {
            var testToneInTuningRequest = new ModifyToneInTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                ToneId = toneId,
                TuningId = tuningId,
                Id = id
            };
            var result = validator.TestValidate(testToneInTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)(-1), 1, 1, 1, 12, 1)]
        [TestCase((Core.Enums.AccountType)6, 2, 41, 125321, 45, 2)]
        [TestCase(null, 3, 65, 21, 69, 3)]
        public void ModifyToneInTuningRequestValidator_ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, int? position, int? toneId, int? tuningId, int id)
        {
            var testToneInTuningRequest = new ModifyToneInTuningRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Position = position,
                ToneId = toneId,
                TuningId = tuningId,
                Id = id
            };
            var result = validator.TestValidate(testToneInTuningRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

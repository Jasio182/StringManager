﻿using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class GetStringsSetsWithCorrepondingTensionRequestValidatorTests
    {
        private GetStringsSetsWithCorrepondingTensionRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new GetStringsSetsWithCorrepondingTensionRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, Core.Enums.StringType.WoundBrass)]
        [TestCase((Core.Enums.AccountType)1, 2, 21, 11, Core.Enums.StringType.WoundNylon)]
        [TestCase((Core.Enums.AccountType)0, 3, 152, 734, null)]
        public void GetStringsSetsWithCorrepondingTensionRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, int myInstrumentId, int resultTuningId, Core.Enums.StringType? stringType)
        {
            var testUserRequest = new GetStringsSetsWithCorrepondingTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                MyInstrumentId = myInstrumentId,
                ResultTuningId = resultTuningId,
                StringType = stringType
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, -1, 1, Core.Enums.StringType.WoundBrass)]
        [TestCase((Core.Enums.AccountType)1, 2, -21, 11, Core.Enums.StringType.WoundNylon)]
        [TestCase((Core.Enums.AccountType)0, 3, 0, 734, null)]
        public void GetStringsSetsWithCorrepondingTensionRequestValidator_ShouldHaveMyInstrumentIdErrors(Core.Enums.AccountType? accountType, int? userId, int myInstrumentId, int resultTuningId, Core.Enums.StringType stringType)
        {
            var testUserRequest = new GetStringsSetsWithCorrepondingTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                MyInstrumentId = myInstrumentId,
                ResultTuningId = resultTuningId,
                StringType = stringType
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.MyInstrumentId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, -1, Core.Enums.StringType.WoundBrass)]
        [TestCase((Core.Enums.AccountType)1, 2, 21, -11, Core.Enums.StringType.WoundNylon)]
        [TestCase((Core.Enums.AccountType)0, 3, 152, 0, null)]
        public void GetStringsSetsWithCorrepondingTensionRequestValidator_ShouldHaveResultTuningIdErrors(Core.Enums.AccountType? accountType, int? userId, int myInstrumentId, int resultTuningId, Core.Enums.StringType stringType)
        {
            var testUserRequest = new GetStringsSetsWithCorrepondingTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                MyInstrumentId = myInstrumentId,
                ResultTuningId = resultTuningId,
                StringType = stringType
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.ResultTuningId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, -1, (Core.Enums.StringType)7)]
        [TestCase((Core.Enums.AccountType)1, 2, 21, -11, (Core.Enums.StringType)11)]
        [TestCase((Core.Enums.AccountType)0, 3, 152, 0, (Core.Enums.StringType)(-10))]
        public void GetStringsSetsWithCorrepondingTensionRequestValidator_ShouldHaveStringTypedErrors(Core.Enums.AccountType? accountType, int? userId, int myInstrumentId, int resultTuningId, Core.Enums.StringType stringType)
        {
            var testUserRequest = new GetStringsSetsWithCorrepondingTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                MyInstrumentId = myInstrumentId,
                ResultTuningId = resultTuningId,
                StringType = stringType
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.StringType);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, 1, 1, Core.Enums.StringType.WoundBrass)]
        [TestCase((Core.Enums.AccountType)1, 0, 21, 11, Core.Enums.StringType.WoundNylon)]
        [TestCase((Core.Enums.AccountType)0, -25, 152, 734, null)]
        public void GetStringsSetsWithCorrepondingTensionRequestValidator_ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, int myInstrumentId, int resultTuningId, Core.Enums.StringType stringType)
        {
            var testUserRequest = new GetStringsSetsWithCorrepondingTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                MyInstrumentId = myInstrumentId,
                ResultTuningId = resultTuningId,
                StringType = stringType
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)3, 1, 1, 1, Core.Enums.StringType.WoundBrass)]
        [TestCase((Core.Enums.AccountType)(-1), 2, 21, 11, Core.Enums.StringType.WoundNylon)]
        [TestCase((Core.Enums.AccountType)313, 3, 152, 734, null)]
        public void GetStringsSetsWithCorrepondingTensionRequestValidator_ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, int myInstrumentId, int resultTuningId, Core.Enums.StringType stringType)
        {
            var testUserRequest = new GetStringsSetsWithCorrepondingTensionRequest()
            {
                AccountType = accountType,
                UserId = userId,
                MyInstrumentId = myInstrumentId,
                ResultTuningId = resultTuningId,
                StringType = stringType
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

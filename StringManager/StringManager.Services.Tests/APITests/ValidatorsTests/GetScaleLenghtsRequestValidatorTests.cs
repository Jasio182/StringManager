using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Services.API.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class GetScaleLenghtsRequestValidatorTests
    {
        private GetScaleLenghtsRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new GetScaleLenghtsRequestValidator();
        }

        [Test]
        [TestCase(null, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 21)]
        [TestCase((Core.Enums.AccountType)0, null, 18)]
        public void GetScaleLenghtsRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, int instrumentId)
        {
            var testUserRequest = new GetScaleLenghtsRequest()
            {
                AccountType = accountType,
                UserId = userId,
                InstrumentId = instrumentId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, -1)]
        [TestCase((Core.Enums.AccountType)1, 2, -21)]
        [TestCase((Core.Enums.AccountType)0, 3, 0)]
        public void GetScaleLenghtsRequestValidator_ShouldHaveInstrumentIdErrors(Core.Enums.AccountType? accountType, int? userId, int instrumentId)
        {
            var testUserRequest = new GetScaleLenghtsRequest()
            {
                AccountType = accountType,
                UserId = userId,
                InstrumentId = instrumentId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.InstrumentId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, 1)]
        [TestCase((Core.Enums.AccountType)1, 0, 21)]
        [TestCase((Core.Enums.AccountType)0, -3, 18)]
        public void GetScaleLenghtsRequestValidator_ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, int instrumentId)
        {
            var testUserRequest = new GetScaleLenghtsRequest()
            {
                AccountType = accountType,
                UserId = userId,
                InstrumentId = instrumentId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)(-1), 1, 1)]
        [TestCase((Core.Enums.AccountType)6, 2, 21)]
        [TestCase((Core.Enums.AccountType)18, 3, 18)]
        public void GetScaleLenghtsRequestValidator_ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, int instrumentId)
        {
            var testUserRequest = new GetScaleLenghtsRequest()
            {
                AccountType = accountType,
                UserId = userId,
                InstrumentId = instrumentId
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

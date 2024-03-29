﻿using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class RemoveInstalledStringRequestValidatorTests
    {
        private RemoveInstalledStringRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new RemoveInstalledStringRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1)]
        [TestCase((Core.Enums.AccountType)1, 12, 11)]
        [TestCase((Core.Enums.AccountType)0, 13, 21)]
        public void RemoveInstalledStringRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, int id)
        {
            var testRemoveInstalledStringRequest = new RemoveInstalledStringRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Id = id
            };
            var result = validator.TestValidate(testRemoveInstalledStringRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, -1)]
        [TestCase((Core.Enums.AccountType)1, 12, -11)]
        [TestCase((Core.Enums.AccountType)0, 13, 0)]
        public void RemoveInstalledStringRequestValidator_ShouldHaveIdError(Core.Enums.AccountType? accountType, int? userId, int id)
        {
            var testRemoveInstalledStringRequest = new RemoveInstalledStringRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Id = id
            };
            var result = validator.TestValidate(testRemoveInstalledStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.Id);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)2, 1, 1)]
        [TestCase((Core.Enums.AccountType)(-1), 12, 11)]
        [TestCase((Core.Enums.AccountType)52, 13, 21)]
        public void RemoveInstalledStringRequestValidator_ShouldHaveAccountTypeError(Core.Enums.AccountType? accountType, int? userId, int id)
        {
            var testRemoveInstalledStringRequest = new RemoveInstalledStringRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Id = id
            };
            var result = validator.TestValidate(testRemoveInstalledStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, 1)]
        [TestCase((Core.Enums.AccountType)1, 0, 11)]
        [TestCase((Core.Enums.AccountType)0, -62, 21)]
        public void RemoveInstalledStringRequestValidator_ShouldHaveUserIdError(Core.Enums.AccountType? accountType, int? userId, int id)
        {
            var testRemoveInstalledStringRequest = new RemoveInstalledStringRequest()
            {
                AccountType = accountType,
                UserId = userId,
                Id = id
            };
            var result = validator.TestValidate(testRemoveInstalledStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }
    }
}

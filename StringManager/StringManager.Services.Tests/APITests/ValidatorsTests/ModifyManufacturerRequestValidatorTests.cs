﻿using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class ModifyManufacturerRequestValidatorTests
    {
        private ModifyManufacturerRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new ModifyManufacturerRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, "test1")]
        [TestCase((Core.Enums.AccountType)1, 12, 11, "test2")]
        [TestCase((Core.Enums.AccountType)0, 13, 21, "test3")]
        public void ModifyManufacturerRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, int id, string name)
        {
            var testModifyManufacturerRequest = new ModifyManufacturerRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                Name = name
            };
            var result = validator.TestValidate(testModifyManufacturerRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, -1, "test1")]
        [TestCase((Core.Enums.AccountType)1, 12, -11, "test2")]
        [TestCase((Core.Enums.AccountType)0, 13, 0, "test3")]
        public void ModifyManufacturerRequestValidator_ShouldHaveIdError(Core.Enums.AccountType? accountType, int? userId, int id, string name)
        {
            var testModifyManufacturerRequest = new ModifyManufacturerRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                Name = name
            };
            var result = validator.TestValidate(testModifyManufacturerRequest);
            result.ShouldHaveValidationErrorFor(request => request.Id);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, "")]
        [TestCase((Core.Enums.AccountType)1, 12, 11, null)]
        public void ModifyManufacturerRequestValidator_ShouldHaveNameError(Core.Enums.AccountType? accountType, int? userId, int id, string name)
        {
            var testModifyManufacturerRequest = new ModifyManufacturerRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                Name = name
            };
            var result = validator.TestValidate(testModifyManufacturerRequest);
            result.ShouldHaveValidationErrorFor(request => request.Name);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, 1, "test1")]
        [TestCase((Core.Enums.AccountType)1, 0, 11, "test2")]
        [TestCase((Core.Enums.AccountType)0, -16, 21, "test3")]
        public void ModifyManufacturerRequestValidator_ShouldHaveUserIdError(Core.Enums.AccountType? accountType, int? userId, int id, string name)
        {
            var testModifyManufacturerRequest = new ModifyManufacturerRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                Name = name
            };
            var result = validator.TestValidate(testModifyManufacturerRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)5, 1, 1, "test1")]
        [TestCase((Core.Enums.AccountType)(-1), 12, 11, "test2")]
        [TestCase((Core.Enums.AccountType)(-16), 13, 21, "test3")]
        public void ModifyManufacturerRequestValidator_ShouldHaveAccountTypeError(Core.Enums.AccountType? accountType, int? userId, int id, string name)
        {
            var testModifyManufacturerRequest = new ModifyManufacturerRequest()
            {
                Id = id,
                AccountType = accountType,
                UserId = userId,
                Name = name
            };
            var result = validator.TestValidate(testModifyManufacturerRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}

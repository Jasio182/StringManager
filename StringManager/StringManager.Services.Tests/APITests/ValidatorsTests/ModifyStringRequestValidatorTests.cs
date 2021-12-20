﻿using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class ModifyStringRequestValidatorTests
    {
        private ModifyStringRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new ModifyStringRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 1, 0.1, Core.Enums.StringType.PlainNylon, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, null, null, null, null, null, 2)]
        [TestCase((Core.Enums.AccountType)0, 3, 6, 51, 21, 0.1414, Core.Enums.StringType.PlainBrass, 3)]
        public void ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, int? manufacturerId, int? numberOfDaysGood, int? size, double? specificWeight, Core.Enums.StringType? stringType, int id)
        {
            var testStringRequest = new ModifyStringRequest()
            {
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                NumberOfDaysGood = numberOfDaysGood,
                Size = size,
                SpecificWeight = specificWeight,
                StringType = stringType,
                Id = id
            };
            var result = validator.TestValidate(testStringRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, -1, 1, 1, 0.1, Core.Enums.StringType.PlainNylon, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, -4, 123, 154, 0.00001, Core.Enums.StringType.WoundNylon, 2)]
        [TestCase((Core.Enums.AccountType)0, 3, 0, 51, 21, 0.1414, Core.Enums.StringType.PlainBrass, 3)]
        public void ShouldHaveManufacturerIdErrors(Core.Enums.AccountType? accountType, int? userId, int? manufacturerId, int? numberOfDaysGood, int? size, double? specificWeight, Core.Enums.StringType? stringType, int id)
        {
            var testStringRequest = new ModifyStringRequest()
            {
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                NumberOfDaysGood = numberOfDaysGood,
                Size = size,
                SpecificWeight = specificWeight,
                StringType = stringType,
                Id = id
            };
            var result = validator.TestValidate(testStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.ManufacturerId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, -1, 1, 0.1, Core.Enums.StringType.PlainNylon, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 4, -123, 154, 0.00001, Core.Enums.StringType.WoundNylon, 2)]
        [TestCase((Core.Enums.AccountType)0, 3, 6, 0, 21, 0.1414, Core.Enums.StringType.PlainBrass, 3)]
        public void ShouldHaveNumberOfDaysGoodErrors(Core.Enums.AccountType? accountType, int? userId, int? manufacturerId, int? numberOfDaysGood, int? size, double? specificWeight, Core.Enums.StringType? stringType, int id)
        {
            var testStringRequest = new ModifyStringRequest()
            {
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                NumberOfDaysGood = numberOfDaysGood,
                Size = size,
                SpecificWeight = specificWeight,
                StringType = stringType,
                Id = id
            };
            var result = validator.TestValidate(testStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.NumberOfDaysGood);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, -1, 0.1, Core.Enums.StringType.PlainNylon, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 4, 123, -154, 0.00001, Core.Enums.StringType.WoundNylon, 2)]
        [TestCase((Core.Enums.AccountType)0, 3, 6, 51, 0, 0.1414, Core.Enums.StringType.PlainBrass, 3)]
        public void ShouldHaveSizeErrors(Core.Enums.AccountType? accountType, int? userId, int? manufacturerId, int? numberOfDaysGood, int? size, double? specificWeight, Core.Enums.StringType? stringType, int id)
        {
            var testStringRequest = new ModifyStringRequest()
            {
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                NumberOfDaysGood = numberOfDaysGood,
                Size = size,
                SpecificWeight = specificWeight,
                StringType = stringType,
                Id = id
            };
            var result = validator.TestValidate(testStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.Size);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 1, -1, Core.Enums.StringType.PlainNylon, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 4, 123, 154, -0.00001, Core.Enums.StringType.WoundNylon, 2)]
        [TestCase((Core.Enums.AccountType)0, 3, 6, 51, 21, 0, Core.Enums.StringType.PlainBrass, 3)]
        public void ShouldHaveSpecificWeightErrors(Core.Enums.AccountType? accountType, int? userId, int? manufacturerId, int? numberOfDaysGood, int? size, double? specificWeight, Core.Enums.StringType? stringType, int id)
        {
            var testStringRequest = new ModifyStringRequest()
            {
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                NumberOfDaysGood = numberOfDaysGood,
                Size = size,
                SpecificWeight = specificWeight,
                StringType = stringType,
                Id = id
            };
            var result = validator.TestValidate(testStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.SpecificWeight);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 1, 0.1, (Core.Enums.StringType)11, 1)]
        [TestCase((Core.Enums.AccountType)1, 2, 4, 123, 154, 0.00001, (Core.Enums.StringType)7, 2)]
        [TestCase((Core.Enums.AccountType)0, 3, 6, 51, 21, 0.1414, (Core.Enums.StringType)(-5), 3)]
        public void ShouldHaveStringTypeErrors(Core.Enums.AccountType? accountType, int? userId, int? manufacturerId, int? numberOfDaysGood, int? size, double? specificWeight, Core.Enums.StringType? stringType, int id)
        {
            var testStringRequest = new ModifyStringRequest()
            {
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                NumberOfDaysGood = numberOfDaysGood,
                Size = size,
                SpecificWeight = specificWeight,
                StringType = stringType,
                Id = id
            };
            var result = validator.TestValidate(testStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.StringType);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, 1, 1, 1, 0.1, Core.Enums.StringType.PlainNylon, -1)]
        [TestCase((Core.Enums.AccountType)1, 2, null, null, null, null, null, 0)]
        [TestCase((Core.Enums.AccountType)0, 3, 6, 51, 21, 0.1414, Core.Enums.StringType.PlainBrass, -6)]
        public void ShouldHaveIdErrors(Core.Enums.AccountType? accountType, int? userId, int? manufacturerId, int? numberOfDaysGood, int? size, double? specificWeight, Core.Enums.StringType? stringType, int id)
        {
            var testStringRequest = new ModifyStringRequest()
            {
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                NumberOfDaysGood = numberOfDaysGood,
                Size = size,
                SpecificWeight = specificWeight,
                StringType = stringType,
                Id = id
            };
            var result = validator.TestValidate(testStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.Id);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, 1, 1, 1, 0.1, Core.Enums.StringType.PlainNylon, 1)]
        [TestCase((Core.Enums.AccountType)1, 0, 4, 123, 154, 0.00001, Core.Enums.StringType.WoundNylon, 2)]
        [TestCase((Core.Enums.AccountType)0, null, 6, 51, 21, 0.1414, Core.Enums.StringType.PlainBrass, 3)]
        public void ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, int? manufacturerId, int? numberOfDaysGood, int? size, double? specificWeight, Core.Enums.StringType? stringType, int id)
        {
            var testStringRequest = new ModifyStringRequest()
            {
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                NumberOfDaysGood = numberOfDaysGood,
                Size = size,
                SpecificWeight = specificWeight,
                StringType = stringType,
                Id = id
            };
            var result = validator.TestValidate(testStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)6, 1, 1, 1, 1, 0.1, Core.Enums.StringType.PlainNylon, 1)]
        [TestCase((Core.Enums.AccountType)(-1), 2, 4, 123, 154, 0.00001, Core.Enums.StringType.WoundNylon, 2)]
        [TestCase(null, 3, 6, 51, 21, 0.1414, Core.Enums.StringType.PlainBrass, 3)]
        public void ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, int? manufacturerId, int? numberOfDaysGood, int? size, double? specificWeight, Core.Enums.StringType? stringType, int id)
        {
            var testStringRequest = new ModifyStringRequest()
            {
                AccountType = accountType,
                UserId = userId,
                ManufacturerId = manufacturerId,
                NumberOfDaysGood = numberOfDaysGood,
                Size = size,
                SpecificWeight = specificWeight,
                StringType = stringType,
                Id = id
            };
            var result = validator.TestValidate(testStringRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }
    }
}
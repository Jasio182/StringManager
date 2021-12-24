using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    internal class ModifyUserRequestValidatorTests
    {
        private ModifyUserRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new ModifyUserRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, Core.Enums.GuitarDailyMaintanance.PlayAsIs, Core.Enums.AccountType.User, "testEmail1", "testPassword1", Core.Enums.PlayStyle.Hard, "testUsername1", 1)]
        [TestCase((Core.Enums.AccountType)1, 2, null, null, null, null, null, null, 2)]
        [TestCase((Core.Enums.AccountType)0, 3, Core.Enums.GuitarDailyMaintanance.CleanHandsWipedStrings, Core.Enums.AccountType.User, "testEmail3", "testPassword3", Core.Enums.PlayStyle.Light, "testUsername3", 3)]
        public void ModifyUserRequestValidator_ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, Core.Enums.GuitarDailyMaintanance? dailyMaintenance, Core.Enums.AccountType? accountTypeToUpdate,
            string email, string password, Core.Enums.PlayStyle? playStyle, string username, int id)
        {
            var testUserRequest = new ModifyUserRequest()
            {
                AccountType = accountType,
                UserId = userId,
                DailyMaintanance = dailyMaintenance,
                AccountTypeToUpdate = accountTypeToUpdate,
                Email = email,
                Password = password,
                PlayStyle = playStyle,
                Username = username,
                Id = id
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, (Core.Enums.GuitarDailyMaintanance)(-1), Core.Enums.AccountType.User, "testEmail1", "testPassword1", Core.Enums.PlayStyle.Hard, "testUsername1", 1)]
        [TestCase((Core.Enums.AccountType)1, 2, (Core.Enums.GuitarDailyMaintanance)11, Core.Enums.AccountType.Admin, "testEmail2", "testPassword2", Core.Enums.PlayStyle.Moderate, "testUsername2", 2)]
        [TestCase((Core.Enums.AccountType)0, 3, (Core.Enums.GuitarDailyMaintanance)5, Core.Enums.AccountType.User, "testEmail3", "testPassword3", Core.Enums.PlayStyle.Light, "testUsername3", 3)]
        public void ModifyUserRequestValidator_ShouldHaveDailyMaintananceErrors(Core.Enums.AccountType? accountType, int? userId, Core.Enums.GuitarDailyMaintanance? dailyMaintenance, Core.Enums.AccountType? accountTypeToUpdate,
            string email, string password, Core.Enums.PlayStyle? playStyle, string username, int id)
        {
            var testUserRequest = new ModifyUserRequest()
            {
                AccountType = accountType,
                UserId = userId,
                DailyMaintanance = dailyMaintenance,
                AccountTypeToUpdate = accountTypeToUpdate,
                Email = email,
                Password = password,
                PlayStyle = playStyle,
                Username = username,
                Id = id
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.DailyMaintanance);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, Core.Enums.GuitarDailyMaintanance.PlayAsIs, (Core.Enums.AccountType)6, "testEmail1", "testPassword1", Core.Enums.PlayStyle.Hard, "testUsername1", 1)]
        [TestCase((Core.Enums.AccountType)1, 2, Core.Enums.GuitarDailyMaintanance.CleanHands, (Core.Enums.AccountType)11, "testEmail2", "testPassword2", Core.Enums.PlayStyle.Moderate, "testUsername2", 2)]
        [TestCase((Core.Enums.AccountType)0, 3, Core.Enums.GuitarDailyMaintanance.CleanHandsWipedStrings, (Core.Enums.AccountType)(-11), "testEmail3", "testPassword3", Core.Enums.PlayStyle.Light, "testUsername3", 3)]
        public void ModifyUserRequestValidator_ShouldHaveAccountTypeToUpdateErrors(Core.Enums.AccountType? accountType, int? userId, Core.Enums.GuitarDailyMaintanance? dailyMaintenance, Core.Enums.AccountType? accountTypeToUpdate,
            string email, string password, Core.Enums.PlayStyle? playStyle, string username, int id)
        {
            var testUserRequest = new ModifyUserRequest()
            {
                AccountType = accountType,
                UserId = userId,
                DailyMaintanance = dailyMaintenance,
                AccountTypeToUpdate = accountTypeToUpdate,
                Email = email,
                Password = password,
                PlayStyle = playStyle,
                Username = username,
                Id = id
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountTypeToUpdate);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, Core.Enums.GuitarDailyMaintanance.PlayAsIs, Core.Enums.AccountType.User, "testEmail1", "testPassword1", (Core.Enums.PlayStyle)11, "testUsername1", 1)]
        [TestCase((Core.Enums.AccountType)1, 2, Core.Enums.GuitarDailyMaintanance.CleanHands, Core.Enums.AccountType.Admin, "testEmail2", "testPassword2", (Core.Enums.PlayStyle)17, "testUsername2", 2)]
        [TestCase((Core.Enums.AccountType)0, 3, Core.Enums.GuitarDailyMaintanance.CleanHandsWipedStrings, Core.Enums.AccountType.User, "testEmail3", "testPassword3", (Core.Enums.PlayStyle)(-6), "testUsername3", 3)]
        public void ModifyUserRequestValidator_ShouldHavePlayStyleErrors(Core.Enums.AccountType? accountType, int? userId, Core.Enums.GuitarDailyMaintanance? dailyMaintenance, Core.Enums.AccountType? accountTypeToUpdate,
            string email, string password, Core.Enums.PlayStyle? playStyle, string username, int id)
        {
            var testUserRequest = new ModifyUserRequest()
            {
                AccountType = accountType,
                UserId = userId,
                DailyMaintanance = dailyMaintenance,
                AccountTypeToUpdate = accountTypeToUpdate,
                Email = email,
                Password = password,
                PlayStyle = playStyle,
                Username = username,
                Id = id
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.PlayStyle);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, Core.Enums.GuitarDailyMaintanance.PlayAsIs, Core.Enums.AccountType.User, "testEmail1", "testPassword1", Core.Enums.PlayStyle.Hard, "testUsername1", -1)]
        [TestCase((Core.Enums.AccountType)1, 2, null, null, null, null, null, null, 0)]
        [TestCase((Core.Enums.AccountType)0, 3, Core.Enums.GuitarDailyMaintanance.CleanHandsWipedStrings, Core.Enums.AccountType.User, "testEmail3", "testPassword3", Core.Enums.PlayStyle.Light, "testUsername3", -3)]
        public void ModifyUserRequestValidator_ShouldHaveIdErrors(Core.Enums.AccountType? accountType, int? userId, Core.Enums.GuitarDailyMaintanance? dailyMaintenance, Core.Enums.AccountType? accountTypeToUpdate,
            string email, string password, Core.Enums.PlayStyle? playStyle, string username, int id)
        {
            var testUserRequest = new ModifyUserRequest()
            {
                AccountType = accountType,
                UserId = userId,
                DailyMaintanance = dailyMaintenance,
                AccountTypeToUpdate = accountTypeToUpdate,
                Email = email,
                Password = password,
                PlayStyle = playStyle,
                Username = username,
                Id = id
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.Id);
        }

    [Test]
        [TestCase((Core.Enums.AccountType)(-1), 1, Core.Enums.GuitarDailyMaintanance.PlayAsIs, Core.Enums.AccountType.User, "testEmail1", "testPassword1", Core.Enums.PlayStyle.Hard, "testUsername1", 1)]
        [TestCase((Core.Enums.AccountType)8, 2, Core.Enums.GuitarDailyMaintanance.CleanHands, Core.Enums.AccountType.Admin, "testEmail2", "testPassword2", Core.Enums.PlayStyle.Moderate, "testUsername2", 2)]
        [TestCase((Core.Enums.AccountType)18, 3, Core.Enums.GuitarDailyMaintanance.CleanHandsWipedStrings, Core.Enums.AccountType.User, "testEmail3", "testPassword3", Core.Enums.PlayStyle.Light, "testUsername3", 3)]
        public void ModifyUserRequestValidator_ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, Core.Enums.GuitarDailyMaintanance? dailyMaintenance, Core.Enums.AccountType? accountTypeToUpdate,
            string email, string password, Core.Enums.PlayStyle? playStyle, string username, int id)
        {
            var testUserRequest = new ModifyUserRequest()
            {
                AccountType = accountType,
                UserId = userId,
                DailyMaintanance = dailyMaintenance,
                AccountTypeToUpdate = accountTypeToUpdate,
                Email = email,
                Password = password,
                PlayStyle = playStyle,
                Username = username,
                Id = id
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, Core.Enums.GuitarDailyMaintanance.PlayAsIs, Core.Enums.AccountType.User, "testEmail1", "testPassword1", Core.Enums.PlayStyle.Hard, "testUsername1", 1)]
        [TestCase((Core.Enums.AccountType)1, 0, Core.Enums.GuitarDailyMaintanance.CleanHands, Core.Enums.AccountType.Admin, "testEmail2", "testPassword2", Core.Enums.PlayStyle.Moderate, "testUsername2", 2)]
        [TestCase((Core.Enums.AccountType)0, -6, Core.Enums.GuitarDailyMaintanance.CleanHandsWipedStrings, Core.Enums.AccountType.User, "testEmail3", "testPassword3", Core.Enums.PlayStyle.Light, "testUsername3", 3)]
        public void ModifyUserRequestValidator_ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, Core.Enums.GuitarDailyMaintanance? dailyMaintenance, Core.Enums.AccountType? accountTypeToUpdate,
            string email, string password, Core.Enums.PlayStyle? playStyle, string username, int id)
        {
            var testUserRequest = new ModifyUserRequest()
            {
                AccountType = accountType,
                UserId = userId,
                DailyMaintanance = dailyMaintenance,
                AccountTypeToUpdate = accountTypeToUpdate,
                Email = email,
                Password = password,
                PlayStyle = playStyle,
                Username = username,
                Id = id
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }
    }
}

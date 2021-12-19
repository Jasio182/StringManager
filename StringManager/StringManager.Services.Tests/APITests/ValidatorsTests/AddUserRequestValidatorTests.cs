using FluentValidation.TestHelper;
using NUnit.Framework;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Validators;

namespace StringManager.Services.Tests.APITests.ValidatorsTests
{
    public class AddUserRequestValidatorTests
    {
        private AddUserRequestValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new AddUserRequestValidator();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, Core.Enums.GuitarDailyMaintanance.PlayAsIs, Core.Enums.AccountType.User, "testEmail1", "testPassword1", Core.Enums.PlayStyle.Hard, "testUsername1")]
        [TestCase((Core.Enums.AccountType)1, 2, Core.Enums.GuitarDailyMaintanance.CleanHands, Core.Enums.AccountType.Admin, "testEmail2", "testPassword2", Core.Enums.PlayStyle.Moderate, "testUsername2")]
        [TestCase((Core.Enums.AccountType)0, 3, Core.Enums.GuitarDailyMaintanance.CleanHandsWipedStrings, Core.Enums.AccountType.User, "testEmail3", "testPassword3", Core.Enums.PlayStyle.Light, "testUsername3")]
        public void ShouldNotHaveAnyErrors(Core.Enums.AccountType? accountType, int? userId, Core.Enums.GuitarDailyMaintanance dailyMaintenance, Core.Enums.AccountType accountTypeToAdd,
            string email, string password, Core.Enums.PlayStyle playStyle, string username)
        {
            var testUserRequest = new AddUserRequest()
            {
                AccountType = accountType,
                UserId = userId,
                DailyMaintanance = dailyMaintenance,
                AccountTypeToAdd = accountTypeToAdd,
                Email = email,
                Password = password,
                PlayStyle = playStyle,
                Username = username
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, (Core.Enums.GuitarDailyMaintanance)(-1), Core.Enums.AccountType.User, "testEmail1", "testPassword1", Core.Enums.PlayStyle.Hard, "testUsername1")]
        [TestCase((Core.Enums.AccountType)1, 2, (Core.Enums.GuitarDailyMaintanance)11, Core.Enums.AccountType.Admin, "testEmail2", "testPassword2", Core.Enums.PlayStyle.Moderate, "testUsername2")]
        [TestCase((Core.Enums.AccountType)0, 3, (Core.Enums.GuitarDailyMaintanance)5, Core.Enums.AccountType.User, "testEmail3", "testPassword3", Core.Enums.PlayStyle.Light, "testUsername3")]
        public void ShouldHaveDailyMaintananceErrors(Core.Enums.AccountType? accountType, int? userId, Core.Enums.GuitarDailyMaintanance dailyMaintenance, Core.Enums.AccountType accountTypeToAdd,
            string email, string password, Core.Enums.PlayStyle playStyle, string username)
        {
            var testUserRequest = new AddUserRequest()
            {
                AccountType = accountType,
                UserId = userId,
                DailyMaintanance = dailyMaintenance,
                AccountTypeToAdd = accountTypeToAdd,
                Email = email,
                Password = password,
                PlayStyle = playStyle,
                Username = username
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.DailyMaintanance);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, Core.Enums.GuitarDailyMaintanance.PlayAsIs, (Core.Enums.AccountType)6, "testEmail1", "testPassword1", Core.Enums.PlayStyle.Hard, "testUsername1")]
        [TestCase((Core.Enums.AccountType)1, 2, Core.Enums.GuitarDailyMaintanance.CleanHands, (Core.Enums.AccountType)11, "testEmail2", "testPassword2", Core.Enums.PlayStyle.Moderate, "testUsername2")]
        [TestCase((Core.Enums.AccountType)0, 3, Core.Enums.GuitarDailyMaintanance.CleanHandsWipedStrings, (Core.Enums.AccountType)(-11), "testEmail3", "testPassword3", Core.Enums.PlayStyle.Light, "testUsername3")]
        public void ShouldHaveAccountTypeToAddErrors(Core.Enums.AccountType? accountType, int? userId, Core.Enums.GuitarDailyMaintanance dailyMaintenance, Core.Enums.AccountType accountTypeToAdd,
            string email, string password, Core.Enums.PlayStyle playStyle, string username)
        {
            var testUserRequest = new AddUserRequest()
            {
                AccountType = accountType,
                UserId = userId,
                DailyMaintanance = dailyMaintenance,
                AccountTypeToAdd = accountTypeToAdd,
                Email = email,
                Password = password,
                PlayStyle = playStyle,
                Username = username
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountTypeToAdd);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, Core.Enums.GuitarDailyMaintanance.PlayAsIs, Core.Enums.AccountType.User, "", "testPassword1", Core.Enums.PlayStyle.Hard, "testUsername1")]
        [TestCase((Core.Enums.AccountType)1, 2, Core.Enums.GuitarDailyMaintanance.CleanHands, Core.Enums.AccountType.Admin, null, "testPassword2", Core.Enums.PlayStyle.Moderate, "testUsername2")]
        public void ShouldHaveEmailErrors(Core.Enums.AccountType? accountType, int? userId, Core.Enums.GuitarDailyMaintanance dailyMaintenance, Core.Enums.AccountType accountTypeToAdd,
            string email, string password, Core.Enums.PlayStyle playStyle, string username)
        {
            var testUserRequest = new AddUserRequest()
            {
                AccountType = accountType,
                UserId = userId,
                DailyMaintanance = dailyMaintenance,
                AccountTypeToAdd = accountTypeToAdd,
                Email = email,
                Password = password,
                PlayStyle = playStyle,
                Username = username
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.Email);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, Core.Enums.GuitarDailyMaintanance.PlayAsIs, Core.Enums.AccountType.User, "testEmail1", "", Core.Enums.PlayStyle.Hard, "testUsername1")]
        [TestCase((Core.Enums.AccountType)1, 2, Core.Enums.GuitarDailyMaintanance.CleanHands, Core.Enums.AccountType.Admin, "testEmail2", null, Core.Enums.PlayStyle.Moderate, "testUsername2")]
        public void ShouldHavePasswordErrors(Core.Enums.AccountType? accountType, int? userId, Core.Enums.GuitarDailyMaintanance dailyMaintenance, Core.Enums.AccountType accountTypeToAdd,
            string email, string password, Core.Enums.PlayStyle playStyle, string username)
        {
            var testUserRequest = new AddUserRequest()
            {
                AccountType = accountType,
                UserId = userId,
                DailyMaintanance = dailyMaintenance,
                AccountTypeToAdd = accountTypeToAdd,
                Email = email,
                Password = password,
                PlayStyle = playStyle,
                Username = username
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.Password);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, Core.Enums.GuitarDailyMaintanance.PlayAsIs, Core.Enums.AccountType.User, "testEmail1", "testPassword1", (Core.Enums.PlayStyle)11, "testUsername1")]
        [TestCase((Core.Enums.AccountType)1, 2, Core.Enums.GuitarDailyMaintanance.CleanHands, Core.Enums.AccountType.Admin, "testEmail2", "testPassword2", (Core.Enums.PlayStyle)17, "testUsername2")]
        [TestCase((Core.Enums.AccountType)0, 3, Core.Enums.GuitarDailyMaintanance.CleanHandsWipedStrings, Core.Enums.AccountType.User, "testEmail3", "testPassword3", (Core.Enums.PlayStyle)(-6), "testUsername3")]
        public void ShouldHavePlayStyleErrors(Core.Enums.AccountType? accountType, int? userId, Core.Enums.GuitarDailyMaintanance dailyMaintenance, Core.Enums.AccountType accountTypeToAdd,
            string email, string password, Core.Enums.PlayStyle playStyle, string username)
        {
            var testUserRequest = new AddUserRequest()
            {
                AccountType = accountType,
                UserId = userId,
                DailyMaintanance = dailyMaintenance,
                AccountTypeToAdd = accountTypeToAdd,
                Email = email,
                Password = password,
                PlayStyle = playStyle,
                Username = username
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.PlayStyle);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, 1, Core.Enums.GuitarDailyMaintanance.PlayAsIs, Core.Enums.AccountType.User, "testEmail1", "testPassword1", Core.Enums.PlayStyle.Hard, "")]
        [TestCase((Core.Enums.AccountType)1, 2, Core.Enums.GuitarDailyMaintanance.CleanHands, Core.Enums.AccountType.Admin, "testEmail2", "testPassword2", Core.Enums.PlayStyle.Moderate, null)]
        public void ShouldHaveUsernameErrors(Core.Enums.AccountType? accountType, int? userId, Core.Enums.GuitarDailyMaintanance dailyMaintenance, Core.Enums.AccountType accountTypeToAdd,
            string email, string password, Core.Enums.PlayStyle playStyle, string username)
        {
            var testUserRequest = new AddUserRequest()
            {
                AccountType = accountType,
                UserId = userId,
                DailyMaintanance = dailyMaintenance,
                AccountTypeToAdd = accountTypeToAdd,
                Email = email,
                Password = password,
                PlayStyle = playStyle,
                Username = username
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.Username);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)(-1), 1, Core.Enums.GuitarDailyMaintanance.PlayAsIs, Core.Enums.AccountType.User, "testEmail1", "testPassword1", Core.Enums.PlayStyle.Hard, "testUsername1")]
        [TestCase((Core.Enums.AccountType)8, 2, Core.Enums.GuitarDailyMaintanance.CleanHands, Core.Enums.AccountType.Admin, "testEmail2", "testPassword2", Core.Enums.PlayStyle.Moderate, "testUsername2")]
        [TestCase(null, 3, Core.Enums.GuitarDailyMaintanance.CleanHandsWipedStrings, Core.Enums.AccountType.User, "testEmail3", "testPassword3", Core.Enums.PlayStyle.Light, "testUsername3")]
        public void ShouldHaveAccountTypeErrors(Core.Enums.AccountType? accountType, int? userId, Core.Enums.GuitarDailyMaintanance dailyMaintenance, Core.Enums.AccountType accountTypeToAdd,
            string email, string password, Core.Enums.PlayStyle playStyle, string username)
        {
            var testUserRequest = new AddUserRequest()
            {
                AccountType = accountType,
                UserId = userId,
                DailyMaintanance = dailyMaintenance,
                AccountTypeToAdd = accountTypeToAdd,
                Email = email,
                Password = password,
                PlayStyle = playStyle,
                Username = username
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.AccountType);
        }

        [Test]
        [TestCase((Core.Enums.AccountType)0, -1, Core.Enums.GuitarDailyMaintanance.PlayAsIs, Core.Enums.AccountType.User, "testEmail1", "testPassword1", Core.Enums.PlayStyle.Hard, "testUsername1")]
        [TestCase((Core.Enums.AccountType)1, 0, Core.Enums.GuitarDailyMaintanance.CleanHands, Core.Enums.AccountType.Admin, "testEmail2", "testPassword2", Core.Enums.PlayStyle.Moderate, "testUsername2")]
        [TestCase((Core.Enums.AccountType)0, null, Core.Enums.GuitarDailyMaintanance.CleanHandsWipedStrings, Core.Enums.AccountType.User, "testEmail3", "testPassword3", Core.Enums.PlayStyle.Light, "testUsername3")]
        public void ShouldHaveUserIdErrors(Core.Enums.AccountType? accountType, int? userId, Core.Enums.GuitarDailyMaintanance dailyMaintenance, Core.Enums.AccountType accountTypeToAdd,
    string email, string password, Core.Enums.PlayStyle playStyle, string username)
        {
            var testUserRequest = new AddUserRequest()
            {
                AccountType = accountType,
                UserId = userId,
                DailyMaintanance = dailyMaintenance,
                AccountTypeToAdd = accountTypeToAdd,
                Email = email,
                Password = password,
                PlayStyle = playStyle,
                Username = username
            };
            var result = validator.TestValidate(testUserRequest);
            result.ShouldHaveValidationErrorFor(request => request.UserId);
        }
    }
}

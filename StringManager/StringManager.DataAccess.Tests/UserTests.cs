using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using System.Linq;

namespace StringManager.DataAccess.Tests
{
    public class UserTests
    {
        private DbContextOptions<StringManagerStorageContext> options;

        [OneTimeSetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<StringManagerStorageContext>()
                .UseInMemoryDatabase(databaseName: "StringManagerDatabase").Options;

            using (var context = new StringManagerStorageContext(options))
            {
                context.Users.Add(new User()
                {
                    DailyMaintanance = Core.Enums.GuitarDailyMaintanance.WipedString,
                    AccountType = Core.Enums.AccountType.Admin,
                    Email = "TestEmail1",
                    Password = "TestPassword1",
                    PlayStyle = Core.Enums.PlayStyle.Hard,
                    Username = "TestUsername1"
                });
                context.Users.Add(new User()
                {
                    DailyMaintanance = Core.Enums.GuitarDailyMaintanance.PlayAsIs,
                    AccountType = Core.Enums.AccountType.User,
                    Email = "TestEmail2",
                    Password = "TestPassword2",
                    PlayStyle = Core.Enums.PlayStyle.Moderate,
                    Username = "TestUsername2"
                });
                context.SaveChanges();
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            using (var context = new StringManagerStorageContext(options))
            {
                context.Database.EnsureDeleted();
            }
        }

        [Test]
        public void AddUserCommand_success()
        {
            //Arrange
            var mockParameter = new User()
            {
                DailyMaintanance = Core.Enums.GuitarDailyMaintanance.CleanHandsWipedStrings,
                AccountType = Core.Enums.AccountType.User,
                Email = "TestEmail3",
                Password = "TestPassword3",
                PlayStyle = Core.Enums.PlayStyle.Light,
                Username = "TestUsername3"
            };
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var UserCommand = new AddUserCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(UserCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(mockParameter.DailyMaintanance, response.DailyMaintanance);
                Assert.AreEqual(mockParameter.AccountType, response.AccountType);
                Assert.AreEqual(mockParameter.Email, response.Email);
                Assert.AreEqual(mockParameter.Password, response.Password);
                Assert.AreEqual(mockParameter.PlayStyle, response.PlayStyle);
                Assert.AreEqual(mockParameter.Username, response.Username);
            }
        }

        [Test]
        public void ModifyUserCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var mockParameter = context.Users.FirstOrDefault(x => x.Id == 1);
                mockParameter.Username = "testChangedUsername";
                var commandExecutor = new CommandExecutor(context);
                var UserCommand = new ModifyUserCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(UserCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
                Assert.AreEqual(mockParameter.Username, response.Username);
                Assert.AreEqual(mockParameter.Email, response.Email);
            }
        }

        [Test]
        public void GetUsersQuery_all()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var UserQuery = new GetUsersQuery();

                //Act
                var response = queryExecutor.Execute(UserQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Count > 2);
            }
        }

        [Test]
        public void GetUsersQuery_specific()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var UserQuery = new GetUsersQuery()
                {
                    Type = Core.Enums.AccountType.Admin
                };

                //Act
                var response = queryExecutor.Execute(UserQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Count == 1);
            }
        }

        [Test]
        public void GetUserByIdQuery_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var UserQuery = new GetUserByIdQuery()
                {
                    Id = 1
                };

                //Act
                var response = queryExecutor.Execute(UserQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
            }
        }

        [Test]
        public void GetUserByIdQuery_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var UserQuery = new GetUserByIdQuery()
                {
                    Id = 5
                };

                //Act
                var response = queryExecutor.Execute(UserQuery).Result;

                //Assert
                Assert.IsNull(response);
            }
        }

        [Test]
        public void GetUserByUsername_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var UserQuery = new GetUserByUsernameQuery()
                {
                    Username = "TestUsername2"
                };

                //Act
                var response = queryExecutor.Execute(UserQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual("TestUsername2", response.Username);
            }
        }

        [Test]
        public void GetUserByUsername_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var UserQuery = new GetUserByUsernameQuery()
                {
                    Username = "NoSuchUserTest"
                };

                //Act
                var response = queryExecutor.Execute(UserQuery).Result;

                //Assert
                Assert.IsNull(response);
            }
        }
    }
}

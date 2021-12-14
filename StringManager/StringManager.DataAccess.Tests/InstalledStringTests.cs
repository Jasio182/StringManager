using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using System.Linq;

namespace StringManager.DataAccess.Tests.Commands
{
    public class InstalledStringTests : DbContextTestsBase
    {
        public InstalledStringTests() : base()
        {
            using (var context = new StringManagerStorageContext(options))
            {
                context.InstalledStrings.Add( new InstalledString() { Id = 1, MyInstrument = new MyInstrument(), Position = 1, String = new String(), Tone = new Tone() });
                context.InstalledStrings.Add( new InstalledString() { Id = 2, MyInstrument = new MyInstrument(), Position = 2, String = new String(), Tone = new Tone() });
                context.SaveChanges();
            }
        }

        [Test]
        public void AddInstalledStringCommand_success()
        {
            //Arrange
            var mockParameter = new InstalledString()
            {
                MyInstrument = new MyInstrument(),
                Position = 2,
                String = new String(),
                Tone = new Tone()
            };
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var installedStringCommand = new AddInstalledStringCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(installedStringCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(3, response.Id);
                Assert.AreEqual(mockParameter.MyInstrument, response.MyInstrument);
                Assert.AreEqual(mockParameter.Position, response.Position);
                Assert.AreEqual(mockParameter.String, response.String);
                Assert.AreEqual(mockParameter.Tone, response.Tone);
            }
        }

        [Test]
        public void ModifyInstalledStringCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var mockParameter = context.InstalledStrings.FirstOrDefault(x => x.Id == 1);
                var commandExecutor = new CommandExecutor(context);
                var installedStringCommand = new ModifyInstalledStringCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(installedStringCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
                Assert.AreEqual(mockParameter.MyInstrument, response.MyInstrument);
                Assert.AreEqual(mockParameter.Position, response.Position);
                Assert.AreEqual(mockParameter.String, response.String);
                Assert.AreEqual(mockParameter.Tone, response.Tone);
            }
        }

        [Test]
        public void GetInstalledStringQuery_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var installedStringQuery = new GetInstalledStringQuery()
                {
                    AccountType = Core.Enums.AccountType.Admin,
                    UserId = 1,
                    Id = 1
                };

                //Act
                var response = queryExecutor.Execute(installedStringQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
            }
        }

        [Test]
        public void GetInstalledStringQuery_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var installedStringQuery = new GetInstalledStringQuery()
                {
                    AccountType = Core.Enums.AccountType.Admin,
                    UserId = 1,
                    Id = 7
                };

                //Act
                var response = queryExecutor.Execute(installedStringQuery).Result;

                //Assert
                Assert.IsNull(response);
            }
        }

        [Test]
        public void GetInstalledStringQuery_NotAuthorized()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var installedStringQuery = new GetInstalledStringQuery()
                {
                    AccountType = Core.Enums.AccountType.User,
                    UserId = 1,
                    Id = 7
                };

                //Act
                var response = queryExecutor.Execute(installedStringQuery).Result;

                //Assert
                Assert.IsNull(response);
            }
        }

        [Test]
        public void RemoveInstalledStringCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var installedStringCommand = new RemoveInstalledStringCommand()
                {
                    Parameter = 2
                };

                //Act
                var response = commandExecutor.Execute(installedStringCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(2, response.Id);
            }
        }

        [Test]
        public void RemoveInstalledStringCommand_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var installedStringCommand = new RemoveInstalledStringCommand()
                {
                    Parameter = 6
                };

                //Act
                var response = commandExecutor.Execute(installedStringCommand).Result;

                //Assert
                Assert.IsNull(response);
            }
        }
    }
}

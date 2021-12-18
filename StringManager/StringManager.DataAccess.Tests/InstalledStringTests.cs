using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using System.Linq;

namespace StringManager.DataAccess.Tests
{
    public class InstalledStringTests
    {
        private User testUser = new User()
        {
            DailyMaintanance = Core.Enums.GuitarDailyMaintanance.WipedString,
            AccountType = Core.Enums.AccountType.Admin,
            Email = "TestEmail",
            Password = "TestPassword",
            PlayStyle = Core.Enums.PlayStyle.Hard,
            Username = "TestUsername"
        };
        private MyInstrument testMyInstrument = new MyInstrument()
        {
            LastDeepCleaning = new System.DateTime(2021, 3, 21),
            LastStringChange = new System.DateTime(2021, 3, 21),
            NextDeepCleaning = new System.DateTime(2021, 8, 24),
            NextStringChange = new System.DateTime(2021, 2, 11),
            GuitarPlace = Core.Enums.WhereGuitarKept.Stand,
            HoursPlayedWeekly = 12,
            NeededLuthierVisit = false,
            OwnName = "TestName",
            UserId = 1
        };
        private Manufacturer testManufacturer = new Manufacturer()
        {
            Name = "testManufacturer"
        };
        private String[] testStrings =
        {
            new String()
            {
                NumberOfDaysGood = 128,
                Size = 11,
                SpecificWeight = 0.0001,
                StringType = Core.Enums.StringType.PlainNikled,
                ManufacturerId = 1
            },
            new String()
            {
                NumberOfDaysGood = 128,
                Size = 9,
                SpecificWeight = 0.00007,
                StringType = Core.Enums.StringType.PlainNikled,
                ManufacturerId = 1
            },
            new String()
            {
                Id = 3,
                NumberOfDaysGood = 128,
                Size = 52,
                SpecificWeight = 0.0008,
                StringType = Core.Enums.StringType.WoundNikled,
                ManufacturerId = 1
            }
        };
        private Tone[] testTones =
        {
            new Tone()
            {
                Frequency = 11.2,
                Name = "TestTone1",
                WaveLenght = 12
            },
            new Tone()
            {
                Frequency = 1254,
                Name = "TestTone2",
                WaveLenght = 16
            },
            new Tone()
            {
                Frequency = 18,
                Name = "TestTone3",
                WaveLenght = 11
            }
        };

        private DbContextOptions<StringManagerStorageContext> options;

        [OneTimeSetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<StringManagerStorageContext>()
                .UseInMemoryDatabase(databaseName: "StringManagerDatabase").Options;

            using (var context = new StringManagerStorageContext(options))
            {
                context.Users.Add(testUser);
                context.Manufacturers.Add(testManufacturer);
                context.MyInstruments.Add(testMyInstrument);
                for (int i = 0; i < testTones.Length; i++)
                {
                    context.Strings.Add(testStrings[i]);
                    context.Tones.Add(testTones[i]);
                }
                context.InstalledStrings.Add(new InstalledString()
                {
                    MyInstrumentId = 1,
                    Position = 1,
                    StringId = 1,
                    ToneId = 1
                });
                context.InstalledStrings.Add(new InstalledString()
                {
                    MyInstrumentId = 1,
                    Position = 2,
                    StringId = 2,
                    ToneId = 2
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
        public void AddInstalledStringCommand_success()
        {
            //Arrange
            var mockParameter = new InstalledString()
            {
                MyInstrumentId = 1,
                Position = 3,
                StringId = 3,
                ToneId = 3
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
                Assert.AreEqual(mockParameter.MyInstrumentId, response.MyInstrumentId);
                Assert.AreEqual(mockParameter.Position, response.Position);
                Assert.AreEqual(mockParameter.StringId, response.StringId);
                Assert.AreEqual(mockParameter.ToneId, response.ToneId);
            }
        }

        [Test]
        public void ModifyInstalledStringCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var mockParameter = context.InstalledStrings.FirstOrDefault(x => x.Id == 1);
                mockParameter.Position = 4;
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
                Assert.AreEqual(mockParameter.MyInstrumentId, response.MyInstrumentId);
                Assert.AreEqual(mockParameter.Position, response.Position);
                Assert.AreEqual(mockParameter.StringId, response.StringId);
                Assert.AreEqual(mockParameter.ToneId, response.ToneId);
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
                Assert.AreEqual(testMyInstrument.Id, response.MyInstrument.Id);
                Assert.AreEqual(testStrings[0].Id, response.String.Id);
                Assert.AreEqual(testTones[0].Id, response.Tone.Id);
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

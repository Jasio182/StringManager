using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using System.Linq;

namespace StringManager.DataAccess.Tests
{
    public class MyInstrumentTests
    {
        private Manufacturer testManufacturer = new Manufacturer()
        {
            Name = "TestManufacturer"
        };
        private Instrument[] testInstruments =
        {
            new Instrument()
            {
                ScaleLenghtTreble = 628,
                ScaleLenghtBass = 628,
                NumberOfStrings = 6,
                Model = "TestModel1",
                ManufacturerId = 1
            },
            new Instrument()
            {
                ScaleLenghtTreble = 631,
                ScaleLenghtBass = 658,
                NumberOfStrings = 7,
                Model = "TestModel2",
                ManufacturerId = 1
            },
            new Instrument()
            {
                ScaleLenghtTreble = 631,
                ScaleLenghtBass = 658,
                NumberOfStrings = 7,
                Model = "TestModel3",
                ManufacturerId = 1
            }
        };

        private User testUser = new User()
        {
            DailyMaintanance = Core.Enums.GuitarDailyMaintanance.WipedString,
            AccountType = Core.Enums.AccountType.Admin,
            Email = "TestEmail",
            Password = "TestPassword",
            PlayStyle = Core.Enums.PlayStyle.Hard,
            Username = "TestUsername"
        };

        private DbContextOptions<StringManagerStorageContext> options;

        [OneTimeSetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<StringManagerStorageContext>()
                .UseInMemoryDatabase(databaseName: "StringManagerDatabase").Options;

            using (var context = new StringManagerStorageContext(options))
            {
                context.Manufacturers.Add(testManufacturer);
                context.Users.Add(testUser);
                foreach (var testInstrument in testInstruments)
                    context.Instruments.Add(testInstrument);
                context.MyInstruments.Add(new MyInstrument()
                {
                    LastDeepCleaning = new System.DateTime(2021, 3, 21),
                    LastStringChange = new System.DateTime(2021, 3, 21),
                    NextDeepCleaning = new System.DateTime(2021, 8, 24),
                    NextStringChange = new System.DateTime(2021, 2, 11),
                    GuitarPlace = Core.Enums.WhereGuitarKept.Stand,
                    HoursPlayedWeekly = 12,
                    NeededLuthierVisit = false,
                    OwnName = "TestName1",
                    UserId = 1,
                    InstrumentId = 1,
                });
                context.MyInstruments.Add(new MyInstrument()
                {
                    LastDeepCleaning = new System.DateTime(2021, 3, 22),
                    LastStringChange = new System.DateTime(2021, 3, 22),
                    NextDeepCleaning = new System.DateTime(2021, 7, 9),
                    NextStringChange = new System.DateTime(2021, 3, 1),
                    GuitarPlace = Core.Enums.WhereGuitarKept.SoftCase,
                    HoursPlayedWeekly = 5,
                    NeededLuthierVisit = true,
                    OwnName = "TestName2",
                    UserId = 1,
                    InstrumentId = 2,
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
        public void AddMyInstrumentCommand_success()
        {
            //Arrange
            var mockParameter = new MyInstrument()
            {
                LastDeepCleaning = new System.DateTime(2022, 3, 21),
                LastStringChange = new System.DateTime(2022, 3, 21),
                NextDeepCleaning = new System.DateTime(2022, 8, 24),
                NextStringChange = new System.DateTime(2022, 2, 11),
                GuitarPlace = Core.Enums.WhereGuitarKept.HardCase,
                HoursPlayedWeekly = 36,
                NeededLuthierVisit = false,
                OwnName = "TestName3",
                UserId = 1,
                InstrumentId = 3,
            };
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var MyInstrumentCommand = new AddMyInstrumentCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(MyInstrumentCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(mockParameter.LastDeepCleaning, response.LastDeepCleaning);
                Assert.AreEqual(mockParameter.LastStringChange, response.LastStringChange);
                Assert.AreEqual(mockParameter.NextDeepCleaning, response.NextDeepCleaning);
                Assert.AreEqual(mockParameter.NextStringChange, response.NextStringChange);
                Assert.AreEqual(mockParameter.GuitarPlace, response.GuitarPlace);
                Assert.AreEqual(mockParameter.HoursPlayedWeekly, response.HoursPlayedWeekly);
                Assert.AreEqual(mockParameter.NeededLuthierVisit, response.NeededLuthierVisit);
                Assert.AreEqual(mockParameter.OwnName, response.OwnName);
                Assert.AreEqual(mockParameter.UserId, response.UserId);
                Assert.AreEqual(mockParameter.InstrumentId, response.InstrumentId);
            }
        }

        [Test]
        public void ModifyMyInstrumentCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var mockParameter = context.MyInstruments.FirstOrDefault(x => x.Id == 1);
                mockParameter.OwnName = "testChangedName";
                var commandExecutor = new CommandExecutor(context);
                var MyInstrumentCommand = new ModifyMyInstrumentCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(MyInstrumentCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
                Assert.AreEqual(mockParameter.OwnName, response.OwnName);
                Assert.AreEqual(mockParameter.InstrumentId, response.InstrumentId);
            }
        }

        [Test]
        public void GetMyInstrumentsQuery_all()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var MyInstrumentQuery = new GetMyInstrumentsQuery();

                //Act
                var response = queryExecutor.Execute(MyInstrumentQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Count >= 2);
            }
        }

        [Test]
        public void GetMyInstrumentsQuery_forUserSuccess()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var MyInstrumentQuery = new GetMyInstrumentsQuery()
                {
                    UserId = 1
                };

                //Act
                var response = queryExecutor.Execute(MyInstrumentQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Count >= 2);
            }
        }

        [Test]
        public void GetMyInstrumentsQuery_forUserNotFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var MyInstrumentQuery = new GetMyInstrumentsQuery()
                {
                    UserId = 2
                };

                //Act
                var response = queryExecutor.Execute(MyInstrumentQuery).Result;

                //Assert
                Assert.IsEmpty(response);
            }
        }

        [Test]
        public void GetMyInstrumentQuery_success_UserInstrument()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var MyInstrumentQuery = new GetMyInstrumentQuery()
                {
                    Id = 1,
                    UserId = 1,
                    AccountType = Core.Enums.AccountType.User
                };

                //Act
                var response = queryExecutor.Execute(MyInstrumentQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
                Assert.AreEqual("TestName1", response.OwnName);
            }
        }

        [Test]
        public void GetMyInstrumentQuery_success_AdminInstrument()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var MyInstrumentQuery = new GetMyInstrumentQuery()
                {
                    Id = 1,
                    UserId = 2,
                    AccountType = Core.Enums.AccountType.Admin
                };

                //Act
                var response = queryExecutor.Execute(MyInstrumentQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
                Assert.AreEqual("TestName1", response.OwnName);
            }
        }

        [Test]
        public void GetMyInstrumentQuery_fail_WrongParameters()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var MyInstrumentQuery = new GetMyInstrumentQuery()
                {
                    Id = 1,
                    UserId = 2,
                    AccountType = Core.Enums.AccountType.User
                };

                //Act
                var response = queryExecutor.Execute(MyInstrumentQuery).Result;

                //Assert
                Assert.IsNull(response);
            }
        }

        [Test]
        public void GetMyInstrumentQuery_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var MyInstrumentQuery = new GetMyInstrumentQuery()
                {
                    Id = 8,
                    UserId = 1,
                    AccountType = Core.Enums.AccountType.User
                };

                //Act
                var response = queryExecutor.Execute(MyInstrumentQuery).Result;

                //Assert
                Assert.IsNull(response);
            }
        }

        [Test]
        public void RemoveMyInstrumentCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var MyInstrumentCommand = new RemoveMyInstrumentCommand()
                {
                    Parameter = new System.Tuple<int, int, Core.Enums.AccountType>(2,3,Core.Enums.AccountType.Admin)
                };

                //Act
                var response = commandExecutor.Execute(MyInstrumentCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(2, response.Id);
            }
        }

        [Test]
        public void RemoveMyInstrumentCommand_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var MyInstrumentCommand = new RemoveMyInstrumentCommand()
                {
                    Parameter = new System.Tuple<int, int, Core.Enums.AccountType>(6, 3, Core.Enums.AccountType.Admin)
                };

                //Act
                var response = commandExecutor.Execute(MyInstrumentCommand).Result;

                //Assert
                Assert.IsNull(response);
            }
        }
    }
}

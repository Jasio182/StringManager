using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StringManager.DataAccess.CQRS;
using StringManager.DataAccess.CQRS.Commands;
using StringManager.DataAccess.CQRS.Queries;
using StringManager.DataAccess.Entities;
using System.Linq;

namespace StringManager.DataAccess.Tests
{
    public class StringSetTests
    {
        private String[] testStrings =
        {
            new String()
            {
                NumberOfDaysGood = 11,
                Size = 11,
                SpecificWeight = 0.001,
                StringType = Core.Enums.StringType.PlainNikled,
                ManufacturerId = 1
            },
            new String()
            {
                NumberOfDaysGood = 11,
                Size = 11,
                SpecificWeight = 0.001,
                StringType = Core.Enums.StringType.WoundBrass,
                ManufacturerId = 1
            }
        };
        private StringInSet[] testStringsInSets =
        {
            new StringInSet()
            {
                Position = 1,
                StringId = 1,
                StringsSetId = 1,
            },
            new StringInSet()
            {
                Position = 1,
                StringId = 1,
                StringsSetId = 2,
            },
            new StringInSet()
            {
                Position = 2,
                StringId = 2,
                StringsSetId = 1,
            },
            new StringInSet()
            {
                Position = 2,
                StringId = 2,
                StringsSetId = 2,
            },
            new StringInSet()
            {
                Position = 1,
                StringId = 1,
                StringsSetId = 3,
            },
            new StringInSet()
            {
                Position = 2,
                StringId = 2,
                StringsSetId = 3,
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
                context.StringsSets.Add(new StringsSet()
                {
                    Name = "TestStringsSet1",
                    NumberOfStrings = 2
                });
                context.StringsSets.Add(new StringsSet()
                {
                    Name = "TestStringsSet2",
                    NumberOfStrings = 2
                });
                foreach(var testString in testStrings)
                {
                    context.Strings.Add(testString);
                }
                foreach(var testStringInSet in testStringsInSets)
                {
                    context.StringsInSets.Add(testStringInSet);
                }
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
        public void AddStringsSetCommand_success()
        {
            //Arrange
            var mockParameter = new StringsSet()
            {
                Name = "TestStringsSet2",
                NumberOfStrings = 2
            };
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var StringsSetCommand = new AddStringsSetCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(StringsSetCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(mockParameter.Name, response.Name);
                Assert.AreEqual(mockParameter.NumberOfStrings, response.NumberOfStrings);
            }
        }

        [Test]
        public void ModifyStringsSetCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var mockParameter = context.StringsSets.FirstOrDefault(x => x.Id == 1);
                mockParameter.Name = "testChangedName";
                var commandExecutor = new CommandExecutor(context);
                var StringsSetCommand = new ModifyStringsSetCommand()
                {
                    Parameter = mockParameter
                };

                //Act
                var response = commandExecutor.Execute(StringsSetCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
                Assert.AreEqual(mockParameter.Name, response.Name);
                Assert.AreEqual(mockParameter.NumberOfStrings, response.NumberOfStrings);
            }
        }

        [Test]
        public void GetStringsSetsQuery_all()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var StringsSetQuery = new GetStringsSetsQuery();

                //Act
                var response = queryExecutor.Execute(StringsSetQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Count >= 2);
            }
        }

        [Test]
        public void GetStringsSetsQuery_specificStringType()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var StringsSetQuery = new GetStringsSetsQuery()
                {
                    StringType = Core.Enums.StringType.PlainNikled
                };

                //Act
                var response = queryExecutor.Execute(StringsSetQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Count >= 1);
            }
        }

        [Test]
        public void GetStringsSetQuery_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var StringsSetQuery = new GetStringsSetQuery()
                {
                    Id = 1
                };

                //Act
                var response = queryExecutor.Execute(StringsSetQuery).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Id);
            }
        }

        [Test]
        public void GetStringsSetQuery_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var StringsSetQuery = new GetStringsSetQuery()
                {
                    Id = 8
                };

                //Act
                var response = queryExecutor.Execute(StringsSetQuery).Result;

                //Assert
                Assert.IsNull(response);
            }
        }

        [Test]
        public void RemoveStringsSetCommand_success()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var StringsSetCommand = new RemoveStringsSetCommand()
                {
                    Parameter = 2
                };

                //Act
                var response = commandExecutor.Execute(StringsSetCommand).Result;

                //Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(2, response.Id);
            }
        }

        [Test]
        public void RemoveStringsSetCommand_notFound()
        {
            //Arrange
            using (var context = new StringManagerStorageContext(options))
            {
                var commandExecutor = new CommandExecutor(context);
                var StringsSetCommand = new RemoveStringsSetCommand()
                {
                    Parameter = 6
                };

                //Act
                var response = commandExecutor.Execute(StringsSetCommand).Result;

                //Assert
                Assert.IsNull(response);
            }
        }
    }
}

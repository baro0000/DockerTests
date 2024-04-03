using Microsoft.EntityFrameworkCore;
using Moq;
using SOTags.DataAccess;
using SOTags.DataAccess.Components;
using SOTags.DataAccess.Entities;

namespace SOTags.Tests.UnitTests.SOTags.DataAccessTests
{
    public class InitialDataLoaderTests : TestsBase
    {
        [Fact]
        public void LoadData_AddsTagsToContextAndSavesChanges()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseDbContext>()
                .UseInMemoryDatabase(databaseName: "InitialDataLoaderTestDatabase")
                .Options;

            using (var context = new DatabaseDbContext(options))
            {
                var loader = new InitialDataLoader();
                var tagList = new List<Tag>();
                var obj1 = new Tag { Name = "TestTag1", Count = 999 };
                var obj2 = new Tag { Name = "TestTag2", Count = 999 };
                var obj3 = new Tag { Name = "TestTag3", Count = 999 };
                tagList.Add(obj1);
                tagList.Add(obj2);
                tagList.Add(obj3);
                // Act
                loader.LoadData(tagList, context);

                // Assert
                var addedTags = context.Tags.ToList();
                Assert.Equal(3, addedTags.Count);

                ClearDatabase(context);
            }
        }
    }
}

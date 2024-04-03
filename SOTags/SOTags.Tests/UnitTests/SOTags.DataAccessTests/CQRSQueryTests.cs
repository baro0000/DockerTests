using Microsoft.EntityFrameworkCore;
using SOTags.DataAccess.Components;
using SOTags.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOTags.DataAccess.Entities;
using SOTags.DataAccess.CQRS.Queries;
using SOTags.DataAccess.CQRS;

namespace SOTags.Tests.UnitTests.SOTags.DataAccessTests
{
    public class CQRSQueryTests : TestsBase
    {
        [Fact]
        public async Task ExecuteGetTagsQuery_ReturnsTagList()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseDbContext>()
                .UseInMemoryDatabase(databaseName: "QueryTests")
                .Options;

            using (var context = new DatabaseDbContext(options))
            {
                var queryExecutor = new QueryExecutor(context);
                var tagList = new List<Tag>
                {
                    new Tag { Name = "TestTag1", Count = 999 },
                    new Tag { Name = "TestTag2", Count = 999 },
                    new Tag { Name = "TestTag3", Count = 999 }
                };
                context.Tags.AddRange(tagList);
                context.SaveChanges();

                // Act
                var result = await queryExecutor.Execute(new GetTagsQuery());


                // Assert
                Assert.Equal(3, result.Count);

                ClearDatabase(context);
            }
        }
    }
}

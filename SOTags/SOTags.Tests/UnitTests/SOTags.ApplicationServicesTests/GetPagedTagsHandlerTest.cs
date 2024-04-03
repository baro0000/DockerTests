using AutoMapper;
using FluentAssertions;
using Moq;
using SOTags.ApplicationServices.API.Domain;
using SOTags.ApplicationServices.API.Domain.Models;
using SOTags.ApplicationServices.API.Handlers;
using SOTags.DataAccess.CQRS;
using SOTags.DataAccess.CQRS.Queries;
using SOTags.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTags.Tests.UnitTests.SOTags.ApplicationServicesTests
{
    public class GetPagedTagsHandlerTest
    {
        [Fact]
        public async Task GetPagedTagsHandler_ReturnsCorrectPagedResult()
        {
            // Arrange
            var request = new GetPagedTagsRequest()
            {
                SortByName = "DESC",
                SortByCount = null,
                Page = 2,
                PageSize = 10
            };
            var tagList = GenerateSampleTags();
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataAccess.Entities.Tag, ApplicationServices.API.Domain.Models.Tag>(); 
            }).CreateMapper();

            var queryExecutorMock = new Mock<IQueryExecutor>();
            queryExecutorMock.Setup(executor => executor.Execute(It.IsAny<GetTagsQuery>())).ReturnsAsync(tagList);
            var handler = new GetPagedTagsHandler(mapper, queryExecutorMock.Object);

            // Act
            var response = await handler.Handle(request, default);

            // Assert
            response.Should().NotBeNull();
            response.Data.Should().NotBeNull();
            response.Data.TotalItemsCount.Should().Be(tagList.Count);
            response.Data.TotalPages.Should().Be(10);
            response.Data.ItemsFrom.Should().Be(11);
            response.Data.ItemsTo.Should().Be(20);
            response.Data.Items.Count.Should().Be(10);
            response.Data.Items.Count.Should().Be(10);
            response.Data.Items.Select(x => x.Name).Should().BeInDescendingOrder();


        }
        private List<DataAccess.Entities.Tag> GenerateSampleTags()
        {
            // Wygeneruj przykładowe dane
            var tags = new List<DataAccess.Entities.Tag>();
            for (int i = 1; i <= 100; i++)
            {
                tags.Add(new DataAccess.Entities.Tag { Id = i, Name = $"Tag{i}", Count = i * 10 });
            }
            return tags;
        }
    }
}

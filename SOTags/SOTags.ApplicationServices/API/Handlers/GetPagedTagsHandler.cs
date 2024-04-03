using AutoMapper;
using MediatR;
using SOTags.ApplicationServices.API.Domain;
using SOTags.ApplicationServices.API.Domain.Models;
using SOTags.DataAccess.CQRS;
using SOTags.DataAccess.CQRS.Queries;

namespace SOTags.ApplicationServices.API.Handlers
{
    public class GetPagedTagsHandler : IRequestHandler<GetPagedTagsRequest, GetPagedTagsResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;

        public GetPagedTagsHandler(IMapper mapper, IQueryExecutor queryExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
        }
        public async Task<GetPagedTagsResponse> Handle(GetPagedTagsRequest request, CancellationToken cancellationToken)
        {
            var sortByName = request.SortByName;
            var sortByCount = request.SortByCount;
            var page = request.Page;
            var pageSize = request.PageSize;

            var query = new GetTagsQuery();
            var tags = await queryExecutor.Execute(query);
            var mappedTags = mapper.Map<List<Tag>>(tags);
            var totalCount = mappedTags.Count;
            var pagedResult = new PagedResult(mappedTags, totalCount, pageSize, page, sortByName, sortByCount);

            var response = new GetPagedTagsResponse()
            {
                Data = pagedResult
            };

            return response;
        }
    }
}

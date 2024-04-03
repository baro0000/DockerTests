using MediatR;

namespace SOTags.ApplicationServices.API.Domain
{
    public class GetPagedTagsRequest : IRequest<GetPagedTagsResponse>
    {
        public string? SortByName { get; set; }
        public string? SortByCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

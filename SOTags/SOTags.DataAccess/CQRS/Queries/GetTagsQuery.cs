using Microsoft.EntityFrameworkCore;
using SOTags.DataAccess.Entities;

namespace SOTags.DataAccess.CQRS.Queries
{
    public class GetTagsQuery : QueryBase<List<Entities.Tag>>
    {
        public override Task<List<Tag>> Execute(DatabaseDbContext context)
        {
            return context.Tags.ToListAsync();
        }
    }
}

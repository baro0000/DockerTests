using SOTags.DataAccess.CQRS.Queries;

namespace SOTags.DataAccess.CQRS
{
    public interface IQueryExecutor
    {
        Task<TResult> Execute<TResult>(QueryBase<TResult> query);
    }
}
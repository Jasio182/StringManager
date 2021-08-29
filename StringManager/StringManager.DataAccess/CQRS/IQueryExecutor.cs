using StringManager.DataAccess.CQRS.Queries;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS
{
    public interface IQueryExecutor
    {
        Task<TResult> Execute<TResult>(QueryBase<TResult> query);
    }
}
using StringManager.DataAccess.CQRS.Queries;
using System.Threading.Tasks;

namespace StringManager.Services
{
    public interface IQueryExecutor
    {
        Task<TResult> Execute<TResult>(QueryBase<TResult> query);
    }
}
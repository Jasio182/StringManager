using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public abstract class QueryBase<TResult>
    {
        public abstract Task<TResult> Execute(StringManagerStorageContext context);
    }
}

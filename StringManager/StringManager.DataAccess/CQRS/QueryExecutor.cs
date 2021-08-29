using StringManager.DataAccess.CQRS.Queries;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly StringManagerStorageContext context;

        public QueryExecutor(StringManagerStorageContext context)
        {
            this.context = context;
        }

        public Task<TResult> Execute<TResult>(QueryBase<TResult> query)
        {
            return query.Execute(context);
        }
    }
}

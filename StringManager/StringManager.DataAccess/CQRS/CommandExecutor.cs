using StringManager.DataAccess.CQRS.Commands;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly StringManagerStorageContext context;

        public CommandExecutor(StringManagerStorageContext context)
        {
            this.context = context;
        }

        public Task<TResult> Execute<TParameter, TResult>(CommandBase<TParameter, TResult> query)
        {
            return query.Execute(context);
        }
    }
}

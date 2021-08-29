using StringManager.DataAccess.CQRS.Commands;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS
{
    public interface ICommandExecutor
    {
        Task<TResult> Execute<TParameter, TResult>(CommandBase<TParameter, TResult> query);
    }
}

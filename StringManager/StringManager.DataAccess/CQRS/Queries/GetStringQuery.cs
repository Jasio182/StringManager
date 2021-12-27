using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetStringQuery : QueryBase<String>
    {
        public int Id { get; set; }

        public override async Task<String> Execute(StringManagerStorageContext context)
        {
            try
            {
                return await context.Strings
                    .Include(thisString => thisString.Manufacturer)
                    .FirstAsync(singleString => singleString.Id == Id);
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }
    }
}

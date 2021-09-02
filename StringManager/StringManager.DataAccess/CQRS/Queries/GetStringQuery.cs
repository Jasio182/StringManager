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
            var singleString = await context.Strings
                .Include(thisString => thisString.Manufacturer)
                .FirstOrDefaultAsync(singleString => singleString.Id == Id);
            return singleString;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class RemoveManufacturerCommand : CommandBase<int, Manufacturer>
    {
        public override async Task<Manufacturer> Execute(StringManagerStorageContext context)
        {
            var manufacturerToDelete = await context.Manufacturers.FirstOrDefaultAsync(manufacturer => manufacturer.Id == Parameter);
            return manufacturerToDelete == null ? null : await Remove(context, manufacturerToDelete);
        }
        private async Task<Manufacturer> Remove(StringManagerStorageContext context, Manufacturer manufacturerToDelete)
        {
            context.Manufacturers.Remove(manufacturerToDelete);
            await context.SaveChangesAsync();
            context.Entry(manufacturerToDelete).State = EntityState.Detached;
            return manufacturerToDelete;
        }
    }
}

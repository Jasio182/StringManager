using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class ModifyManufacturerCommand : CommandBase<Manufacturer, Manufacturer>
    {
        public override async Task<Manufacturer> Execute(StringManagerStorageContext context)
        {
            context.Manufacturers.Update(Parameter);
            await context.SaveChangesAsync();
            context.Entry(Parameter).State = EntityState.Detached;
            return Parameter;
        }
    }
}

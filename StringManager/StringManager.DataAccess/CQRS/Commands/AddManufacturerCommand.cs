using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class AddManufacturerCommand : CommandBase<Manufacturer, Manufacturer>
    {
        public override async Task<Manufacturer> Execute(StringManagerStorageContext context)
        {
            await context.Manufacturers.AddAsync(Parameter);
            await context.SaveChangesAsync();
            return Parameter;
        }
    }
}

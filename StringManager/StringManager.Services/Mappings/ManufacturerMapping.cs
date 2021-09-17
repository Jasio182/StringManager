using AutoMapper;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.Mappings
{
    public class ManufacturerMapping : Profile
    {
        public ManufacturerMapping()
        {
            CreateMap<Manufacturer, Core.Models.Manufacturer>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name));

            CreateMap<AddManufacturerRequest, Manufacturer>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name));
        }
    }
}

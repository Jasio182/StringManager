using AutoMapper;
using StringManager.DataAccess.Entities;

namespace StringManager.Services.Mappings
{
    public class StringInSetMapping : Profile
    {
        public StringInSetMapping()
        {
            CreateMap<StringInSet, Core.Models.StringInSet>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Position, y => y.MapFrom(z => z.Position))
                .ForMember(x => x.Size, y => y.MapFrom(z => z.String.Size))
                .ForMember(x => x.SpecificWeight, y => y.MapFrom(z => z.String.SpecificWeight))
                .ForMember(x => x.StringId, y => y.MapFrom(z => z.String.Id))
                .ForMember(x => x.StringType, y => y.MapFrom(z => z.String.StringType))
                .ForMember(x => x.Manufacturer, y => y.MapFrom(z => z.String.Manufacturer.Name));
        }
    }
}

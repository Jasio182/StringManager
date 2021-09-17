using AutoMapper;
using StringManager.DataAccess.Entities;

namespace StringManager.Services.Mappings
{
    class StringsMapping : Profile
    {
        public StringsMapping()
        {
            CreateMap<String, Core.Models.String>()
               .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
               .ForMember(x => x.Size, y => y.MapFrom(z => z.Size))
               .ForMember(x => x.NumberOfDaysGood, y => y.MapFrom(z => z.NumberOfDaysGood))
               .ForMember(x => x.SpecificWeight, y => y.MapFrom(z => z.SpecificWeight))
               .ForMember(x => x.StringType, y => y.MapFrom(z => z.StringType))
               .ForMember(x => x.Manufacturer, y => y.MapFrom(z => z.Manufacturer.Name));
        }
    }
}

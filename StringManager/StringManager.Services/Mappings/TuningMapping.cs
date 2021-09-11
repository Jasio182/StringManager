using AutoMapper;
using StringManager.DataAccess.Entities;

namespace StringManager.Services.Mappings
{
    public class TuningMapping : Profile
    {
        public TuningMapping()
        {
            CreateMap<Tuning, Core.Models.TuningList>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.NumberOfStrings, y => y.MapFrom(z => z.NumberOfStrings));

            CreateMap<Tuning, Core.Models.Tuning>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.NumberOfStrings, y => y.MapFrom(z => z.NumberOfStrings));
        }
    }
}

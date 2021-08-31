using AutoMapper;
using StringManager.DataAccess.Entities;

namespace StringManager.Services.Mappings
{
    class InstrumentMapping : Profile
    {
        public InstrumentMapping()
        {
            CreateMap<Instrument, Core.Models.Instrument>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Model, y => y.MapFrom(z => z.Model))
                .ForMember(x => x.NumberOfStrings, y => y.MapFrom(z => z.NumberOfStrings))
                .ForMember(x => x.ScaleLenghtBass, y => y.MapFrom(z => z.ScaleLenghtBass))
                .ForMember(x => x.ScaleLenghtTreble, y => y.MapFrom(z => z.ScaleLenghtTreble))
                .ForMember(x => x.Manufacturer, y => y.MapFrom(z => z.Manufacturer.Name));
        }
    }
}

using AutoMapper;
using StringManager.DataAccess.Entities;

namespace StringManager.Services.Mappings
{
    public class MyInstrumentMapping : Profile
    {
        public MyInstrumentMapping()
        {
            CreateMap<MyInstrument, Core.Models.MyInstrumentList>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Model, y => y.MapFrom(z => z.Instrument.Model))
                .ForMember(x => x.OwnName, y => y.MapFrom(z => z.OwnName))
                .ForMember(x => x.NumberOfStrings, y => y.MapFrom(z => z.Instrument.NumberOfStrings))
                .ForMember(x => x.ScaleLenghtBass, y => y.MapFrom(z => z.Instrument.ScaleLenghtBass))
                .ForMember(x => x.ScaleLenghtTreble, y => y.MapFrom(z => z.Instrument.ScaleLenghtTreble))
                .ForMember(x => x.Manufacturer, y => y.MapFrom(z => z.Instrument.Manufacturer.Name));

            CreateMap<MyInstrument, Core.Models.MyInstrument>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Model, y => y.MapFrom(z => z.Instrument.Model))
                .ForMember(x => x.OwnName, y => y.MapFrom(z => z.OwnName))
                .ForMember(x => x.NumberOfStrings, y => y.MapFrom(z => z.Instrument.NumberOfStrings))
                .ForMember(x => x.ScaleLenghtBass, y => y.MapFrom(z => z.Instrument.ScaleLenghtBass))
                .ForMember(x => x.ScaleLenghtTreble, y => y.MapFrom(z => z.Instrument.ScaleLenghtTreble))
                .ForMember(x => x.Manufacturer, y => y.MapFrom(z => z.Instrument.Manufacturer.Name));
        }
    }
}

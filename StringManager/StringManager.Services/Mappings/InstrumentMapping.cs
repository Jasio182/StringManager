using AutoMapper;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.DataAccess.Entities;

namespace StringManager.Services.Mappings
{
    public class InstrumentMapping : Profile
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

            CreateMap<System.Tuple<AddInstrumentRequest, Manufacturer>, Instrument>()
                .ForMember(x => x.Model, y => y.MapFrom(z => z.Item1.Model))
                .ForMember(x => x.NumberOfStrings, y => y.MapFrom(z => z.Item1.NumberOfStrings))
                .ForMember(x => x.ScaleLenghtBass, y => y.MapFrom(z => z.Item1.ScaleLenghtBass))
                .ForMember(x => x.ScaleLenghtTreble, y => y.MapFrom(z => z.Item1.ScaleLenghtTreble))
                .ForMember(x => x.Manufacturer, y => y.MapFrom(z => z.Item2));
        }
    }
}

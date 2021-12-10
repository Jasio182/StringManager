using AutoMapper;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain.Requests;

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
                .ForMember(x => x.Manufacturer, y => y.MapFrom(z => z.Instrument.Manufacturer.Name))
                .ForMember(x => x.HoursPlayedWeekly, y => y.MapFrom(z => z.HoursPlayedWeekly))
                .ForMember(x => x.GuitarPlace, y => y.MapFrom(z => z.GuitarPlace))
                .ForMember(x => x.NeededLuthierVisit, y => y.MapFrom(z => z.NeededLuthierVisit))
                .ForMember(x => x.LastDeepCleaning, y => y.MapFrom(z => z.LastDeepCleaning))
                .ForMember(x => x.NextDeepCleaning, y => y.MapFrom(z => z.NextDeepCleaning))
                .ForMember(x => x.LastStringChange, y => y.MapFrom(z => z.LastStringChange))
                .ForMember(x => x.NextStringChange, y => y.MapFrom(z => z.NextStringChange));

            CreateMap<System.Tuple<AddMyInstrumentRequest, Instrument, User>, MyInstrument>()
                .ForMember(x => x.NeededLuthierVisit, y => y.MapFrom(z => z.Item1.NeededLuthierVisit))
                .ForMember(x => x.OwnName, y => y.MapFrom(z => z.Item1.OwnName))
                .ForMember(x => x.GuitarPlace, y => y.MapFrom(z => z.Item1.GuitarPlace))
                .ForMember(x => x.LastDeepCleaning, y => y.MapFrom(z => z.Item1.LastDeepCleaning))
                .ForMember(x => x.LastStringChange, y => y.MapFrom(z => z.Item1.LastStringChange))
                .ForMember(x => x.Instrument, y => y.MapFrom(z => z.Item2))
                .ForMember(x => x.User, y => y.MapFrom(z => z.Item3));
        }
    }
}

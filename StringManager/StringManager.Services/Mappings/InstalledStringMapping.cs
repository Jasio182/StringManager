using AutoMapper;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.Mappings
{
    public class InstalledStringMapping : Profile
    {
        public InstalledStringMapping()
        {
            CreateMap<InstalledString, Core.Models.InstalledString>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Position, y => y.MapFrom(z => z.Position))
                .ForMember(x => x.StringId, y => y.MapFrom(z => z.String.Id))
                .ForMember(x => x.Size, y => y.MapFrom(z => z.String.Size))
                .ForMember(x => x.SpecificWeight, y => y.MapFrom(z => z.String.SpecificWeight))
                .ForMember(x => x.StringType, y => y.MapFrom(z => z.String.StringType))
                .ForMember(x => x.Manufacturer, y => y.MapFrom(z => z.String.Manufacturer.Name))
                .ForMember(x => x.ToneId, y => y.MapFrom(z => z.Tone.Id))
                .ForMember(x => x.ToneName, y => y.MapFrom(z => z.Tone.Name))
                .ForMember(x => x.Frequency, y => y.MapFrom(z => z.Tone.Frequency))
                .ForMember(x => x.WaveLenght, y => y.MapFrom(z => z.Tone.WaveLenght));

            CreateMap<System.Tuple<AddInstalledStringRequest, MyInstrument, String, Tone>, InstalledString>()
                .ForMember(x => x.Position, y => y.MapFrom(z => z.Item1.Position))
                .ForMember(x => x.MyInstrument, y => y.MapFrom(z => z.Item2))
                .ForMember(x => x.String, y => y.MapFrom(z => z.Item3))
                .ForMember(x => x.Tone, y => y.MapFrom(z => z.Item4));
        }
    }
}

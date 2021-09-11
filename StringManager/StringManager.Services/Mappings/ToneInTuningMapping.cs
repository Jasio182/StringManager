using AutoMapper;
using StringManager.DataAccess.Entities;

namespace StringManager.Services.Mappings
{
    public class ToneInTuningMapping : Profile
    {
        public ToneInTuningMapping()
        {
            CreateMap<ToneInTuning, Core.Models.ToneInTuning>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Position, y => y.MapFrom(z => z.Position))
                .ForMember(x => x.ToneName, y => y.MapFrom(z => z.Tone.Name))
                .ForMember(x => x.Frequency, y => y.MapFrom(z => z.Tone.Frequency))
                .ForMember(x => x.WaveLenght, y => y.MapFrom(z => z.Tone.WaveLenght));
        }
    }
}

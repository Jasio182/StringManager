using AutoMapper;
using StringManager.DataAccess.Entities;

namespace StringManager.Core.Mappings
{
    public class ToneProfile : Profile
    {
        public ToneProfile()
        {
            CreateMap<Tone, Models.Tone>()
                .ForMember(x=>x.Id, y=>y.MapFrom(z=>z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Frequency, y => y.MapFrom(z => z.Frequency))
                .ForMember(x => x.WaveLenght, y => y.MapFrom(z => z.WaveLenght));
        }
    }
}

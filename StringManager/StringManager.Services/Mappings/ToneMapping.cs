using AutoMapper;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.Mappings
{
    public class ToneMapping : Profile
    {
        public ToneMapping()
        {
            CreateMap<Tone, Core.Models.Tone>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Frequency, y => y.MapFrom(z => z.Frequency))
                .ForMember(x => x.WaveLenght, y => y.MapFrom(z => z.WaveLenght));

            CreateMap<AddToneRequest, Tone>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Frequency, y => y.MapFrom(z => z.Frequency))
                .ForMember(x => x.WaveLenght, y => y.MapFrom(z => z.WaveLenght));
        }
    }
}

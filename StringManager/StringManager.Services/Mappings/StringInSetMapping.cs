using AutoMapper;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.DataAccess.Entities;

namespace StringManager.Services.Mappings
{
    public class StringInSetMapping : Profile
    {
        public StringInSetMapping()
        {
            CreateMap<StringInSet, Core.Models.StringInSet>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Position, y => y.MapFrom(z => z.Position))
                .ForMember(x => x.Size, y => y.MapFrom(z => z.String.Size))
                .ForMember(x => x.SpecificWeight, y => y.MapFrom(z => z.String.SpecificWeight))
                .ForMember(x => x.StringId, y => y.MapFrom(z => z.String.Id))
                .ForMember(x => x.StringType, y => y.MapFrom(z => z.String.StringType))
                .ForMember(x => x.Manufacturer, y => y.MapFrom(z => z.String.Manufacturer.Name));

            CreateMap<System.Tuple<AddStringInSetRequest, StringsSet, String>, StringInSet>()
                .ForMember(x => x.Position, y => y.MapFrom(z => z.Item1.Position))
                .ForMember(x => x.StringsSet, y => y.MapFrom(z => z.Item2))
                .ForMember(x => x.String, y => y.MapFrom(z => z.Item3));
        }
    }
}

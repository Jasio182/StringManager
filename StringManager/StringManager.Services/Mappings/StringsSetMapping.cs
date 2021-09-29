﻿using AutoMapper;
using StringManager.DataAccess.Entities;
using StringManager.Services.API.Domain.Requests;

namespace StringManager.Services.Mappings
{
    public class StringsSetMapping : Profile
    {
        public StringsSetMapping()
        {
            CreateMap<StringsSet, Core.Models.StringsSet>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.NumberOfStrings, y => y.MapFrom(z => z.NumberOfStrings));

            CreateMap<AddStringsSetRequest, StringsSet>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.NumberOfStrings, y => y.MapFrom(z => z.NumberOfStrings));
        }
    }
    
}

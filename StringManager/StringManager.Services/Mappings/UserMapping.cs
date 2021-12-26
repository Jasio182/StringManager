using AutoMapper;
using StringManager.Core.MediatorRequestsAndResponses.Requests;
using StringManager.DataAccess.Entities;

namespace StringManager.Services.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, Core.Models.User>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Username, y => y.MapFrom(z => z.Username))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
                .ForMember(x => x.AccountType, y => y.MapFrom(z => z.AccountType))
                .ForMember(x => x.PlayStyle, y => y.MapFrom(z => z.PlayStyle))
                .ForMember(x => x.DailyMaintanance, y => y.MapFrom(z => z.DailyMaintanance));

            CreateMap<AddUserRequest, User>()
                .ForMember(x => x.Username, y => y.MapFrom(z => z.Username))
                .ForMember(x => x.Password, y => y.MapFrom(z => z.Password))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
                .ForMember(x => x.AccountType, y => y.MapFrom(z => z.AccountTypeToAdd))
                .ForMember(x => x.PlayStyle, y => y.MapFrom(z => z.PlayStyle))
                .ForMember(x => x.DailyMaintanance, y => y.MapFrom(z => z.DailyMaintanance));
        }
    }
}

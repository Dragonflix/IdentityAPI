using AutoMapper;
using IdentityBLL.Models;
using IdentityDAL.Entities;

namespace TimeTrackingBLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Role, RoleModel>()
                    .ReverseMap();
            CreateMap<User, UserModel>()
                    .ForMember(dest => dest.Sex, opt => opt.MapFrom(e => e.Sex.ToString()))
                    .ForMember(dest => dest.RoleName, opt => opt.MapFrom(e => e.Role.RoleName));
            CreateMap<UserModel, User>()
                    .ForMember(dest => dest.Sex, opt => opt.MapFrom(e => (Sex)Enum.Parse(typeof(Sex), e.Sex)));
        }
    }
}

using AutoMapper;

namespace AspNetCoreDemo
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Pepole, Pepole_E>()
                .ForMember(dest=>dest.name_e,opt=>opt.MapFrom(src=>src.name))
                .ForMember(dest=>dest.age_e,opt=>opt.MapFrom(src=>src.age))
                .ForMember(dest=>dest.gender_e,opt=>opt.MapFrom(src=>src.gender));
        }
    }
}

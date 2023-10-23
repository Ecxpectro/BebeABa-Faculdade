using AutoMapper;
using DB.Models;
using Shared.Models;

namespace Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        { 
            CreateMap<Users, UserModel>().ReverseMap();
        }
    }
}

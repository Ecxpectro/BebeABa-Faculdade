using AutoMapper;
using DB.Models;
using Shared.Models;

namespace Api.Profiles
{
    public class ChildrenProfile : Profile
    {
        public ChildrenProfile()
        {
            CreateMap<Children, ChildrenModel>().ReverseMap();
        }
    }
}

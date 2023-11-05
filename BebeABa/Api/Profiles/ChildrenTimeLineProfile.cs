using AutoMapper;
using DB.Models;
using Shared.Models;

namespace Api.Profiles
{
    public class ChildrenTimeLineProfile : Profile
    {
        public ChildrenTimeLineProfile()
        {
            CreateMap<ChildrenTimeLine, ChildrenTimeLineModel>().ReverseMap();
        }
    }
}

using AutoMapper;
using DB.Models;
using Shared.Models;

namespace Api.Profiles
{
    public class ForumAnswerProfile : Profile
    {
        public ForumAnswerProfile()
        {
            CreateMap<ForumAnswer, ForumAnswerModel>().ReverseMap();
        }
    }
}

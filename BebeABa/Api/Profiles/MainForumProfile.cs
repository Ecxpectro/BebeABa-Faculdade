using AutoMapper;
using DB.Models;
using Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Profiles
{
    public class MainForumProfile : Profile
    {
        public MainForumProfile()
        {
            CreateMap<MainForum, MainForumModel>().ReverseMap();
            CreateMap<MainForum, MainForumModel>()
                .ForMember(dest => dest.ForumAnswers, opt => opt.MapFrom(src => MapForumAnswers(src)))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => MapUser(src)));
        }
        private List<ForumAnswerModel> MapForumAnswers(MainForum mainForum)
        {
            List<ForumAnswerModel> result = null;

            if (mainForum is not null && mainForum.ForumRelation is not null && mainForum.ForumRelation.Any())
            {
                result = new List<ForumAnswerModel>(0);

                Parallel.ForEach(mainForum.ForumRelation, forumRelation =>
                {
                    if(forumRelation is not null) {
                        result.Add(new ForumAnswerModel()
                        {
                            ForumAnswer1 = forumRelation.ForumAnswer.ForumAnswer1,
                            ForumAnswerDate = forumRelation.ForumAnswer.ForumAnswerDate,
                            ForumAnswerId = forumRelation.ForumAnswerId,
                            UserId = forumRelation.ForumAnswer.UserId,
                            User = new UserModel()
                            {
                                UserFullName = forumRelation.ForumAnswer.User.UserFullName,
                                UserFilePath = forumRelation.ForumAnswer.User.UserFilePath
                            }
                        });

                    }
                  
                });
            }

            return result;
        }
        private UserModel MapUser(MainForum mainForum)
        {
            UserModel result = new UserModel();

            if (mainForum is not null && mainForum.User is not null)
            {
                result = new UserModel
                {
                    UserFilePath = mainForum.User.UserFilePath,
                    UserFullName = mainForum.User.UserFullName,
                };
            }

            return result;
        }
    }
}

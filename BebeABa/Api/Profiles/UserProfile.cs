using AutoMapper;
using DB.Models;
using Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace Api.Profiles
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<Users, UserModel>().ReverseMap();
			CreateMap<Users, UserModel>()
				.ForMember(dest => dest.Childrens, opt => opt.MapFrom(src => MapChildrens(src))); ;


		}
		private List<ChildrenModel> MapChildrens(Users users)
		{
			List<ChildrenModel> result = null;
			if (users is not null && users.Children is not null && users.Children.Any())
			{
				result = new List<ChildrenModel>(0);
				foreach (var children in users.Children)
				{
					result.Add(new ChildrenModel
					{
						BirthDate = children.BirthDate,
						ChildrenFatherName = children.ChildrenFatherName,
						ChildrenId = children.ChildrenId,
						ChildrenName = children.ChildrenName,
						ChildrenMotherName = children.ChildrenMotherName,
						ImgPath = children.ImgPath,
						UserId = children.UserId
					});
				}
			}
			return result;
		}
	}
}



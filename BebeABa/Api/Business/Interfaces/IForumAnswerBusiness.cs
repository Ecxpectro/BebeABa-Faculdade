using Shared.ApiUtilities;
using Shared.Models;
using System.Threading.Tasks;

namespace Api.Business.Interfaces
{
    public interface IForumAnswerBusiness
    {
        Task<Response> CreateAnswer(ForumAnswerModel forumAnswer);
        Task<Response> Delete(long id);
    }
}

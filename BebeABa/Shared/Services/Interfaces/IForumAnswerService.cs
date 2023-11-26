using Shared.ApiUtilities;
using Shared.Models;
using System.Threading.Tasks;

namespace Shared.Services.Interfaces
{
    public interface IForumAnswerService
    {
        Task<Response> CreateAnswer(ForumAnswerModel forumAnswer);
        Task<Response> DeleteAnswer(long id);
    }
}

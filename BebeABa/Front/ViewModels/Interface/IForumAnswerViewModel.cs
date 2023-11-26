using Shared.ApiUtilities;
using Shared.Models;
using System.Threading.Tasks;

namespace Front.ViewModels.Interface
{
    public interface IForumAnswerViewModel
    {
        Task<Response> CreateAnswer(ForumAnswerModel forumAnswer);
        Task<Response> DeleteAnswer(long id);
    }
}

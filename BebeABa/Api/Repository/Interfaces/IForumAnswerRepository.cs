using DB.Models;
using System.Threading.Tasks;

namespace Api.Repository.Interfaces
{
    public interface IForumAnswerRepository
    {
        Task<ForumAnswer> CreateAnswer(ForumAnswer forumAnswer, long mainForumId);
    }
}

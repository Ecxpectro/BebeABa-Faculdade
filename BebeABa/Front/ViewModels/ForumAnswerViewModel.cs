using Front.ViewModels.Interface;
using Shared.ApiUtilities;
using Shared.Models;
using Shared.Services.Interfaces;
using System.Threading.Tasks;

namespace Front.ViewModels
{
    public class ForumAnswerViewModel : IForumAnswerViewModel
    {
        private readonly IForumAnswerService _forumAnswerService;

        public ForumAnswerViewModel(IForumAnswerService forumAnswerService)
        {
            _forumAnswerService = forumAnswerService;
        }
        public async Task<Response> CreateAnswer(ForumAnswerModel forumAnswer) => await _forumAnswerService.CreateAnswer(forumAnswer);

        public async Task<Response> DeleteAnswer(long id)=> await _forumAnswerService.DeleteAnswer(id);
    }
}

using Front.ViewModels.Interface;
using Shared.ApiUtilities;
using Shared.Models;
using Shared.Services.Interfaces;
using System.Threading.Tasks;

namespace Front.ViewModels
{
    public class MainForumViewModel : IMainForumViewModel
    {
        private readonly IMainForumService _mainForumService;

        public MainForumViewModel(IMainForumService mainForumService)
        {
            _mainForumService = mainForumService;
        }
        public async Task<Response> CreateForum(MainForumModel mainForum) => await _mainForumService.CreateForum(mainForum);

        public async Task<Response> DeleteMainForum(long id) => await _mainForumService.DeleteForum(id);

        public async Task<Response> GetAllForum() => await _mainForumService.GetAllForum();

    }
}

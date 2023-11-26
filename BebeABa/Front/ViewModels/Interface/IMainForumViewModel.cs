using Shared.ApiUtilities;
using Shared.Models;
using System.Threading.Tasks;

namespace Front.ViewModels.Interface
{
    public interface IMainForumViewModel
    {
        Task<Response> CreateForum(MainForumModel mainForum);
        Task<Response> GetAllForum();
        Task<Response> DeleteMainForum(long id);
    }
}

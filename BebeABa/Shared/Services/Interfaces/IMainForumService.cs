using Shared.ApiUtilities;
using Shared.Models;
using System.Threading.Tasks;

namespace Shared.Services.Interfaces
{
    public interface IMainForumService
    {
        Task<Response> CreateForum(MainForumModel mainForum);
        Task<Response> GetAllForum();
    }
}

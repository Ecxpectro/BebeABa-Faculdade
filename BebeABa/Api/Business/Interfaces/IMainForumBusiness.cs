using Shared.ApiUtilities;
using Shared.Models;
using System.Threading.Tasks;

namespace Api.Business.Interfaces
{
    public interface IMainForumBusiness
    {
        Task<Response> CreateForum(MainForumModel mainForum);
        Task<Response> GetAllForum();
        Task<Response> Delete(long id);
    }
}

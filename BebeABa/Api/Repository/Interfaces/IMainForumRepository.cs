using DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Repository.Interfaces
{
    public interface IMainForumRepository
    {
        Task<MainForum> CreateForum(MainForum mainForum);
        Task<IEnumerable<MainForum>> GetAllForum();
        Task<MainForum> GetById(long id);
        Task<bool> DeleteForum(MainForum mainForum);
    }
}

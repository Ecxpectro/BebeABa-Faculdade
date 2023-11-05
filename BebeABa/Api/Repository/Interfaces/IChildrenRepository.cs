using DB.Models;
using System.Threading.Tasks;

namespace Api.Repository.Interfaces
{
    public interface IChildrenRepository
    {
        Task<bool> CreateChildren(Children children);
        public Task<Children> GetChildrenById(long childrenId);
    }
}

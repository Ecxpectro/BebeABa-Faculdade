using DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Repository.Interfaces
{
    public interface IChildrenRepository
    {
        Task<bool> CreateChildren(Children children);
        Task<Children> GetChildrenById(long childrenId);
        Task<List<Children>> GetChildrenByUserId(long userId);
    }
}

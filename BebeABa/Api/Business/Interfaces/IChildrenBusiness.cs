using Shared.ApiUtilities;
using Shared.Models;
using System.Threading.Tasks;

namespace Api.Business.Interfaces
{
    public interface IChildrenBusiness
    {
        Task<Response> CreateChildren(ChildrenModel children);
        Task<Response> GetChildrenById(long childrenId);
    }
}

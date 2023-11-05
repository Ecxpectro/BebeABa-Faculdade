using Shared.ApiUtilities;
using Shared.Models;
using System.Threading.Tasks;

namespace Front.ViewModels.Interface
{
    public interface IChildrenViewModel
    {
        Task<Response> CreateChildren(ChildrenModel children);
        Task<Response> GetChildrenById(long childrenId);
    }
}

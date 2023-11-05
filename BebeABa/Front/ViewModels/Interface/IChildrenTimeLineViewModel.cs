using Shared.ApiUtilities;
using Shared.Models;
using System.Threading.Tasks;

namespace Front.ViewModels.Interface
{
    public interface IChildrenTimeLineViewModel
    {
        Task<Response> Save(ChildrenTimeLineModel childrenTimeLine);
    }
}

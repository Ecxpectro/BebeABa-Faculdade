using Shared.ApiUtilities;
using Shared.FilterModels;
using Shared.Models;
using System.Threading.Tasks;

namespace Front.ViewModels.Interface
{
    public interface IChildrenTimeLineViewModel
    {
        Task<Response> Save(ChildrenTimeLineModel childrenTimeLine);
        Task<Response> GetChildrenTimeLineByFilters(ChildrenTimeLineFilterModel filters);

    }
}

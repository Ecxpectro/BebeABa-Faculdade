using Front.ViewModels.Interface;
using Shared.ApiUtilities;
using Shared.FilterModels;
using Shared.Models;
using Shared.Services.Interfaces;
using System.Threading.Tasks;

namespace Front.ViewModels
{
    public class ChildrenTimeLineViewModel : IChildrenTimeLineViewModel
    {
        private readonly IChildrenTimeLineService _childrenTimeLineService;

        public ChildrenTimeLineViewModel(IChildrenTimeLineService childrenTimeLineService)
        {
            _childrenTimeLineService = childrenTimeLineService;
        }

        public async Task<Response> GetChildrenTimeLineByFilters(ChildrenTimeLineFilterModel filters) => await _childrenTimeLineService.GetChildrenByFilters(filters);

        public async Task<Response> Save(ChildrenTimeLineModel childrenTimeLine) => await _childrenTimeLineService.Save(childrenTimeLine);

    }
}

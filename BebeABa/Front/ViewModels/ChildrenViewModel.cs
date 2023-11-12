using Front.ViewModels.Interface;
using Shared.ApiUtilities;
using Shared.Models;
using Shared.Services.Interfaces;
using System.Threading.Tasks;

namespace Front.ViewModels
{
    public class ChildrenViewModel : IChildrenViewModel
    {
        private readonly IChildrenService _childrenService;

        public ChildrenViewModel(IChildrenService childrenService)
        {
            _childrenService = childrenService;
        }
        public async Task<Response> CreateChildren(ChildrenModel children) => await _childrenService.CreateChildren(children);

        public async Task<Response> GetChildrenById(long childrenId) => await _childrenService.GetChildrenById(childrenId);

		public async Task<Response> GetChildrenByUserId(long userId) => await _childrenService.GetChildrenByUserId(userId);

	}
}

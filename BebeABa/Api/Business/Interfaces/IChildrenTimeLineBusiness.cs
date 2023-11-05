using Shared.ApiUtilities;
using Shared.Models;
using System.Threading.Tasks;

namespace Api.Business.Interfaces
{
    public interface IChildrenTimeLineBusiness
    {
        Task<Response> Save(ChildrenTimeLineModel childrenTimeLine);
    }
}

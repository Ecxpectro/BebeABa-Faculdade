using DB.Models;
using Shared.FilterModels;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.Interfaces
{
    public interface IChildrenTimeLineRepository
    {
        Task<bool> Save(ChildrenTimeLine childrenTimeLine);
        IQueryable<ChildrenTimeLine> GetByFilters(ChildrenTimeLineFilterModel filters);
    }
}

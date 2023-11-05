using DB.Models;
using System.Threading.Tasks;

namespace Api.Repository.Interfaces
{
    public interface IChildrenTimeLineRepository
    {
        Task<bool> Save(ChildrenTimeLine childrenTimeLine);
    }
}

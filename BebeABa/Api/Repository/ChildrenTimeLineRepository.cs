using Api.Repository.Interfaces;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using Shared.FilterModels;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository
{
    public class ChildrenTimeLineRepository : IChildrenTimeLineRepository
    {
        private readonly BebeaBaContext _context;

        public ChildrenTimeLineRepository(BebeaBaContext context)
        {
            _context = context;
        }

        public IQueryable<ChildrenTimeLine> GetByFilters(ChildrenTimeLineFilterModel filters)
        {
           var query = _context.ChildrenTimeLine.AsNoTracking();
            if (filters != null)
            {
                if (filters.ChildrenId.HasValue)
                {
                    query = query.Where(x => x.ChildrenId == filters.ChildrenId);
                }
            }
            return query;
        }

        public async Task<bool> Save(ChildrenTimeLine childrenTimeLine)
        {
            if (childrenTimeLine.ChildrenTimeLineId == 0)
            {
                await _context.ChildrenTimeLine.AddAsync(childrenTimeLine);
            }
            else
            {
                _context.ChildrenTimeLine.Update(childrenTimeLine);
            }
            
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

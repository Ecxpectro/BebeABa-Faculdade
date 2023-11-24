using Api.Repository.Interfaces;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository
{
    public class MainForumRepository : IMainForumRepository
    {
        private readonly BebeaBaContext _context;

        public MainForumRepository(BebeaBaContext context)
        {
            _context = context;
        }

        public async Task<MainForum> CreateForum(MainForum mainForum)
        {
            await _context.MainForum.AddAsync(mainForum);
            await _context.SaveChangesAsync();
            return mainForum;
        }

        public async Task<IEnumerable<MainForum>> GetAllForum() => await _context.MainForum.Include(x => x.User).Include(x => x.ForumRelation).ThenInclude(x => x.ForumAnswer).ThenInclude(x => x.User).OrderByDescending(x => x.MainForumId).ToListAsync();

    }
}

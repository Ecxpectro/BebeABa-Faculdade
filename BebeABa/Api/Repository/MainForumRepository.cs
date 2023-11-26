using Api.Repository.Interfaces;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System;
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

        public async Task<bool> DeleteForum(MainForum mainForum)
        {
            bool isOk = false;
            try
            {
                if (mainForum is not null)
                {
                    await DeleteRelationById(mainForum.MainForumId);
                    _context.MainForum.Remove(mainForum);
                    isOk = true;
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception)
            {
            }
            return isOk;
        }

        public async Task DeleteRelationById(long relationId)
        {
           var listOfRelation =  await _context.ForumRelation
        .Where(x => x.MainForumId == relationId)
        .ToListAsync();
            
            _context.ForumRelation.RemoveRange(listOfRelation);

            await _context.SaveChangesAsync();
        }

        public async Task<MainForum> GetById(long id) => await _context.MainForum.FirstOrDefaultAsync(x => x.MainForumId == id);
    }
}

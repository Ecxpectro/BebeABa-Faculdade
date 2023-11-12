using Api.Repository.Interfaces;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository
{
	public class ChildrenRepository : IChildrenRepository
    {
        private readonly BebeaBaContext _context;

        public ChildrenRepository(BebeaBaContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateChildren(Children children)
        {
            _context.Add(children);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Children> GetChildrenById(long childrenId) => await _context.Children.FirstOrDefaultAsync(x => x.ChildrenId == childrenId);

		public async Task<List<Children>> GetChildrenByUserId(long userId) => await _context.Children.Where(x => x.UserId == userId).ToListAsync();

	}
}

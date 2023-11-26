using Api.Repository.Interfaces;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BebeaBaContext _context;

        public UserRepository(BebeaBaContext context)
        {
            _context = context;
        }
        public async Task<Users> CreateUser(Users user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Users> Login(Users user) =>
            await _context
                .Users
                .Include(x => x.Children)
                .FirstOrDefaultAsync(u =>
                    (u.UserEmail == user.UserEmail) &&
                    u.UserPassword == user.UserPassword);
    }
}

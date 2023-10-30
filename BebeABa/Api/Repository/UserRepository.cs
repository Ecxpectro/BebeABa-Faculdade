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
        public async Task<bool> CreateUser(Users user)
        {
            _context.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Users> Login(Users user) =>
            await _context
                .Users
                .FirstOrDefaultAsync(u =>
                    (u.UserEmail == user.UserEmail) &&
                    u.UserPassword == user.UserPassword);
    }
}

using Api.Repository.Interfaces;
using DB.Models;
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
    }
}

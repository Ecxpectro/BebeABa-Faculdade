using DB.Models;
using System.Threading.Tasks;

namespace Api.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<Users> CreateUser(Users user);
        Task<Users> Login(Users user);
    }
}

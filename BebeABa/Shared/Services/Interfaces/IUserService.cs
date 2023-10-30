using Shared.Models;
using Shared.ApiUtilities;
using System.Threading.Tasks;

namespace Shared.Services.Interfaces
{
    public interface IUserService
    {
        Task<Response> CreateUser(UserModel user);
        Task<Response> Login(UserModel user);
    }
}

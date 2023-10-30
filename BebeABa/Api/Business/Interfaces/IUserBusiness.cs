using Shared.Models;
using Shared.ApiUtilities;
using System.Threading.Tasks;

namespace Api.Business.Interfaces
{
    public interface IUserBusiness
    {
        Task<Response> CreateUser(UserModel user);
        Task<Response> Login(UserModel user);
    }
}

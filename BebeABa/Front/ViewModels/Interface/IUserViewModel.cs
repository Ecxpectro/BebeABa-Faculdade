using Shared.Models;
using Shared.ApiUtilities;
using System.Threading.Tasks;

namespace Front.ViewModels.Interface
{
    public interface IUserViewModel
    {
        Task<Response> CreateUser(UserModel user);
    }
}

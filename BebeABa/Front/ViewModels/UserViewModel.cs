using Front.ViewModels.Interface;
using Shared.Models;
using Shared.Services.Interfaces;
using Shared.ApiUtilities;
using System.Threading.Tasks;

namespace Front.ViewModels
{
    public class UserViewModel : IUserViewModel
    {
        private readonly IUserService _userService;

        public UserViewModel(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Response> CreateUser(UserModel user) => await _userService.CreateUser(user);
    }
}

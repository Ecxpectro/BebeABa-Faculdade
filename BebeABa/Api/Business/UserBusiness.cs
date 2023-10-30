using Api.Business.Interfaces;
using Api.Repository.Interfaces;
using AutoMapper;
using DB.Models;
using Shared.Models;
using Shared.ApiUtilities;
using System;
using System.Threading.Tasks;
using Shared.Enums;

namespace Api.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserBusiness(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Response> CreateUser(UserModel user)
        {
            var response = new Response();

            try
            {
                response.Result = await _userRepository.CreateUser(_mapper.Map<Users>(user));
                response.Status = Convert.ToBoolean(response.Result) ? StatusCode.Success : StatusCode.NotFound;
                response.Message = Convert.ToBoolean(response.Result) ? string.Empty : "Could not add data.";
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.ServerError;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response> Login(UserModel user)
        {
            var response = new Response();

            try
            {
                var userLogged = await _userRepository.Login(_mapper.Map<Users>(user));

                if (userLogged != null)
                {
                    response.Status = StatusCode.Success;
                    response.Result = _mapper.Map<UserModel>(userLogged);
                }
                else
                {
                    response.Status = StatusCode.NotFound;
                    response.Message = "Not Found.";
                }
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.ServerError;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}

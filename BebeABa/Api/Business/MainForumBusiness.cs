using Api.Business.Interfaces;
using Api.Repository.Interfaces;
using AutoMapper;
using DB.Models;
using Shared.ApiUtilities;
using Shared.Enums;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Business
{
    public class MainForumBusiness : IMainForumBusiness
    {
        private readonly IMainForumRepository _mainForumRepository;
        private readonly IMapper _mapper;

        public MainForumBusiness(IMainForumRepository mainForumRepository, IMapper mapper)
        {
            _mainForumRepository = mainForumRepository;
            _mapper = mapper;
        }
        public async Task<Response> CreateForum(MainForumModel mainForum)
        {
            var response = new Response();

            try
            {
                response.Result = await _mainForumRepository.CreateForum(_mapper.Map<MainForum>(mainForum));
                response.Status = response.Result != null ? StatusCode.Success : StatusCode.NotFound;
                response.Message = response.Result != null ? string.Empty : "Could not add data.";
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.ServerError;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response> Delete(long id)
        {
            var response = new Response();

            try
            {
                var mainForum = await _mainForumRepository.GetById(id);
                if (mainForum is not null)
                {
                    response.Result = await _mainForumRepository.DeleteForum(mainForum);
                    response.Status = response.Result is not null ? StatusCode.Success : StatusCode.BadRequest;
                    response.Message = response.Result is not null ? string.Empty : "Not saved or update.";
                }
                else
                {
                    response.Status = StatusCode.NotFound;
                    response.Message = "Not found record.";
                }
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.ServerError;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response> GetAllForum()
        {
            var response = new Response();

            try
            {
                var forums = await _mainForumRepository.GetAllForum();

                if (forums != null)
                {
                    response.Status = StatusCode.Success;
                    response.Result = _mapper.Map<List<MainForumModel>>(forums);
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

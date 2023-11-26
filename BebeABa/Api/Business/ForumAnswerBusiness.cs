using Api.Business.Interfaces;
using DB.Models;
using Shared.ApiUtilities;
using Shared.Enums;
using Shared.Models;
using System.Threading.Tasks;
using System;
using AutoMapper;
using Api.Repository.Interfaces;

namespace Api.Business
{
    public class ForumAnswerBusiness : IForumAnswerBusiness
    {
        private readonly IMapper _mapper;
        private readonly IForumAnswerRepository _forumAnswerRepository;

        public ForumAnswerBusiness(IMapper mapper, IForumAnswerRepository forumAnswerRepository)
        {
            _mapper = mapper;
            _forumAnswerRepository = forumAnswerRepository;
        }
        public async Task<Response> CreateAnswer(ForumAnswerModel forumAnswer)
        {
            var response = new Response();

            try
            {
                response.Result = await _forumAnswerRepository.CreateAnswer(_mapper.Map<ForumAnswer>(forumAnswer), forumAnswer.MainForumId);
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
                var answer = await _forumAnswerRepository.GetById(id);
                if (answer is not null)
                {
                    response.Result = await _forumAnswerRepository.DeleteAnswer(answer);
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
    }
}

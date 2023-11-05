using Api.Business.Interfaces;
using Api.Repository.Interfaces;
using AutoMapper;
using DB.Models;
using Shared.ApiUtilities;
using Shared.Enums;
using Shared.Models;
using System;
using System.Threading.Tasks;

namespace Api.Business
{
    public class ChildrenBusiness : IChildrenBusiness
    {
        private readonly IChildrenRepository _childrenRepository;
        private readonly IMapper _mapper;

        public ChildrenBusiness(IChildrenRepository childrenRepository, IMapper mapper)
        {
            _childrenRepository = childrenRepository;
            _mapper = mapper;
        }
        public async Task<Response> CreateChildren(ChildrenModel children)
        {
            var response = new Response();

            try
            {
                response.Result = await _childrenRepository.CreateChildren(_mapper.Map<Children>(children));
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

        public async Task<Response> GetChildrenById(long childrenId)
        {
            Response response = new Response();
            try
            {
                var children = await _childrenRepository.GetChildrenById(childrenId);
                if (children is not null)
                {
                    response.Status = StatusCode.Success;
                    response.Result = _mapper.Map<ChildrenModel>(children);
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

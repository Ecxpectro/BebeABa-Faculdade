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
    public class ChildrenTimeLineBusiness : IChildrenTimeLineBusiness
    {
        private readonly IMapper _mapper;
        private readonly IChildrenTimeLineRepository _childrenTimeLineRepository;

        public ChildrenTimeLineBusiness(IMapper mapper, IChildrenTimeLineRepository childrenTimeLineRepository)
        {
            _mapper = mapper;
            _childrenTimeLineRepository = childrenTimeLineRepository;
        }
        public async Task<Response> Save(ChildrenTimeLineModel childrenTimeLine)
        {
            var response = new Response();

            try
            {
                response.Result = await _childrenTimeLineRepository.Save(_mapper.Map<ChildrenTimeLine>(childrenTimeLine));
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
    }
}

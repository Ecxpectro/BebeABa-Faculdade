using Api.Business.Interfaces;
using Api.Repository.Interfaces;
using AutoMapper;
using DB.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shared.ApiUtilities;
using Shared.Enums;
using Shared.FilterModels;
using Shared.Helpers;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Response> GetByFilters(ChildrenTimeLineFilterModel filters)
        {
            var response = new Response();

            try
            {
                var query = _childrenTimeLineRepository.GetByFilters(filters);
                if (query.Any())
                {
                    if (filters.GetAllList)
                    {
                        response.Result = _mapper.Map<List<ChildrenTimeLineModel>>(await query.ToListAsync());
                    }
                    else
                    {
                        response.Result = new FilterResultModel<ChildrenTimeLineModel>
                        {
                            List = _mapper.Map<List<ChildrenTimeLineModel>>(
                                await query
                                .Skip(FunctionsHelper.SkipRows(filters.Page, filters.PageSize))
                            .Take(filters.PageSize)
                                .ToListAsync()
                            ),
                            Pager = new PagerModel(query.Count(), filters.Page, filters.PageSize)
                        };
                    }

                    response.Status = StatusCode.Success;

                }
                else
                {
                    response.Status = StatusCode.NotFound;
                    response.Message = "Not found records.";
                }

            }
            catch (Exception ex)
            {
                response.Status = StatusCode.ServerError;
                response.Message = ex.Message;
            }

            return response;
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

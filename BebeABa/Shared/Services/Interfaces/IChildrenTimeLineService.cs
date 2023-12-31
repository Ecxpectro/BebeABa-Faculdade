﻿using Shared.ApiUtilities;
using Shared.FilterModels;
using Shared.Models;
using System.Threading.Tasks;

namespace Shared.Services.Interfaces
{
    public interface IChildrenTimeLineService
    {
        Task<Response> Save(ChildrenTimeLineModel childrenTimeLine);
        Task<Response> GetChildrenByFilters(ChildrenTimeLineFilterModel filters);
    }
}

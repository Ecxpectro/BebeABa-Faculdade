﻿using Shared.ApiUtilities;
using Shared.Models;
using System.Threading.Tasks;

namespace Shared.Services.Interfaces
{
    public interface IChildrenService
    {
        Task<Response> CreateChildren(ChildrenModel children);
        Task<Response> GetChildrenById(long childrenId);
        Task<Response> GetChildrenByUserId(long userId);
    }
}

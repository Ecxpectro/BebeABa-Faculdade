using Api.Business;
using Api.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.FilterModels;
using Shared.Models;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/BebeABa")]
    [ApiController]
    public class ChildrenTimeLineController : ControllerBase
    {
        private readonly IChildrenTimeLineBusiness _childrenTimeLineBusiness;

        public ChildrenTimeLineController(IChildrenTimeLineBusiness childrenTimeLineBusiness)
        {
            _childrenTimeLineBusiness = childrenTimeLineBusiness;
        }
        [HttpPost("ChildrenTimeLine")]
        public async Task<IActionResult> Save(ChildrenTimeLineModel childrenTimeLine) => Ok(await _childrenTimeLineBusiness.Save(childrenTimeLine));

        [HttpPost("ChildrenTimeLine/GetByFilters")]
        public async Task<IActionResult> GetByFilters(ChildrenTimeLineFilterModel filters) => Ok(await _childrenTimeLineBusiness.GetByFilters(filters));
    }
}

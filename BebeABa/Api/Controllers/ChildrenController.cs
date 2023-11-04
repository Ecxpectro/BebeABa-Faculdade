using Api.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using System.Threading.Tasks;


namespace Api.Controllers
{
    [Route("api/BebeABa")]
    [ApiController]
    public class ChildrenController : ControllerBase
    {
        private readonly IChildrenBusiness _childrenBusiness;

        public ChildrenController(IChildrenBusiness childrenBusiness)
        {
            _childrenBusiness = childrenBusiness;
        }

        [HttpPost("Children")]
        public async Task<IActionResult> CreateChildren(ChildrenModel children) => Ok(await _childrenBusiness.CreateChildren(children));
    }
}

using Front.ViewModels.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared;
using Shared.ApiUtilities;
using Shared.FilterModels;
using Shared.Helpers;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Front.Controllers
{
    public class ChildrenController : Controller
    {
        private readonly IChildrenViewModel _childrenViewModel;
        private readonly IConfiguration _configuration;
        private readonly IChildrenTimeLineViewModel _childrenTimeLineViewModel;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChildrenController(
            IChildrenViewModel childrenViewModel,
            IConfiguration configuration,
            IChildrenTimeLineViewModel childrenTimeLineViewModel,
            IHttpContextAccessor httpContextAccessor)
        {
            _childrenViewModel = childrenViewModel;
            _configuration = configuration;
            _childrenTimeLineViewModel = childrenTimeLineViewModel;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        [Route("Children/Index/{childrenId:long}")]
        public IActionResult Index(long childrenId)
        {
            return View(childrenId);
        }

        public IActionResult RegisterChild()
        {
            return View();
        }

        public async Task<IActionResult> ChildrenProfile()
        {
            UserModel user = null;
            List<ChildrenModel> childrens = new List<ChildrenModel>(0);
            var cookieValue = _httpContextAccessor.HttpContext?.Request.Cookies["userLoggedBebeABa"];
            if (cookieValue != null)
            { user = JsonConvert.DeserializeObject<UserModel>(FunctionsHelper.Decrypt(cookieValue)); }
            if (user != null)
            {
                var response = await _childrenViewModel.GetChildrenByUserId(user.UserId);
                childrens = JsonConvert.DeserializeObject<List<ChildrenModel>>(response.Result.ToString());
            }

            return View(childrens);
        }

        public async Task<IActionResult> Save(IFormCollection formCollection)
        {
            var isOk = false;
            string msg = string.Empty;
            Response response = new Response();
            try
            {
                var files = formCollection.Files;
                var children = JsonConvert.DeserializeObject<ChildrenModel>(formCollection["ChildrenJson"]);
                if (children != null)
                {
                    if (files.Any() && files[0] is { Length: > 0 })
                    {
                        var extension = files[0].FileName.Split('.').Last();
                        if (extension == "jpeg" || extension == "jpg" || extension == "png")
                        {

                            var fileName = files[0].FileName;
                            var path = $"{Constants.ChildImagesPath}/{FunctionsHelper.GenerateNumbersRandom(0, 999999)}{fileName}";

                            using (var fs = System.IO.File.Create(path))
                            { await files[0].CopyToAsync(fs); }
                            children.ImgPath = fileName;
                        }
                        else
                        {
                            msg = "Invalid format for the image";
                        }
                    }
                    response = await _childrenViewModel.CreateChildren(children);
                    if (response.Status == Shared.Enums.StatusCode.Success)
                    {
                        isOk = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }


            return Json(new { success = isOk, message = msg });
        }

        public async Task<IActionResult> GridTimeLine(long childrenId)
        {
            string draw = string.Empty;
            int recordsTotal = 0;
            var data = new List<ChildrenTimeLineModel>(0);

            try
            {
                draw = Request.Form["draw"].FirstOrDefault();
                // Skip number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                var filters = new ChildrenTimeLineFilterModel
                {
                    Page = ((start != null ? Convert.ToInt32(start) : 0) / (length != null ? Convert.ToInt32(length) : 10)) + 1,
                    PageSize = length != null ? Convert.ToInt32(length) : 10,
                    ChildrenId = childrenId
                };
                if (childrenId > 0)
                {
                    var response = await _childrenTimeLineViewModel.GetChildrenTimeLineByFilters(filters);
                    if (response is not null && response.Status == Shared.Enums.StatusCode.Success)
                    {
                        var result = JsonConvert.DeserializeObject<FilterResultModel<ChildrenTimeLineModel>>(response.Result.ToString());
                        recordsTotal = result.Pager.TotalItems;
                        data = result.List;
                    }
                }

            }
            catch (Exception ex)
            { }

            return Json(new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = data
            });
        }


        public async Task<IActionResult> SaveTimeLine(IFormCollection formCollection)
        {
            var isOk = false;
            string msg = string.Empty;
            Response response = new Response();
            try
            {
                var files = formCollection.Files;
                var childrenTimeLine = JsonConvert.DeserializeObject<ChildrenTimeLineModel>(formCollection["ChildrenTimeLineJson"]);
                if (childrenTimeLine != null)
                {

                    var childResponse = await _childrenViewModel.GetChildrenById(childrenTimeLine.ChildrenId);
                    var child = JsonConvert.DeserializeObject<ChildrenModel>(childResponse.Result.ToString());
                    if (child != null)
                    {
                        int childAge = childrenTimeLine.TimeLineDate.Year - child.BirthDate.Year;
                        childrenTimeLine.ChildAge = childAge;
                    }

                    if (files.Any() && files[0] is { Length: > 0 })
                    {

                        var inputFileName = files[0].FileName;
                        string fileName = $"{FunctionsHelper.GenerateNumbersRandom(0, 999999)}{inputFileName}";
                        var path = $"{Constants.ChildrenTimeLineImagesPath}/{fileName}";

                        using (var fs = System.IO.File.Create(path))
                        { await files[0].CopyToAsync(fs); }
                        childrenTimeLine.FilePath = fileName;
                    }
                    response = await _childrenTimeLineViewModel.Save(childrenTimeLine);
                    if (response.Status == Shared.Enums.StatusCode.Success)
                    {
                        isOk = true;
                    }
                }

            }
            catch (Exception ex)
            {
            }


            return Json(new { success = isOk, message = msg });
        }

        public IActionResult DownloadFile(string fileName)
        {
            var filePath = Path.Combine(Constants.ChildrenTimeLineImagesPath, fileName);

            if (System.IO.File.Exists(filePath))
            {
                var fileContent = System.IO.File.ReadAllBytes(filePath);

                var contentType = "application/octet-stream";

                return File(fileContent, contentType, fileName);
            }
            return NotFound();
        }
    }
}

using Front.ViewModels.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared;
using Shared.ApiUtilities;
using Shared.Helpers;
using Shared.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Front.Controllers
{
    public class ChildrenController : Controller
    {
        private readonly IChildrenViewModel _childrenViewModel;
        private readonly IConfiguration _configuration;
        private readonly IChildrenTimeLineViewModel _childrenTimeLineViewModel;

        public ChildrenController(
            IChildrenViewModel childrenViewModel,
            IConfiguration configuration,
            IChildrenTimeLineViewModel childrenTimeLineViewModel)
        {
            _childrenViewModel = childrenViewModel;
            _configuration = configuration;
            _childrenTimeLineViewModel = childrenTimeLineViewModel;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterChild()
        {
            return View();
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
                if(children != null)
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
                    if (child != null){
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
    }
}

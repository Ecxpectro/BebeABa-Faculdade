using Front.ViewModels.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared;
using Shared.ApiUtilities;
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

        public ChildrenController(IChildrenViewModel childrenViewModel, IConfiguration configuration)
        {
            _childrenViewModel = childrenViewModel;
            _configuration = configuration;
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
                            var path = $"{Constants.ChildImagesPath}/{fileName}";

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
    }
}

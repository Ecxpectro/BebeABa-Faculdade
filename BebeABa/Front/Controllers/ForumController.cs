using Front.ViewModels.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.ApiUtilities;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Front.Controllers
{
    public class ForumController : Controller
    {
        private readonly IMainForumViewModel _mainForumViewModel;
        private readonly IForumAnswerViewModel _forumAnswerViewModel;

        public ForumController(IMainForumViewModel mainForumViewModel, IForumAnswerViewModel forumAnswerViewModel)
        {
            _mainForumViewModel = mainForumViewModel;
            _forumAnswerViewModel = forumAnswerViewModel;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _mainForumViewModel.GetAllForum();

            var forum = JsonConvert.DeserializeObject<List<MainForumModel>>(response.Result.ToString());
            return View(forum);
        }

        public async Task<IActionResult> CreatePost(MainForumModel mainForum)
        {
            var isOk = false;
            string msg = string.Empty;
            Response response;
            MainForumModel model = new MainForumModel();

            try
            {

                if (mainForum != null)
                {
                    response = await _mainForumViewModel.CreateForum(mainForum);
                    model = JsonConvert.DeserializeObject<MainForumModel>(response.Result.ToString());
                    if (response.Status == Shared.Enums.StatusCode.Success)
                    {
                        isOk = true;
                    }
                }
            }
            catch (Exception ex)
            { }

            return Json(new {data = model, success = isOk, message = msg });
        }
        public async Task<IActionResult> CreateAnswer(ForumAnswerModel forumAnswer)
        {
            var isOk = false;
            string msg = string.Empty;
            Response response;
            ForumAnswerModel model = new ForumAnswerModel();

            try
            {

                if (forumAnswer != null)
                {
                    response = await _forumAnswerViewModel.CreateAnswer(forumAnswer);
                    model = JsonConvert.DeserializeObject<ForumAnswerModel>(response.Result.ToString());
                    if (response.Status == Shared.Enums.StatusCode.Success)
                    {
                        isOk = true;
                    }
                }
            }
            catch (Exception ex)
            { }

            return Json(new { data = model, success = isOk, message = msg });
        }
    }
}

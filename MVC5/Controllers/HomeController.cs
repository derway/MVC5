using MVC5.Models;
using MVC5.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MVC5.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _userService = new UserService();
        public ActionResult Session_Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 验证用户身份
                var user = _userService.Authenticate(model.Username, model.Password);
                if (user != null)
                {
                    // 将用户信息存入Session
                    Session["UserId"] = user.Id;
                    Session["UserName"] = user.Username;
                    Session["UserRole"] = user.Role;

                    return RedirectToAction("Session_Dashboard");
                }
            }
            return View(model);
        }

        public ActionResult Session_Dashboard()
        {
            // 检查用户是否已登录
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Session_Login");
            }

            // 使用Session中的数据
            ViewBag.UserName = Session["UserName"];
            ViewBag.UserRole = Session["UserRole"];

            return View();
        }

        public ActionResult Session_Logout()
        {
            // 清除Session
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Session_Login");
        }
        [HttpGet]
        public ActionResult EditProfile()
        {
            // 模擬取得使用者資料
            var model = new UserProfileViewModel
            {
                Name = "Derway",
                Email = "derway@example.com"
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditProfile(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 模擬儲存資料成功
                TempData["SuccessMessage"] = "您的資料已成功更新！";
                return RedirectToAction("ProfileSummary");
            }

            return View(model);
        }

        public ActionResult ProfileSummary()
        {
            ViewBag.Message = TempData["SuccessMessage"];
            return View();
        }

        public ActionResult Tempdata_Dashboard()
        {
            ViewBag.UserName = TempData["UserName"];
            return View();
        }
        [HttpGet]
        public ActionResult Tempdata_Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Tempdata_Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 登录成功，保存消息并重定向
                TempData["SuccessMessage"] = "登录成功！";
                TempData["UserName"] = model.Username;
                return RedirectToAction("Tempdata_Dashboard");
            }

            // 登录失败，返回登录页
            TempData["ErrorMessage"] = "用户名或密码错误";
            return View(model);
        }
        public ActionResult Tempdata_B()
        {
            ViewBag.Message = TempData["Tempdata"]; // 資料只保留一次
            var msg = TempData.Peek("Peekmsg"); // 讀取但不移除
            TempData.Keep("Keepmsg"); // 保留資料到下一次請求
            return View();
        }
        public ActionResult Tempdata_A()
        {
            TempData["Tempdata"] = "資料只會保留到下一次請求結束（例如 Redirect），之後就會被清除。";
            TempData["Keepmsg"] = "保留資料到下一次請求";
            TempData["Peekmsg"] = "保留資料到下一次請求 ";
            return RedirectToAction("Tempdata_B");
        }
        public ActionResult ViewData2()
        {
            // 使用ViewData传递数据
            ViewData["PageTitle"] = "首页";
            ViewData["UserList"] = new List<string> { "张三", "李四", "王五" };
            ViewData["ProductCount"] = 42;

            return View();
        }
        public ActionResult ViewBag2()
        {
            // 使用ViewBag傳遞資料
            ViewBag.PageTitle = "首頁";
            ViewBag.UserCount = 150;
            ViewBag.CurrentDate = DateTime.Now;
            ViewBag.IsAdmin = true;

            return View();
        }
        public ActionResult AspNet()
        {
            // 對應 URL：~/ Home / AspNet
            // 回傳 Views / Home / AspNet.cshtml 頁面。
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            // ViewBag, ViewData，可以在 Controller 與 View 之間傳遞資料。
            // 兩者都只在 Controller → View 的單次請求中有效。
            // 若需要跨 Action 或跨頁面傳遞資料，使用 TempData 或 Session
            // 若希望有型別安全與 IntelliSense 支援，使用「強型別 ViewModel」

            // 如果定義 ViewBag，可在 View 使用 ViewData
            ViewBag.Data2 = "data2";
            
            // 如果定義 ViewData，可在 View 使用 ViewBag
            ViewData["Data1"] = "data1";

            // ViewBag 是 ViewData 的「動態封裝」，它們底層是共用資料來源的。
            ViewData["Name"] = "My Name";
            ViewBag.Name = ViewData["Name"];

            // ViewBag 是一種動態物件 dynamic
            ViewBag.Msg2 = "Msg2 from ViewBag!";

            // ViewData 是一種字典型別 Dictionary<string, object> 
            ViewData["Msg1"] = "Msg1 from ViewData!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

}
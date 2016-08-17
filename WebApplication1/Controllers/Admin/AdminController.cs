using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers.Admin
{
    public class AdminController : BaseAdminController
    {
        // GET: Admin
        public ActionResult Index()
        {
            ViewData["MEMBER_AMOUNT"] = 1;
            ViewData["ORDER_COMPLETED_AMOUNT"] = 2;
            ViewData["ORDER_AMOUNT"] = 3;
            ViewData["NEWS_CATEGORY_AMOUNT"] = 4;
            ViewData["NEWS_AMOUNT"] = 5;
            ViewData["ITEM_AMOUNT"] = 6;
            ViewData["ITEM_CATEGORY_AMOUNT"] = 7;


            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var username = form["username"];
            var password = form["password"];
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password) &&
                DataHelper.AccountHelper.getInstance().loginAdmin(data, username, password))
            {
                //TODO, save session here
                Session[Constants.KEY_SESSION_ADMIN_USERNAME] = username;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Vui lòng kiểm tra tên truy cập hoặc mật khẩu.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            DataHelper.AccountHelper.getInstance().logoutAdmin(this);
            return RedirectToAction("Index");
        }
    }
}
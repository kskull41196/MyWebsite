﻿using System;
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
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var username = form["username"];
            var password = form["password"];
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password) &&
                DataHelper.getInstance().loginAdmin(data, username, password))
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
    }
}
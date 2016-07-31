using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(string id,string id2)
        {
            ViewData["ListModulesHome"] = getHomeModule();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        private List<tbl_module> getHomeModule()
        {
            var result = data.tbl_modules.Where(a => a.type == 2).OrderByDescending(a => a.date_added);
            return result.ToList();
        }
    }
}
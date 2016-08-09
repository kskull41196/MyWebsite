using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers.Admin
{
    public class ShopConfigController : BaseAdminController
    {
        // GET: ShopConfig
        public ActionResult Index()
        {
            return View(URLHelper.URL_ADMIN_CONFIGSHOP);
        }
        [HttpGet]
        public ActionResult Index1()
        {
            return View(URLHelper.URL_ADMIN_CONFIGSHOP);
        }
    }
}
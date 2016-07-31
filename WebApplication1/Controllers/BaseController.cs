using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public abstract class BaseController : Controller
    {
        public DataClassesDataContext data = new DataClassesDataContext();
      
        public BaseController()
        {
            ViewData["ListModules"] = getAllSupportedModules();
        }

        private List<tbl_module> getAllSupportedModules()
        {
            var result = data.tbl_modules.Where(a => a.type == 1).OrderByDescending(a => a.date_added);
            return result.ToList();
        }
    }
}
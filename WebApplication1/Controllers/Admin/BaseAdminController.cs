using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public abstract class BaseAdminController : Controller
    {
        public DataClassesDataContext data = new DataClassesDataContext();
      
        public BaseAdminController()
        {
            //TODO, do anything we need all controllers to inherit.
        }
    }
}
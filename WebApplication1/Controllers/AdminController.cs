using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        DataClassesDataContext data = new DataClassesDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ItemCategory(String act, String id)
        {
            ActionTypeHelper.ActionType type = ActionTypeHelper.getTypeFromString(act);
            ViewBag.Alert = ActionTypeHelper.getStringFromType(type);
            switch (type)
            {
                case ActionTypeHelper.ActionType.TYPE_CREATE:
                    break;
                case ActionTypeHelper.ActionType.TYPE_EDIT:
                    break;
                case ActionTypeHelper.ActionType.TYPE_DEL:
                    break;
                case ActionTypeHelper.ActionType.TYPE_VIEW:
                    return View("ItemCategory_m", data);
                //return View(All_Loaitin);
            }
            return View();
        }
        public ActionResult ItemCategory_m(DataClassesDataContext data) // edit create
        {
            var All_Loaitin = from tt in data.tbl_item_categories select tt;
            return View();
        }
    }
}
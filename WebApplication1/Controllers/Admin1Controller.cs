using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class Admin1Controller : Controller
    {
        DataClassesDataContext data = new DataClassesDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        private List<tbl_item_category> getItemCategory(int count)
        {
            return data.tbl_item_categories.OrderByDescending(a => a.date_added).Take(count).ToList();
        }
        private tbl_item_category getOneItemCategory(int id)
        {
            var itemCategory = data.tbl_item_categories.First(i => i.id == id);
            return itemCategory;
        }
        public ActionResult ItemCategory(String act, String id)
        {
            ActionTypeHelper.ActionType type = ActionTypeHelper.getTypeFromString(act);
            ViewBag.Alert = ActionTypeHelper.getStringFromType(type);
            switch (type)
            {
                case ActionTypeHelper.ActionType.TYPE_CREATE:
                    return View("ItemCategory_m",new List<tbl_item_category>());
                case ActionTypeHelper.ActionType.TYPE_EDIT:
                    if (id == null || id.Equals("")) return View("ItemCategory_m", new List<tbl_item_category>());
                    return View("ItemCategory_m",getOneItemCategory(Int32.Parse(id)));
                case ActionTypeHelper.ActionType.TYPE_DEL:
                    break;
                case ActionTypeHelper.ActionType.TYPE_VIEW:
                    var listItemCategory = getItemCategory(10);
                    return View("ItemCategory",listItemCategory);
            }
            return View();
        }
    }
}
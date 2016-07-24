using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Admin
{
    public class ItemCategoryController : Controller
    {
        DataClassesDataContext data = new DataClassesDataContext();
        // GET: ItemCategory
        private List<tbl_item_category> getItemCategory(int count)
        {
            return data.tbl_item_categories.OrderByDescending(a => a.date_added).Take(count).ToList();
        }
        private tbl_item_category getOneItemCategory(int id)
        {
            var itemCategory = from ic in data.tbl_item_categories
                               where ic.id == id
                               select ic;
            //var itemCategory = data.tbl_item_categories.First(i => i.id == id);
            if(itemCategory==null)
            {
                return new tbl_item_category();
            }
            return itemCategory.Single();
        }
        public ActionResult Index()
        {
            return itemCategoryView();
        }
        public ActionResult itemCategoryView()
        {
            var listItemCategory = getItemCategory(10);
            return View(URLHelper.URL_ADMIN_ITEM_CATEGORY, listItemCategory);
        }
        public ActionResult itemCategoryCreate()
        {
            return View(URLHelper.URL_ADMIN_ITEM_CATEGORY_M,new tbl_item_category());
        }
        public ActionResult itemCategoryEdit(String id)
        {
            return View(URLHelper.URL_ADMIN_ITEM_CATEGORY_M, getOneItemCategory(Int32.Parse(id)));
        }
        public ActionResult itemCategoryDelete(String id)
        {
            return View();
        }
    }
}
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
        [HttpGet]
        public ActionResult itemCategoryCreate()
        {
            return View(URLHelper.URL_ADMIN_ITEM_CATEGORY_M,new tbl_item_category());
        }
        [HttpPost]
        public ActionResult itemCategoryCreate(FormCollection form)
        {
            tbl_item_category tic = new tbl_item_category();
            var name = form["name"];
            var sort = form["sort"];
            var title = form["title"];
            var description = form["description"];
            var keyword = form["keyword"];
            bool err = false;
            if (String.IsNullOrEmpty(name))
            {
                err = true;
                ViewData["Error"] += "Vui lòng nhập tên danh mục!\n";
            }
            tic.name = name;
            if (!String.IsNullOrEmpty(sort))
                tic.sort = Int32.Parse(sort);
            else
                tic.sort = 0;
            tic.title = title;
            tic.description = description;
            tic.keyword = keyword;
            tic.status = 1;
            tic.date_added = DateTime.Now;
            tic.last_modified = DateTime.Now;
            if (err == false)
            {
                data.tbl_item_categories.InsertOnSubmit(tic);
                data.SubmitChanges();
                return RedirectToAction("itemCategoryView");
            }
            else
            {
                return View(URLHelper.URL_ADMIN_ITEM_CATEGORY_M, tic);
            }
        }
        [HttpGet]
        public ActionResult itemCategoryEdit(String id)
        {
            return View(URLHelper.URL_ADMIN_ITEM_CATEGORY_M, getOneItemCategory(Int32.Parse(id)));
        }
        [HttpPost]
        public ActionResult itemCategoryEdit(FormCollection form)
        {
            var id = form["id"];
            if (id == null)
            {
                return itemCategoryCreate(form);
            }
            else
            {
                tbl_item_category tic = getOneItemCategory(Int32.Parse(id));
                var name = form["name"];
                var sort = form["sort"];
                var title = form["title"];
                var description = form["description"];
                var keyword = form["keyword"];
                bool err = false;
                if (String.IsNullOrEmpty(name))
                {
                    err = true;
                    ViewData["Error"] += "Vui lòng nhập tên danh mục!\n";
                }
                tic.name = name;
                if (!String.IsNullOrEmpty(sort))
                    tic.sort = Int32.Parse(sort);
                else
                    tic.sort = 0;
                tic.title = title;
                tic.description = description;
                tic.keyword = keyword;
                tic.status = 1;
                tic.date_added = DateTime.Now;
                tic.last_modified = DateTime.Now;
                if (err == false)
                {
                    UpdateModel(tic);
                    data.SubmitChanges();
                    return RedirectToAction("itemCategoryView");
                }
                else
                {
                    return View(URLHelper.URL_ADMIN_ITEM_CATEGORY_M, tic);
                }
            }
        }
        public ActionResult itemCategoryDelete(String id)
        {
            return View();
        }
    }
}
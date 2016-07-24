using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;

namespace WebApplication1.Controllers.Admin
{
    public class ItemCategoryController : Controller
    {
        DataClassesDataContext data = new DataClassesDataContext();
        // GET: ItemCategory
        private List<tbl_item_category> getItemCategory(int count)
        {
            return getItemCategory(count, "");
        }
        private List<tbl_item_category> getAllItemCategory()
        {
            return getItemCategory(-1, "");
        }
        private List<tbl_item_category> getItemCategory(int count,String keyword)
        {
            var result= data.tbl_item_categories.OrderByDescending(a => a.date_added);
            if (!String.IsNullOrEmpty(keyword))
                result.Where(a => a.name.Contains(keyword));
            if(count != -1)
                result.Take(count);
            return result.ToList();
        }
        private tbl_item_category getOneItemCategory(int id)
        {
            var itemCategory = from ic in data.tbl_item_categories
                               where ic.id == id
                               select ic;
            if(itemCategory==null)
            {
                return new tbl_item_category();
            }
            return itemCategory.Single();
        }
        /*
         * 
         * 
         * 
         */
        public ActionResult Index()
        {
            return itemCategoryView();
        }
        /*
         * 
         * 
         * 
         */
        [HttpGet]
        public ActionResult itemCategoryView()
        {
            var listItemCategory = getItemCategory(10);
            return View(URLHelper.URL_ADMIN_ITEM_CATEGORY, listItemCategory);
        }
        [HttpPost]
        public ActionResult itemCategoryView(FormCollection form)
        {
            var keyword=form["keyword"];
            var listItemCategory = getItemCategory(10,keyword);
            return View(URLHelper.URL_ADMIN_ITEM_CATEGORY, listItemCategory);
        }
        /*
         * 
         * 
         * 
         */
        [HttpGet]
        public ActionResult itemCategoryCreate()
        {
            return View(URLHelper.URL_ADMIN_ITEM_CATEGORY_M, new Tuple<tbl_item_category,List<tbl_item_category>>(new tbl_item_category(),getAllItemCategory()));
        }
        [HttpPost]
        public ActionResult itemCategoryCreate(FormCollection form,HttpPostedFileBase fileUpload)
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
            if (String.IsNullOrEmpty(form["parent"]))
            {
                err = true;
                ViewData["Error"] += "Vui lòng chọn danh mục!\n";
            }
            else
            {
                tic.parent=Int32.Parse(form["parent"]);
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
            if (fileUpload != null)
            {
                var fileName =  Path.GetFileName(DateTime.Now.Millisecond+fileUpload.FileName);
                var path = Path.Combine(Server.MapPath(URLHelper.URL_IMAGE_PATH), fileName);
                if (!System.IO.File.Exists(path))
                {
                    fileUpload.SaveAs(path);
                }
                tic.image = fileName;
            }
            if (err == false)
            {
                data.tbl_item_categories.InsertOnSubmit(tic);
                data.SubmitChanges();
                return RedirectToAction("itemCategoryView");
            }
            else
            {
                return View(URLHelper.URL_ADMIN_ITEM_CATEGORY_M, new Tuple<tbl_item_category, List<tbl_item_category>>(tic, getAllItemCategory()));
            }
        }
        /*
         *
         *
         * 
         */
        [HttpGet]
        public ActionResult itemCategoryEdit(String id)
        {
            return View(URLHelper.URL_ADMIN_ITEM_CATEGORY_M, new Tuple<tbl_item_category, List<tbl_item_category>>(getOneItemCategory(Int32.Parse(id)), getAllItemCategory()));
        }
        [HttpPost]
        public ActionResult itemCategoryEdit(FormCollection form,HttpPostedFileBase fileUpload)
        {
            var id = form["id"];
            if (id == null)
            {
                return itemCategoryCreate(form,fileUpload);
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
                if (String.IsNullOrEmpty(form["parent"]))
                {
                    err = true;
                    ViewData["Error"] += "Vui lòng chọn danh mục!\n";
                }
                else
                {
                    tic.parent = Int32.Parse(form["parent"]);
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
                if (fileUpload != null)
                {
                    var fileName = Path.GetFileName(DateTime.Now.Millisecond + fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath(URLHelper.URL_IMAGE_PATH), fileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fileUpload.SaveAs(path);
                    }
                    String imageOlder = URLHelper.URL_IMAGE_PATH + tic.image;
                    if (System.IO.File.Exists(imageOlder))
                    {
                        System.IO.File.Delete(imageOlder);
                    }
                    tic.image = fileName;
                }
                if (err == false)
                {
                    UpdateModel(tic);
                    data.SubmitChanges();
                    return RedirectToAction("itemCategoryView");
                }
                else
                {
                    return View(URLHelper.URL_ADMIN_ITEM_CATEGORY_M, new Tuple<tbl_item_category, List<tbl_item_category>>(tic, getAllItemCategory()));
                }
            }
        }
        /*
         * 
         * 
         * 
         */
        public ActionResult itemCategoryDelete(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                ViewBag.AlertError = "Không xoá được!";
            }
            else
            {
                try
                {
                    data.tbl_item_categories.DeleteOnSubmit(getOneItemCategory(Int32.Parse(id)));
                    data.SubmitChanges();
                    ViewBag.AlertSuccess = "Xoá thành công!";
                }
                catch (Exception e)
                {
                    ViewBag.AlertError = "Không xoá được";
                }
            }
            return itemCategoryView();
        }
    }
}
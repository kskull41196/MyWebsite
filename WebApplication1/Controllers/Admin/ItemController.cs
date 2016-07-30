using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;

namespace WebApplication1.Controllers.Admin
{
    public class ItemController : Controller
    {
        DataClassesDataContext data = new DataClassesDataContext();
        // GET: ItemCategories
        private List<tbl_item_category> getItemCategories(int count)
        {
            return getItemCategories(count, "");
        }
        private List<tbl_item_category> getAllItemCategories()
        {
            return getItemCategories(-1, "");
        }
        private List<tbl_item_category> getItemCategories(int count, String keyword)
        {
            var result = data.tbl_item_categories.OrderByDescending(a => a.date_added);
            if (!String.IsNullOrEmpty(keyword))
                result.Where(a => a.name.Contains(keyword));
            if (count != -1)
                result.Take(count);
            return result.ToList();
        }
        // GET: Item
        private List<tbl_item> getItem(int count)
        {
            return getItem(count, "");
        }
        private List<tbl_item> getAllItem()
        {
            return getItem(-1, "");
        }
        private List<tbl_item> getItem(int count,String keyword)
        {
            var result= data.tbl_items.OrderByDescending(a => a.date_added);
            if (!String.IsNullOrEmpty(keyword))
                result.Where(a => a.name.Contains(keyword));
            if(count != -1)
                result.Take(count);
            return result.ToList();
        }
        private tbl_item getOneItem(int id)
        {
            var item = from ic in data.tbl_items
                               where ic.id == id
                               select ic;
            if(item==null)
            {
                return new tbl_item();
            }
            return item.Single();
        }
        /*
         * 
         * 
         * 
         */
        public ActionResult Index()
        {
            return itemView();
        }
        /*
         * 
         * 
         * 
         */
        [HttpGet]
        public ActionResult itemView()
        {
            var listItem = getItem(10);
            return View(URLHelper.URL_ADMIN_ITEM, listItem);
        }
        [HttpPost]
        public ActionResult itemView(FormCollection form)
        {
            var keyword=form["keyword"];
            var listItem = getItem(10,keyword);
            return View(URLHelper.URL_ADMIN_ITEM, listItem);
        }
        /*
         * 
         * 
         * 
         */
        [HttpGet]
        public ActionResult itemCreate()
        {
            return View(URLHelper.URL_ADMIN_ITEM_M, new Tuple<tbl_item,List<tbl_item_category>>(new tbl_item(),getAllItemCategories()));
        }
        [HttpPost]
        public ActionResult itemCreate(FormCollection form,HttpPostedFileBase fileUpload)
        {
            tbl_item tic = new tbl_item();
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
                data.tbl_items.InsertOnSubmit(tic);
                data.SubmitChanges();
                return RedirectToAction("itemView");
            }
            else
            {
                return View(URLHelper.URL_ADMIN_ITEM_M, new Tuple<tbl_item, List<tbl_item_category>>(tic, getAllItemCategories()));
            }
        }
        /*
         *
         *
         * 
         */
        [HttpGet]
        public ActionResult itemEdit(String id)
        {
            return View(URLHelper.URL_ADMIN_ITEM_M, new Tuple<tbl_item, List<tbl_item_category>>(getOneItem(Int32.Parse(id)), getAllItemCategories()));
        }
        [HttpPost]
        public ActionResult itemEdit(FormCollection form,HttpPostedFileBase fileUpload)
        {
            var id = form["id"];
            if (id == null)
            {
                return itemCreate(form,fileUpload);
            }
            else
            {
                tbl_item tic = getOneItem(Int32.Parse(id));
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
                    return RedirectToAction("itemView");
                }
                else
                {
                    return View(URLHelper.URL_ADMIN_ITEM_M, new Tuple<tbl_item, List<tbl_item_category>>(tic, getAllItemCategories()));
                }
            }
        }
        /*
         * 
         * 
         * 
         */
        public ActionResult itemDelete(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                ViewBag.AlertError = "Không xoá được!";
            }
            else
            {
                try
                {
                    data.tbl_items.DeleteOnSubmit(getOneItem(Int32.Parse(id)));
                    data.SubmitChanges();
                    ViewBag.AlertSuccess = "Xoá thành công!";
                }
                catch (Exception e)
                {
                    ViewBag.AlertError = "Không xoá được";
                }
            }
            return itemView();
        }
    }
}
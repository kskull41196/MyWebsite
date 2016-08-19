using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;

namespace WebApplication1.Controllers.Admin
{
    public class ItemController : BaseAdminController
    {
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
        private List<tbl_item> getItem(int count, String keyword)
        {

            if (!String.IsNullOrEmpty(keyword))
            {
                var result = data.tbl_items.Where(a => a.name.Contains(keyword)).OrderByDescending(a => a.date_added);
                if (count != -1)
                    result.Take(count);
                return result.ToList();
            }
            else
            {
                var result = data.tbl_items.OrderByDescending(a => a.date_added);
                if (count != -1)
                    result.Take(count);
                return result.ToList();
            }


        }
        private tbl_item getOneItem(int id)
        {
            var item = from ic in data.tbl_items
                       where ic.id == id
                       select ic;
            if (item == null)
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

        public ActionResult ItemSetHotEnable(int id)
        {
            tbl_item tic = getOneItem(id);
            tic.hot = (byte)(tic.hot == 1 ? 0 : 1);
            UpdateModel(tic);
            data.SubmitChanges();
            return RedirectToAction("itemView");
        }
        public ActionResult ItemSetStatusEnable(int id)
        {
            tbl_item tic = getOneItem(id);
            tic.status = (byte)(tic.status == 1 ? 0 : 1);
            UpdateModel(tic);
            data.SubmitChanges();
            return RedirectToAction("itemView");
        }

        /*
         * 
         * 
         * 
         */
        [HttpGet]
        public ActionResult itemView()
        {
            var listItem = getItem(50);
            return View(URLHelper.URL_ADMIN_ITEM, listItem);
        }
        [HttpPost]
        public ActionResult itemView(FormCollection form, String btnDel)
        {

            if (btnDel != null)
            {
                //Delete all
                DataHelper.ProductHelper.getInstance().deleteAllProduct(data);
            }

            var keyword = form["keyword"];
            var listItem = getItem(10, keyword);
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
            return View(URLHelper.URL_ADMIN_ITEM_M, new Tuple<tbl_item, List<tbl_item_category>>(new tbl_item(), getAllItemCategories()));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult itemCreate(FormCollection form, HttpPostedFileBase fileUpload)
        {
            tbl_item tic = new tbl_item();
            var name = form["name"];
            var sort = form["sort"];
            var title = form["title"];
            var description = form["description"];
            var keyword = form["keyword"];
            var price = form["price"];
            var price2 = form["price2"];
            var online_payment = form["online_payment"];
            var detail = form["detail"];
            var detail_short = form["detail_short"];
            
            bool err = false;
            if (String.IsNullOrEmpty(name))
            {
                err = true;
                ViewData["Error"] += "Vui lòng nhập tên sản phẩm!\n";
            }
            if (form["parent"].ToString().Equals("0"))
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
            tic.hot = (int)Constants.ItemStatus.NORMAL;
            tic.date_added = DateTime.Now;
            tic.last_modified = DateTime.Now;
            if (!String.IsNullOrEmpty(price))
                tic.price = Int32.Parse(price);
            else
                tic.price = 0;
            if (!String.IsNullOrEmpty(price2))
                tic.promotion_price = Int32.Parse(price2);
            else
                tic.promotion_price = 0;
            tic.online_payment = online_payment;
            tic.detail = detail;
            tic.detail_short = detail_short;
            if (form["chkClearImg"] != null)
            {
                tic.image = "";
            }
            else if (fileUpload != null)
            {
                var fileName = Path.GetFileName(DateTime.Now.Millisecond + fileUpload.FileName);
                var path = Path.Combine(Server.MapPath(URLHelper.URL_IMAGE_PATH + "item/"), fileName);
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
        [HttpPost, ValidateInput(false)]
        public ActionResult itemEdit(FormCollection form, HttpPostedFileBase fileUpload)
        {
            var id = form["id"];
            if (id == null)
            {
                return itemCreate(form, fileUpload);
            }
            else
            {
                tbl_item tic = getOneItem(Int32.Parse(id));
                var name = form["name"];
                var sort = form["sort"];
                var title = form["title"];
                var description = form["description"];
                var keyword = form["keyword"];
                var price = form["price"];
                var price2 = form["price2"];
                var online_payment = form["online_payment"];
                var detail = form["detail"];
                var detail_short = form["detail_short"];
                bool err = false;
                if (String.IsNullOrEmpty(name))
                {
                    err = true;
                    ViewData["Error"] += "Vui lòng nhập tên sản phẩm!\n";
                }
                if (form["parent"].ToString().Equals("0"))
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
                if (!String.IsNullOrEmpty(price))
                    tic.price = Int32.Parse(price);
                else
                    tic.price = 0;
                if (!String.IsNullOrEmpty(price2))
                    tic.promotion_price = Int32.Parse(price2);
                else
                    tic.promotion_price = 0;
                tic.online_payment = online_payment;
                tic.detail = detail;
                tic.detail_short = detail_short;
                if(form["chkClearImg"] != null)
                {
                    tic.image = "";
                }else if (fileUpload != null)
                {
                    var fileName = Path.GetFileName(DateTime.Now.Millisecond + fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath(URLHelper.URL_IMAGE_PATH + "item/"), fileName);
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
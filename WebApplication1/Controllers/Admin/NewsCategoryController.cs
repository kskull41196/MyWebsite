using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;

namespace WebApplication1.Controllers.Admin
{
    public class NewsCategoryController : BaseAdminController
    {
        // GET: NewsCategory
        private List<tbl_news_category> getNewsCategory(int count)
        {
            return getNewsCategory(count, "");
        }
        private List<tbl_news_category> getAllNewsCategory()
        {
            return getNewsCategory(-1, "");
        }
        private List<tbl_news_category> getNewsCategory(int count, String keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                var result = data.tbl_news_categories.Where(a => a.name.Contains(keyword)).OrderByDescending(a => a.date_added);
                if (count != -1)
                    result.Take(count);
                return result.ToList();
            }
            else
            {
                var result = data.tbl_news_categories.OrderByDescending(a => a.date_added);
                if (count != -1)
                    result.Take(count);
                return result.ToList();
            }
        }
        private tbl_news_category getOneNewsCategory(int id)
        {
            var newsCategory = from ic in data.tbl_news_categories
                               where ic.id == id
                               select ic;
            if (newsCategory == null)
            {
                return new tbl_news_category();
            }
            return newsCategory.Single();
        }
        /*
         * 
         * 
         * 
         */
        public ActionResult Index()
        {
            return newsCategoryView();
        }
        /*
         * 
         * 
         * 
         */
        [HttpGet]
        public ActionResult newsCategoryView()
        {
            var listNewsCategory = getNewsCategory(10);
            return View(URLHelper.URL_ADMIN_NEWS_CATEGORY, listNewsCategory);
        }
        [HttpPost]
        public ActionResult newsCategoryView(FormCollection form, String btnDel)
        {
            if (String.IsNullOrEmpty(btnDel))
            {
                //Delete all
                DataHelper.NewsHelper.getInstance().deleteAllNewsCategory(data);
            }

            var keyword = form["keyword"];
            var listNewsCategory = getNewsCategory(10, keyword);
            return View(URLHelper.URL_ADMIN_NEWS_CATEGORY, listNewsCategory);
        }
        /*
         * 
         * 
         * 
         */
        [HttpGet]
        public ActionResult newsCategoryCreate()
        {
            return View(URLHelper.URL_ADMIN_NEWS_CATEGORY_M, new Tuple<tbl_news_category, List<tbl_news_category>>(new tbl_news_category(), getAllNewsCategory()));
        }
        [HttpPost]
        public ActionResult newsCategoryCreate(FormCollection form, HttpPostedFileBase fileUpload)
        {
            tbl_news_category tic = new tbl_news_category();
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
            if (form["chkClearImg"] != null)
            {
                tic.image = "";
            }
            else if (fileUpload != null)
            {
                var fileName = Path.GetFileName(DateTime.Now.Millisecond + fileUpload.FileName);
                var path = Path.Combine(Server.MapPath(URLHelper.URL_IMAGE_PATH + "news_category/"), fileName);
                if (!System.IO.File.Exists(path))
                {
                    fileUpload.SaveAs(path);
                }
                tic.image = fileName;
            }
            if (err == false)
            {
                data.tbl_news_categories.InsertOnSubmit(tic);
                data.SubmitChanges();
                return RedirectToAction("newsCategoryView");
            }
            else
            {
                return View(URLHelper.URL_ADMIN_NEWS_CATEGORY_M, new Tuple<tbl_news_category, List<tbl_news_category>>(tic, getAllNewsCategory()));
            }
        }
        /*
         *
         *
         * 
         */
        [HttpGet]
        public ActionResult newsCategoryEdit(String id)
        {
            return View(URLHelper.URL_ADMIN_NEWS_CATEGORY_M, new Tuple<tbl_news_category, List<tbl_news_category>>(getOneNewsCategory(Int32.Parse(id)), getAllNewsCategory()));
        }
        [HttpPost]
        public ActionResult newsCategoryEdit(FormCollection form, HttpPostedFileBase fileUpload)
        {
            var id = form["id"];
            if (id == null)
            {
                return newsCategoryCreate(form, fileUpload);
            }
            else
            {
                tbl_news_category tic = getOneNewsCategory(Int32.Parse(id));
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
                if (form["chkClearImg"] != null)
                {
                    tic.image = "";
                }
                else if (fileUpload != null)
                {
                    var fileName = Path.GetFileName(DateTime.Now.Millisecond + fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath(URLHelper.URL_IMAGE_PATH + "news_category/"), fileName);
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
                    return RedirectToAction("newsCategoryView");
                }
                else
                {
                    return View(URLHelper.URL_ADMIN_NEWS_CATEGORY_M, new Tuple<tbl_news_category, List<tbl_news_category>>(tic, getAllNewsCategory()));
                }
            }
        }
        /*
         * 
         * 
         * 
         */
        public ActionResult newsCategoryDelete(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                ViewBag.AlertError = "Không xoá được!";
            }
            else
            {
                try
                {
                    data.tbl_news_categories.DeleteOnSubmit(getOneNewsCategory(Int32.Parse(id)));
                    data.SubmitChanges();
                    ViewBag.AlertSuccess = "Xoá thành công!";
                }
                catch (Exception e)
                {
                    ViewBag.AlertError = "Không xoá được";
                }
            }
            return newsCategoryView();
        }
    }
}
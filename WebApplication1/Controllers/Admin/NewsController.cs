using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;

namespace WebApplication1.Controllers.Admin
{
    public class NewsController : BaseAdminController
    {
        //GET: News categories
        private List<tbl_news_category> getNewsCategories(int count)
        {
            return getNewsCategories(count, "");
        }
        private List<tbl_news_category> getAllNewsCategories()
        {
            return getNewsCategories(-1, "");
        }
        private List<tbl_news_category> getNewsCategories(int count, String keyword)
        {
            var result = data.tbl_news_categories.OrderByDescending(a => a.date_added);
            if (!String.IsNullOrEmpty(keyword))
                result.Where(a => a.name.Contains(keyword));
            if (count != -1)
                result.Take(count);
            return result.ToList();
        }
        // GET: News
        private List<tbl_new> getNews(int count)
        {
            return getNews(count, "");
        }
        private List<tbl_new> getAllNews()
        {
            return getNews(-1, "");
        }
        private List<tbl_new> getNews(int count, String keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                var result = data.tbl_news.Where(a => a.name.Contains(keyword)).OrderByDescending(a => a.date_added);
                if (count != -1)
                    result.Take(count);
                return result.ToList();
            }
            else
            {
                var result = data.tbl_news.OrderByDescending(a => a.date_added);
                if (count != -1)
                    result.Take(count);
                return result.ToList();
            }
        }

        public ActionResult NewsSetHotEnable(int id)
        {
            tbl_new tic = getOneNews(id);
            tic.hot = (byte)(tic.hot == 1 ? 0 : 1);
            UpdateModel(tic);
            data.SubmitChanges();
            return RedirectToAction("newsView");
        }
        public ActionResult NewsSetStatusEnable(int id)
        {
            tbl_new tic = getOneNews(id);
            tic.status = (byte)(tic.status == 1 ? 0 : 1);
            UpdateModel(tic);
            data.SubmitChanges();
            return RedirectToAction("newsView");
        }

        private tbl_new getOneNews(int id)
        {
            var news = from ic in data.tbl_news
                       where ic.id == id
                       select ic;
            if (news == null)
            {
                return new tbl_new();
            }
            return news.Single();
        }
        /*
         * 
         * 
         * 
         */
        public ActionResult Index()
        {
            return newsView();
        }
        /*
         * 
         * 
         * 
         */
        [HttpGet]
        public ActionResult newsView()
        {
            var listNews = getNews(10);
            return View(URLHelper.URL_ADMIN_NEWS, listNews);
        }
        [HttpPost]
        public ActionResult newsView(FormCollection form, String btnDel)
        {
            if (String.IsNullOrEmpty(btnDel))
            {
                //Delete all
                DataHelper.NewsHelper.getInstance().deleteAllNews(data);
            }

            var keyword = form["keyword"];
            var listNews = getNews(10, keyword);
            return View(URLHelper.URL_ADMIN_NEWS, listNews);
        }
        /*
         * 
         * 
         * 
         */
        [HttpGet]
        public ActionResult newsCreate()
        {
            return View(URLHelper.URL_ADMIN_NEWS_M, new Tuple<tbl_new, List<tbl_news_category>>(new tbl_new(), getAllNewsCategories()));
        }
        [HttpPost]
        public ActionResult newsCreate(FormCollection form, HttpPostedFileBase fileUpload)
        {
            tbl_new tic = new tbl_new();
            var name = form["name"];
            var sort = form["sort"];
            var title = form["title"];
            var description = form["description"];
            var keyword = form["keyword"];
            bool err = false;
            if (String.IsNullOrEmpty(name))
            {
                err = true;
                ViewData["Error"] += "Vui lòng nhập tên tin tức!\n";
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
                var path = Path.Combine(Server.MapPath(URLHelper.URL_IMAGE_PATH + "news/"), fileName);
                if (!System.IO.File.Exists(path))
                {
                    fileUpload.SaveAs(path);
                }
                tic.image = fileName;
            }
            if (err == false)
            {
                data.tbl_news.InsertOnSubmit(tic);
                data.SubmitChanges();
                return RedirectToAction("newsView");
            }
            else
            {
                return View(URLHelper.URL_ADMIN_NEWS_M, new Tuple<tbl_new, List<tbl_news_category>>(tic, getAllNewsCategories()));
            }
        }
        /*
         *
         *
         * 
         */
        [HttpGet]
        public ActionResult newsEdit(String id)
        {
            return View(URLHelper.URL_ADMIN_NEWS_M, new Tuple<tbl_new, List<tbl_news_category>>(getOneNews(Int32.Parse(id)), getAllNewsCategories()));
        }
        [HttpPost]
        public ActionResult newsEdit(FormCollection form, HttpPostedFileBase fileUpload)
        {
            var id = form["id"];
            if (id == null)
            {
                return newsCreate(form, fileUpload);
            }
            else
            {
                tbl_new tic = getOneNews(Int32.Parse(id));
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
                    var path = Path.Combine(Server.MapPath(URLHelper.URL_IMAGE_PATH + "news/"), fileName);
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
                    return RedirectToAction("newsView");
                }
                else
                {
                    return View(URLHelper.URL_ADMIN_NEWS_M, new Tuple<tbl_new, List<tbl_news_category>>(tic, getAllNewsCategories()));
                }
            }
        }
        /*
         * 
         * 
         * 
         */
        public ActionResult newsDelete(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                ViewBag.AlertError = "Không xoá được!";
            }
            else
            {
                try
                {
                    data.tbl_news.DeleteOnSubmit(getOneNews(Int32.Parse(id)));
                    data.SubmitChanges();
                    ViewBag.AlertSuccess = "Xoá thành công!";
                }
                catch (Exception e)
                {
                    ViewBag.AlertError = "Không xoá được";
                }
            }
            return newsView();
        }
    }
}
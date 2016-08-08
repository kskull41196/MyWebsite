using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(string id, string id2)
        {
            ViewData["ListModulesHome"] = getHomeModule();
            return View();
        }

        public ActionResult Items()
        {
            return View(URLHelper.URL_HOME_ALL_PRODUCTS, getListAllProducts());
        }
        
        public ActionResult News()
        {
            ViewBag.Message = "Tin Tức";
            return View(URLHelper.URL_HOME_NEWS, getListAllNews());
        }

        public ActionResult Policy()
        {
            ViewBag.Message = "Chính Sách";
            return View(URLHelper.URL_HOME_POLICY, getListAllPolicy());
        }

        public ActionResult NewsDetail(int id)
        {
            tbl_new newsToShowDetail = getNewsById(id);
            List<tbl_new> listNewsWithTheSameCategory = null;
            if (newsToShowDetail != null && newsToShowDetail.parent.HasValue)
            {
                listNewsWithTheSameCategory = getListOtherNewsByCategory(newsToShowDetail.id, newsToShowDetail.parent.Value);
            }
            return View(URLHelper.URL_HOME_NEWS_DETAIL, new Tuple<tbl_new, List<tbl_new>>(newsToShowDetail, listNewsWithTheSameCategory));
        }

        public ActionResult ProductDetail(int id)
        {
            tbl_item itemToShowDetail = getProductById(id);
            List<tbl_item> listItemWithTheSameCategory = null;
            if (itemToShowDetail != null && itemToShowDetail.parent.HasValue)
            {
                listItemWithTheSameCategory = getListOtherProductsByCategory(itemToShowDetail.id, itemToShowDetail.parent.Value);
            }
            return View(URLHelper.URL_HOME_PRODUCT_DETAIL, new Tuple<tbl_item, List<tbl_item>>(itemToShowDetail, listItemWithTheSameCategory));
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }



        //Getting data from models
        private tbl_item getProductById(int id)
        {
            tbl_item result = data.tbl_items.Where(n => n.id == id).Single();
            return result;
        }

        private tbl_new getNewsById(int id)
        {
            tbl_new result = data.tbl_news.Where(n => n.id == id).Single();
            return result;
        }

        private List<tbl_item> getListAllProducts()
        {
            return data.tbl_items.OrderByDescending(a => a.date_added).ToList();
        }

        private tbl_news_category getOneNewsCategory(string categoryName)
        {
            var itemCategory = from ic in data.tbl_news_categories
                               where ic.name.Equals(categoryName)
                               select ic;
            if (itemCategory == null || !itemCategory.Any())
            {
                return new tbl_news_category();
            }
            return itemCategory.Single();
        }
        
        //This method gets all records in tbl_news which has parent name == Constants.NEWS_CATEGORY_NAME_NEWS
        private List<tbl_new> getListAllNews()
        {
            return data.tbl_news.OrderByDescending(a => a.date_added).Where(n => n.parent.Value == getOneNewsCategory(Constants.NEWS_CATEGORY_NAME_NEWS).id).ToList();
        }
        
        //This method gets all records in tbl_news which has parent name == Constants.NEWS_CATEGORY_NAME_POLICY
        private List<tbl_new> getListAllPolicy()
        {
            return data.tbl_news.OrderByDescending(a => a.date_added).Where(n => n.parent.Value == getOneNewsCategory(Constants.NEWS_CATEGORY_NAME_POLICY).id).ToList();
        }
        
        private List<tbl_new> getListOtherNewsByCategory(int id, int parent)
        {
            return data.tbl_news.OrderByDescending(a => a.date_added).Where(n => n.parent == parent && n.id != id).ToList();
        }

        private List<tbl_item> getListOtherProductsByCategory(int id, int parent)
        {
            return data.tbl_items.OrderByDescending(a => a.date_added).Where(n => n.parent == parent && n.id != id).ToList();
        }

        private List<tbl_module> getHomeModule()
        {
            var result = data.tbl_modules.Where(a => a.type == 2).OrderByDescending(a => a.date_added);
            return result.ToList();
        }
    }
}
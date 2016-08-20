using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PartialViewController : BaseController
    {
        /*
         * 
         * Load data
         * 
         */
        public int getNewsCategoryId()
        {
            var menuNewsCategories = from tic in data.tbl_news_categories where tic.name.Equals(Constants.NEWS_CATEGORY_NAME_NEWS) select tic;
            return menuNewsCategories.Any() ? menuNewsCategories.Single().id : -1;
        }

        public int getPolicyCategoryId()
        {
            var menuNewsCategories = from tic in data.tbl_news_categories where tic.name.Equals(Constants.NEWS_CATEGORY_NAME_POLICY) select tic;
            return menuNewsCategories.Any() ? menuNewsCategories.Single().id : -1;
        }

        /*
         * 
         * Partial View show module home
         * 
         */
        public ActionResult listProductHot()
        {
            var listProductHot = from ti in data.tbl_items where ti.status == 1 && ti.hot == 1 select ti;
            return PartialView(URLHelper.URL_HOME_PARTIAL_PRODUCT_HOT, listProductHot);
        }

        /*
         * 
         * Partial View show module left/right
         * 
         */
        public ActionResult menuItemCategories(){
            var menuItemCategories = from tic in data.tbl_item_categories where tic.parent == 0 select tic;
            if (!menuItemCategories.Any())
            {
                return null;
            }
            return PartialView(URLHelper.URL_HOME_PARTIAL_ITEM_CATEGORIES,menuItemCategories);
        }
        public ActionResult menuNewsCategories()
        {
            var menuNewsCategories = from tnc in data.tbl_news_categories where (tnc.parent == 0 && tnc.id != getNewsCategoryId() && tnc.id != getPolicyCategoryId()) select tnc;
            if (menuNewsCategories.Any())
            {
                return PartialView(URLHelper.URL_HOME_PARTIAL_NEWS_CATEGORIES, menuNewsCategories);
            }
            return null;
        }
        public ActionResult topProductView()
        {
            var topProductView = from ti in data.tbl_items where ti.status == 1 orderby ti.view_amount descending select ti;
            if (!topProductView.Any())
            {
                return null;
            }
            List<tbl_item> listProductView = topProductView.ToList();
            if (listProductView == null) listProductView = new List<tbl_item>();
            return PartialView(URLHelper.URL_HOME_PARTIAL_TOP_PRODUCT, topProductView);
        }

        public ActionResult supportOnline()
        {
            List<tbl_support> listSupporters = DataHelper.GeneralHelper.getInstance().getAllSupporters(data);
            if (listSupporters.Count() == 0)
                return null;
            return PartialView(URLHelper.URL_HOME_PARTIAL_SUPPORT_ONLINE, listSupporters);
        }
    }
}
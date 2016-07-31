using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PartialViewController : Controller
    {
        DataClassesDataContext data = new DataClassesDataContext();
        // method menuMain() get menu all menu main
        public ActionResult menuMain()
        {
            String menuMain = "<div id=\"mainNav\"><div id=\"cssmenu\"><div id=\"menu-button\">Menu</div><ul><li class=\"\"><a href=\"\">Trang chủ</a></li>";
            var itemCategoryHot = from tic in data.tbl_item_categories where tic.hot == 1 select tic;
            foreach(tbl_item_category itemCategory in itemCategoryHot){
                menuMain += "<li class=\"\"><a href=\"\">"+itemCategory.name+"</a>";
                menuMainItemCategoryLoop(itemCategory.id);
                menuMain += "</li>";
            }
            var newsCategoryHot = from tnc in data.tbl_news_categories where tnc.hot == 1 select tnc;
            foreach (tbl_news_category newsCategory in newsCategoryHot)
            {
                menuMain += "<li class=\"\"><a href=\"\">" + newsCategory.name + "</a>";
                menuMainNewsCategoryLoop(newsCategory.id);
                menuMain += "</li>";
            }
            menuMain += "<li class=\"\"><a href=\"\">Liên hệ</a></li>";
            return PartialView(menuMain);
        }
        // method menuMain(id) get menu main by id
        public String menuMainItemCategoryLoop(Int32 idParent)
        {
            String menuMain = "";
            var menuMainLoop = from tic in data.tbl_item_categories where tic.parent == idParent select tic;
            if (menuMainLoop.Count() > 0) {
                menuMain += "<ul>";
                foreach (tbl_item_category menuChildItemCategory in menuMainLoop)
                {
                    menuMain += "<li class=\"\"><a href=\"\">"+menuChildItemCategory.name+"</a>";
                    menuMainItemCategoryLoop(menuChildItemCategory.id);
                    menuMain += "</li>";
                }
                menuMain += "</ul>";
            }
            return menuMain;
        }
        public String menuMainNewsCategoryLoop(Int32 idParent)
        {
            String menuMain = "";
            var menuMainLoop = from tnc in data.tbl_news_categories where tnc.parent == idParent select tnc;
            if (menuMainLoop.Count() > 0)
            {
                menuMain += "<ul>";
                foreach (tbl_news_category menuChildNewsCategory in menuMainLoop)
                {
                    menuMain += "<li class=\"\"><a href=\"\">" + menuChildNewsCategory.name + "</a>";
                    menuMainNewsCategoryLoop(menuChildNewsCategory.id);
                    menuMain += "</li>";
                }
                menuMain += "</ul>";
            }
            return menuMain;
        }
        public ActionResult menuItemCategories(){
            var menuItemCategories = from tic in data.tbl_item_categories where tic.status == 1 && tic.parent == 0 select tic;
            return PartialView(menuItemCategories);
        }
        public ActionResult menuNewsCategories()
        {
            var menuNewsCategories = from tnc in data.tbl_item_categories where tnc.status == 1 && tnc.parent == 0 select tnc;
            return PartialView(menuNewsCategories);
        }
        public ActionResult topProductView()
        {
            var topProductView = from ti in data.tbl_items where ti.status == 1 orderby ti.view_amount descending select ti;
            List<tbl_item> listProductView = topProductView.ToList();
            if (listProductView == null) listProductView = new List<tbl_item>();
            return PartialView(topProductView);
        }
        public ActionResult listProductHot()
        {
            var productHot = from ti in data.tbl_items where ti.status == 1 && ti.hot == 1 select ti;
            List<tbl_item> listProductHot = productHot.ToList();
            if (listProductHot == null) listProductHot = new List<tbl_item>();
            return PartialView(listProductHot);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Controllers
{
    public class URLHelper
    {
        //Image paths
        public const String URL_IMAGE_PATH = "~/assets/images/";

        public const String URL_ADMIN_ITEM_CATEGORY = "~/Views/Admin/ItemCategory/ItemCategory.cshtml";
        public const String URL_ADMIN_ITEM_CATEGORY_M = "~/Views/Admin/ItemCategory/ItemCategory_m.cshtml";
        public const String URL_ADMIN_ITEM = "~/Views/Admin/Item/Item.cshtml";
        public const String URL_ADMIN_ITEM_M = "~/Views/Admin/Item/Item_m.cshtml";
        public const String URL_ADMIN_NEWS_CATEGORY = "~/Views/Admin/NewsCategory/NewsCategory.cshtml";
        public const String URL_ADMIN_NEWS_CATEGORY_M = "~/Views/Admin/NewsCategory/NewsCategory_m.cshtml";
        public const String URL_ADMIN_NEWS = "~/Views/Admin/News/News.cshtml";
        public const String URL_ADMIN_NEWS_M = "~/Views/Admin/News/News_m.cshtml";
        public const String URL_ADMIN_CONFIGSHOP = "~/Views/Admin/ShopConfig/ShopConfig.cshtml";
        public const String URL_ADMIN_MODULE = "~/Views/Admin/Module/Module.cshtml";
        public const String URL_ADMIN_MODULE_M = "~/Views/Admin/Module/Module_m.cshtml";

        //Partial Views' URLs
        public const String URL_HOME_PARTIAL_SUPPORT_ONLINE = "~/Views/PartialViews/Module/PartialOnlineSupport.cshtml";
        public const String URL_HOME_PARTIAL_ITEM_CATEGORIES = "~/Views/PartialViews/Module/PartialItemCategories.cshtml";
        public const String URL_HOME_PARTIAL_PRODUCT_HOT = "~/Views/PartialViews/Home/PartialHotProduct.cshtml";
    }
}
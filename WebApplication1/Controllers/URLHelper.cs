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

        //Home URL
        public const String URL_HOME_ALL_PRODUCTS = "~/Views/Home/AllProducts.cshtml";
        public const String URL_HOME_PRODUCTS_BY_CATEGORY = "~/Views/Home/ProductsByCategory.cshtml";
        public const String URL_HOME_NEWS_BY_CATEGORY = "~/Views/Home/NewsByCategory.cshtml";
        public const String URL_HOME_PRODUCT_DETAIL = "~/Views/Home/ProductDetail.cshtml";
        public const String URL_HOME_PAY_SHOPPING_CARD = "~/Views/Home/PayShoppingCard.cshtml";
        public const String URL_HOME_PAY_SHOPPING_CARD_SUCCESSFULLY = "~/Views/Home/PayShoppingCardSuccessfully.cshtml";
        public const String URL_HOME_SHOPPING_CARD = "~/Views/Home/ShoppingCard.cshtml";
        public const String URL_HOME_NEWS_DETAIL = "~/Views/Home/NewsDetail.cshtml";
        public const String URL_HOME_NEWS = "~/Views/Home/News.cshtml";
        public const String URL_HOME_ABOUT = "~/Views/Home/About.cshtml";
        public const String URL_HOME_POLICY = "~/Views/Home/News.cshtml";
        public const String URL_HOME_LOGIN = "~/Views/Home/Login.cshtml";
        public const String URL_HOME_SIGNUP = "~/Views/Home/SignUp.cshtml";
        public const String URL_HOME_COMPLETE_SIGNUP = "~/Views/Home/CompleteSignUp.cshtml";
        public const String URL_HOME_ACTIVATE_MEMBER_ACCOUNT = "~/Views/Home/ActivateMemberAccount.cshtml";
        public const String URL_HOME_FORGOT_PASSWORD = "~/Views/Home/ForgotPassword.cshtml";
        public const String URL_HOME_ITEM_PAID = "~/Views/Home/ItemPaid.cshtml";
        public const String URL_HOME_ITEM_CANCEL = "~/Views/Home/ItemCancel.cshtml";
        public const String URL_HOME_CONTACT = "~/Views/Home/Contact.cshtml";

        //Admin URL
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
        public const String URL_ADMIN_SUPPORT = "~/Views/Admin/Support/Support.cshtml";
        public const String URL_ADMIN_SUPPORT_M = "~/Views/Admin/Support/Support_m.cshtml";
        public const String URL_ADMIN_MEMBER = "~/Views/Admin/Member/Member.cshtml";
        public const String URL_ADMIN_ORDER = "~/Views/Admin/Order/Order.cshtml";
        public const String URL_ADMIN_ORDER_DETAIL = "~/Views/Admin/Order/OrderDetail.cshtml";

        //Partial Views' URLs
        public const String URL_HOME_PARTIAL_SUPPORT_ONLINE = "~/Views/PartialViews/Module/PartialOnlineSupport.cshtml";
        public const String URL_HOME_PARTIAL_ITEM_CATEGORIES = "~/Views/PartialViews/Module/PartialItemCategories.cshtml";
        public const String URL_HOME_PARTIAL_NEWS_CATEGORIES = "~/Views/PartialViews/Module/PartialNewsCategories.cshtml";
        public const String URL_HOME_PARTIAL_TOP_PRODUCT = "~/Views/PartialViews/Module/PartialTopProductView.cshtml";
        public const String URL_HOME_PARTIAL_PRODUCT_HOT = "~/Views/PartialViews/Home/PartialHotProduct.cshtml";
    }
}
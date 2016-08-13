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
            return View(URLHelper.URL_HOME_ALL_PRODUCTS, DataHelper.getInstance().getListAllProducts(data));
        }

        public ActionResult News()
        {
            ViewBag.Message = "Tin Tức";
            return View(URLHelper.URL_HOME_NEWS, DataHelper.getInstance().getListAllNews(data));
        }

        public ActionResult Policy()
        {
            ViewBag.Message = "Chính Sách";
            return View(URLHelper.URL_HOME_POLICY, DataHelper.getInstance().getListAllPolicy(data));
        }

        public ActionResult NewsDetail(int id)
        {
            tbl_new newsToShowDetail = DataHelper.getInstance().getNewsById(data, id);
            List<tbl_new> listNewsWithTheSameCategory = null;
            if (newsToShowDetail != null && newsToShowDetail.parent.HasValue)
            {
                listNewsWithTheSameCategory = DataHelper.getInstance().getListOtherNewsByCategory(data, newsToShowDetail.id, newsToShowDetail.parent.Value);
            }
            return View(URLHelper.URL_HOME_NEWS_DETAIL, new Tuple<tbl_new, List<tbl_new>>(newsToShowDetail, listNewsWithTheSameCategory));
        }

        public ActionResult AddItemToShoppingCard(int id, long? price, int? quantity)
        {
            //Save this item to shopping card
            long mPrice = 0;
            int mAmount = 0;
            if (price.HasValue)
            {
                mPrice = price.Value;
            }
            if (quantity.HasValue)
            {
                mAmount = quantity.Value;
            }
            DataHelper.getInstance().addItemsToShoppingCard(this, id, mPrice, mAmount);

            //Reload the current page.
            return RedirectToAction("ProductDetail", new { id = id });
        }

        public ActionResult ProductDetail(int id)
        {
            tbl_item itemToShowDetail = DataHelper.getInstance().getProductById(data, id);
            List<tbl_item> listItemWithTheSameCategory = null;
            if (itemToShowDetail != null && itemToShowDetail.parent.HasValue)
            {
                listItemWithTheSameCategory = DataHelper.getInstance().getListOtherProductsByCategory(data, itemToShowDetail.id, itemToShowDetail.parent.Value);
            }
            return View(URLHelper.URL_HOME_PRODUCT_DETAIL, new Tuple<tbl_item, List<tbl_item>>(itemToShowDetail, listItemWithTheSameCategory));
        }

        public ActionResult PayShoppingCard()
        {
            return View(URLHelper.URL_HOME_PAY_SHOPPING_CARD);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult ShoppingCard()
        {
            long totalCost = 0;
            List<tbl_order_detail> shoppingCard = DataHelper.getInstance().getShoppingCardInSession(this);
            foreach (tbl_order_detail record in shoppingCard.ToList())
            {
                if (record.price.HasValue && record.quantity.HasValue)
                {
                    totalCost += record.price.Value * record.quantity.Value;
                }
            }

            ViewData[Constants.KEY_VIEWDATA_SHOPPING_CARD_ALL_ITEMS_COST] = totalCost;
            return View(URLHelper.URL_HOME_SHOPPING_CARD, shoppingCard);
        }

        public ActionResult DeleteFromShoppingCard(int id)
        {
            DataHelper.getInstance().DeleteItemsFromShoppingCard(this, id);
            if (DataHelper.getInstance().getShoppingCardInSession(this).Count() > 0)
            {
                return RedirectToAction("ShoppingCard");
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAllFromShoppingCard()
        {
            DataHelper.getInstance().clearShoppingCard(this);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ShoppingCard(FormCollection form)
        {
            return UpdateShoppingCard(form);
        }

        public ActionResult UpdateShoppingCard(FormCollection form)
        {
            List<tbl_order_detail> shoppingCard = DataHelper.getInstance().getShoppingCardInSession(this);
            bool shoppingCardHasBeenChanged = false;
            foreach (tbl_order_detail itemInShoppingCard in shoppingCard.ToList())
            {
                foreach (var key in form.Keys)
                {
                    if (key.ToString().Equals("itemAmount_" + itemInShoppingCard.id_product))
                    {
                        var value = form[key.ToString()];
                        itemInShoppingCard.quantity = Int32.Parse(value);
                        shoppingCardHasBeenChanged = true;
                    }
                }

            }

            if (shoppingCardHasBeenChanged)
            {
                DataHelper.getInstance().updateShoppingCard(this, shoppingCard);
            }
            return RedirectToAction("ShoppingCard");
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var email = form["email"];
            var password = form["password"];
            if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password) &&
                DataHelper.getInstance().loginMember(data, email, password))
            {
                //TODO, save session here
                Session[Constants.KEY_SESSION_MEMBER_USERNAME] = email;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Vui lòng kiểm tra email hoặc mật khẩu.";
                return View(URLHelper.URL_HOME_LOGIN);
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(URLHelper.URL_HOME_LOGIN);
        }

        //Get home module
        private List<tbl_module> getHomeModule()
        {
            var result = data.tbl_modules.Where(a => a.type == 2).OrderByDescending(a => a.date_added);
            return result.ToList();
        }
    }
}
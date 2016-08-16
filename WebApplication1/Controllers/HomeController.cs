﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewData[Constants.KEY_VIEWDATA_LIST_MODULE_INDEX_PAGE] = DataHelper.GeneralHelper.getInstance().getHomeModule(data);
            return View();
        }

        public ActionResult Items()
        {
            return View(URLHelper.URL_HOME_ALL_PRODUCTS, DataHelper.ProductHelper.getInstance().getListAllProducts(data));
        }

        public ActionResult News()
        {
            ViewBag.Message = "Tin Tức";
            return View(URLHelper.URL_HOME_NEWS, DataHelper.NewsHelper.getInstance().getListAllNews(data));
        }

        public ActionResult Policy()
        {
            ViewBag.Message = "Chính Sách";
            return View(URLHelper.URL_HOME_POLICY, DataHelper.NewsHelper.getInstance().getListAllPolicy(data));
        }

        public ActionResult NewsDetail(int id)
        {
            tbl_new newsToShowDetail = DataHelper.NewsHelper.getInstance().getNewsById(data, id);
            List<tbl_new> listNewsWithTheSameCategory = null;
            if (newsToShowDetail != null && newsToShowDetail.parent.HasValue)
            {
                listNewsWithTheSameCategory = DataHelper.NewsHelper.getInstance().getListOtherNewsByCategory(data, newsToShowDetail.id, newsToShowDetail.parent.Value);
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
            DataHelper.ShoppingCardHelper.getInstance().addItemsToShoppingCard(this, id, mPrice, mAmount);

            //Reload the current page.
            return RedirectToAction("ProductDetail", new { id = id });
        }

        public ActionResult ProductDetail(int id)
        {
            tbl_item itemToShowDetail = DataHelper.ProductHelper.getInstance().getProductById(data, id);
            List<tbl_item> listItemWithTheSameCategory = null;
            if (itemToShowDetail != null && itemToShowDetail.parent.HasValue)
            {
                listItemWithTheSameCategory = DataHelper.ProductHelper.getInstance().getListOtherProductsByCategory(data, itemToShowDetail.id, itemToShowDetail.parent.Value);
            }
            return View(URLHelper.URL_HOME_PRODUCT_DETAIL, new Tuple<tbl_item, List<tbl_item>>(itemToShowDetail, listItemWithTheSameCategory));
        }

        [HttpGet]
        public ActionResult PayShoppingCard()
        {
            //If is logging in = false -> redirect to login page
            if (!DataHelper.AccountHelper.getInstance().checkIsMemberLoggingIn(HttpContext))
            {
                return RedirectToAction("Login");
            }

            return View(URLHelper.URL_HOME_PAY_SHOPPING_CARD);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult ShoppingCard()
        {
            long totalCost = 0;
            List<DataHelper.ShoppingCardItemModel> shoppingCard = DataHelper.ShoppingCardHelper.getInstance().getShoppingCardItemModelsInSession(this);
            foreach (DataHelper.ShoppingCardItemModel record in shoppingCard.ToList())
            {
                totalCost += record.total;
            }

            ViewData[Constants.KEY_VIEWDATA_SHOPPING_CARD_ALL_ITEMS_COST] = totalCost;
            return View(URLHelper.URL_HOME_SHOPPING_CARD, shoppingCard);
        }

        public ActionResult DeleteFromShoppingCard(int id)
        {
            DataHelper.ShoppingCardHelper.getInstance().DeleteItemsFromShoppingCard(this, id);
            if (DataHelper.ShoppingCardHelper.getInstance().getShoppingCardInSession(this).Count() > 0)
            {
                return RedirectToAction("ShoppingCard");
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAllFromShoppingCard()
        {
            DataHelper.ShoppingCardHelper.getInstance().clearShoppingCard(this);
            return RedirectToAction("Index");
        }

        public ActionResult ActivateMemberAccount(string email)
        {
            if (DataHelper.AccountHelper.getInstance().activateMemberAccount(this, email))
            {
                string password = DataHelper.AccountHelper.getInstance().getPasswordOfMemberAccount(data, email);
                DataHelper.AccountHelper.getInstance().loginMember(data, email, password);
                ViewBag.Message = "Kích hoạt tài khoản thành công!";
            }
            else
            {
                ViewBag.Message = "Không còn có thể kích hoạt tài khoản này nữa.";
            }
            return View(URLHelper.URL_HOME_ACTIVATE_MEMBER_ACCOUNT);
        }

        [HttpPost]
        public ActionResult ShoppingCard(FormCollection form)
        {
            return UpdateShoppingCard(form);
        }

        public ActionResult UpdateShoppingCard(FormCollection form)
        {
            List<tbl_order_detail> shoppingCard = DataHelper.ShoppingCardHelper.getInstance().getShoppingCardInSession(this);
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
                DataHelper.ShoppingCardHelper.getInstance().updateShoppingCard(this, shoppingCard);
            }
            return RedirectToAction("ShoppingCard");
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var email = form["email"];
            var password = form["password"];
            if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password) &&
                DataHelper.AccountHelper.getInstance().loginMember(data, email, password))
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

        [HttpPost]
        public ActionResult SignUp(FormCollection form, String Gender)
        {
            bool isError = true;
            var email = form["email"];
            var password = form["password"];
            var fullname = form["fullname"];
            var phone = form["phone"];
            var passwordconfirm = form["passwordconfirm"];
            var address = form["address"];
            ViewData["email"] = email;
            ViewData["password"] = password;
            ViewData["fullname"] = fullname;
            ViewData["phone"] = phone;
            ViewData["passwordconfirm"] = passwordconfirm;
            ViewData["address"] = address;
            ViewData["gender"] = Gender;


            if (String.IsNullOrEmpty(phone.ToString()) || !Regex.IsMatch(phone.ToString(), "^[0-9]*$"))
            {
                ViewBag.ErrorMessage = "Điện thoại không đúng định dạng";
            }
            else if (password.ToString().Length < 6)
            {
                ViewBag.ErrorMessage = "Mật khẩu phải nhiều hơn 5 ký tự";
            }
            else if (!password.ToString().Equals(passwordconfirm.ToString()))
            {
                ViewBag.ErrorMessage = "Mật khẩu xác nhận không trùng khớp";
            }
            else if (String.IsNullOrEmpty(email.ToString()) || !Regex.IsMatch(email.ToString(), "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$"))
            {
                ViewBag.ErrorMessage = "Email không đúng định dạng";
            }
            else if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password)
               || String.IsNullOrEmpty(fullname) || String.IsNullOrEmpty(phone)
               || String.IsNullOrEmpty(passwordconfirm) || String.IsNullOrEmpty(address)
               || String.IsNullOrEmpty(Gender))
            {
                ViewBag.ErrorMessage = "Vui lòng nhập tất cả thông tin";
            }
            else
            {
                isError = false;
            }

            if (!isError)
            {
                DateTime dtBirthDay = DateTime.Now;
                Constants.Gender enumGender = (Constants.Gender)Int16.Parse(Gender);

                if (DataHelper.AccountHelper.getInstance().signUp(data, email, password, fullname, phone, address, dtBirthDay, enumGender))
                {
                    DataHelper.AccountHelper.getInstance().loginMember(data, email, password);
                    return RedirectToAction("CompleteSignUp");
                }
                else
                {
                    ViewBag.ErrorMessage = "Tài khoản đã tồn tại";
                }
            }

            return View(URLHelper.URL_HOME_SIGNUP);
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View(URLHelper.URL_HOME_SIGNUP);
        }

        public ActionResult CompleteSignUp()
        {
            return View(URLHelper.URL_HOME_COMPLETE_SIGNUP);
        }
        
        [HttpPost]
        public ActionResult ForgotPassword(FormCollection form)
        {
            var email = form["email"];
            if (email != null && !String.IsNullOrEmpty(email.ToString()) && Regex.IsMatch(email.ToString(), "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$"))
            {
                ViewBag.Message = "Chúng tôi đã gửi mật khẩu vào email của ban.";

                string password = DataHelper.AccountHelper.getInstance().getPasswordOfMemberAccount(data, email);
                EmailHelper.getInstance().sendPasswordRecoveryMail(email, password);
            }
            else
            {
                ViewBag.Message = "Vui lòng nhập email đúng định dạng";
            }

            return View(URLHelper.URL_HOME_FORGOT_PASSWORD);
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View(URLHelper.URL_HOME_FORGOT_PASSWORD);
        }

        public ActionResult Logout()
        {
            DataHelper.AccountHelper.getInstance().logoutMember(this);
            return RedirectToAction("Index");
        }
    }
}
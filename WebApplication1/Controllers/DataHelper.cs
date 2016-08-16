using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Controllers
{
    public class DataHelper
    {
        //Model class
        public class ShoppingCardItemModel
        {
            public String name, image;
            public int id, quantity;
            public long price, total;
        }

        //Helper classes
        public class GeneralHelper
        {
            static GeneralHelper instance;
            public static GeneralHelper getInstance()
            {
                if (instance == null)
                {
                    instance = new GeneralHelper();
                }
                return instance;
            }

            public List<Models.tbl_module> getAllSupportedModules(Models.DataClassesDataContext data)
            {
                var result = data.tbl_modules.Where(a => a.type == 1).OrderByDescending(a => a.date_added);
                return result.ToList();
            }

            public List<Models.tbl_module> getHomeModule(Models.DataClassesDataContext data)
            {
                var result = data.tbl_modules.Where(a => a.type == 2).OrderByDescending(a => a.date_added);
                return result.ToList();
            }

            public List<Models.tbl_support> getAllSupporters(Models.DataClassesDataContext data)
            {
                return data.tbl_supports.ToList();
            }

            public double getDefaultUsdRate()
            {
                return 22260000;
            }
        }

        public class AccountHelper
        {
            static AccountHelper instance;
            public static AccountHelper getInstance()
            {
                if (instance == null)
                {
                    instance = new AccountHelper();
                }
                return instance;
            }

            public Models.tbl_member getMemberAccountByEmail(Models.DataClassesDataContext data, string email)
            {
                Models.tbl_member result = data.tbl_members.Where(n => n.email.Equals(email)).Single();
                return result;
            }

            public bool checkThisAdminAccountExist(Models.DataClassesDataContext data, string username, string password)
            {
                var result = data.tbl_admins.Where(a => a.username.Equals(username) && a.password == password);
                if (result.Count() > 0)
                {
                    return true;
                }
                return false;
            }

            public bool checkThisMemberAccountExist(Models.DataClassesDataContext data, string email, string password)
            {
                var result = data.tbl_members.Where(a => a.email.Equals(email) && a.password == password);
                if (result.Count() > 0)
                {
                    return true;
                }
                return false;
            }

            public string getPasswordOfMemberAccount(Models.DataClassesDataContext data, string email)
            {
                var result = data.tbl_members.Where(a => a.email.Equals(email));
                if (result.Count() > 0)
                {
                    return result.Single().password;
                }
                return "";
            }

            public bool loginAdmin(Models.DataClassesDataContext data, string username, string password)
            {
                return checkThisAdminAccountExist(data, username, password);
            }

            public bool loginMember(Models.DataClassesDataContext data, string email, string password)
            {
                return checkThisMemberAccountExist(data, email, password);
            }

            public bool activateMemberAccount(BaseController context, string email)
            {
                Models.tbl_member memberAccount = getMemberAccountByEmail(context.data, email);
                if (memberAccount.status != (int)Constants.AccountStatus.ACTIVE)
                {
                    memberAccount.status = (int)Constants.AccountStatus.ACTIVE;
                    context.updateModel(memberAccount);
                    context.data.SubmitChanges();
                    return true;
                }
                return false;
            }

            public bool signUp(Models.DataClassesDataContext data, string email, string password
                , string fullname, string phone, string address
                , DateTime birthday, Constants.Gender gender)
            {
                bool doesAccountToAddExist = false;
                if (!doesAccountToAddExist)
                {
                    Models.tbl_member account = new Models.tbl_member();
                    account.email = email;
                    account.address = address;
                    account.date_added = DateTime.Now;
                    account.last_modified = DateTime.Now;
                    account.name = fullname;
                    account.password = password;
                    account.status = (byte)Constants.AccountStatus.INACTIVE;
                    account.birthday = birthday;
                    account.gender = (byte)gender;
                    account.phone = phone;

                    data.tbl_members.InsertOnSubmit(account);
                    data.SubmitChanges();

                    //Send email to activate account
                    EmailHelper.getInstance().sendActivatingMail(email);
                    return true;
                }
                return false;
            }

            public bool checkIsAdminLoggingIn(HttpContextBase context)
            {
                Object session = context.Session[Constants.KEY_SESSION_ADMIN_USERNAME];
                if (session != null && !String.IsNullOrEmpty(session.ToString()))
                {
                    return true;
                }
                return false;
            }

            public bool checkIsMemberLoggingIn(HttpContextBase context)
            {
                Object session = context.Session[Constants.KEY_SESSION_MEMBER_USERNAME];
                if (session != null && !String.IsNullOrEmpty(session.ToString()))
                {
                    return true;
                }
                return false;
            }

            public string getLoggingInMemberEmail(HttpContextBase context)
            {
                Object session = context.Session[Constants.KEY_SESSION_MEMBER_USERNAME];
                if (session != null && !String.IsNullOrEmpty(session.ToString()))
                {
                    return session.ToString();
                }
                return "";
            }

            public void logoutAdmin(BaseAdminController context)
            {
                context.Session[Constants.KEY_SESSION_ADMIN_USERNAME] = null;
            }

            public void logoutMember(BaseController context)
            {
                context.Session[Constants.KEY_SESSION_MEMBER_USERNAME] = null;
            }
        }

        public class ProductHelper
        {
            static ProductHelper instance;
            public static ProductHelper getInstance()
            {
                if (instance == null)
                {
                    instance = new ProductHelper();
                }
                return instance;
            }

            public Models.tbl_item getProductById(Models.DataClassesDataContext data, int id)
            {
                Models.tbl_item result = data.tbl_items.Where(n => n.id == id).Single();
                return result;
            }

            public List<Models.tbl_item> getListAllProducts(Models.DataClassesDataContext data)
            {
                return data.tbl_items.OrderByDescending(a => a.date_added).ToList();

            }

            public List<Models.tbl_item> getListOtherProductsByCategory(Models.DataClassesDataContext data, int id, int parent)
            {
                return data.tbl_items.OrderByDescending(a => a.date_added).Where(n => n.parent == parent && n.id != id).ToList();
            }

            public List<Models.tbl_item> getProductHot(Models.DataClassesDataContext data)
            {
                var result = data.tbl_items.Where(a => a.hot == 1).OrderByDescending(a => a.date_added);
                if (result.Count() < 1)
                    return new List<Models.tbl_item>();
                return result.ToList();
            }
        }

        public class NewsHelper
        {
            static NewsHelper instance;
            public static NewsHelper getInstance()
            {
                if (instance == null)
                {
                    instance = new NewsHelper();
                }
                return instance;
            }

            public Models.tbl_new getNewsById(Models.DataClassesDataContext data, int id)
            {
                Models.tbl_new result = data.tbl_news.Where(n => n.id == id).Single();
                return result;
            }

            public Models.tbl_news_category getOneNewsCategory(Models.DataClassesDataContext data, string categoryName)
            {
                var itemCategory = from ic in data.tbl_news_categories
                                   where ic.name.Equals(categoryName)
                                   select ic;
                if (itemCategory == null || !itemCategory.Any())
                {
                    return new Models.tbl_news_category();
                }
                return itemCategory.Single();
            }

            //This method gets all records in tbl_news which has parent name == Constants.NEWS_CATEGORY_NAME_NEWS
            public List<Models.tbl_new> getListAllNews(Models.DataClassesDataContext data)
            {
                Models.tbl_news_category newsCategory = getOneNewsCategory(data, Constants.NEWS_CATEGORY_NAME_NEWS);
                return data.tbl_news.OrderByDescending(a => a.date_added).Where(n => n.parent.Value == newsCategory.id).ToList();
            }

            //This method gets all records in tbl_news which has parent name == Constants.NEWS_CATEGORY_NAME_POLICY
            public List<Models.tbl_new> getListAllPolicy(Models.DataClassesDataContext data)
            {
                Models.tbl_news_category newsCategory = getOneNewsCategory(data, Constants.NEWS_CATEGORY_NAME_POLICY);
                return data.tbl_news.OrderByDescending(a => a.date_added).Where(n => n.parent.Value == newsCategory.id).ToList();
            }

            public List<Models.tbl_new> getListOtherNewsByCategory(Models.DataClassesDataContext data, int id, int parent)
            {
                return data.tbl_news.OrderByDescending(a => a.date_added).Where(n => n.parent == parent && n.id != id).ToList();
            }
        }

        public class ShoppingCardHelper
        {
            static ShoppingCardHelper instance;
            public static ShoppingCardHelper getInstance()
            {
                if (instance == null)
                {
                    instance = new ShoppingCardHelper();
                }
                return instance;
            }

            public void clearShoppingCard(BaseController context)
            {
                context.Session[Constants.KEY_SESSION_SHOPPING_CARD] = new List<Models.tbl_order_detail>();
            }

            public void DeleteItemsFromShoppingCard(BaseController context, int itemId)
            {
                List<Models.tbl_order_detail> shoppingCard = getShoppingCardInSession(context);
                foreach (Models.tbl_order_detail record in shoppingCard.ToList())
                {
                    if (record.id_product == itemId)
                    {
                        shoppingCard.Remove(record);
                    }
                }
            }

            public void saveOrder(BaseController context, string emailSender,
    string nameSender, string addressSender, string phoneSender,
    string emailReceiver, string nameReceiver, string phoneReceiver, string addressReceiver,
    string note, string totalCost, string curency)
            {
                //Save order
                Models.tbl_order order = new Models.tbl_order();
                context.data.tbl_orders.InsertOnSubmit(order);

                //Save order_details
                for (int i = 0; i < 3; i++)
                {
                    Models.tbl_order_detail orderDetail = new Models.tbl_order_detail();
                    context.data.tbl_order_details.InsertOnSubmit(orderDetail);
                }

                context.data.SubmitChanges();
            }

            public void updateShoppingCard(BaseController context, List<Models.tbl_order_detail> shoppingCard)
            {
                context.Session[Constants.KEY_SESSION_SHOPPING_CARD] = shoppingCard;
            }

            public void addItemsToShoppingCard(BaseController context, int itemId, long price, int amount)
            {
                List<Models.tbl_order_detail> shoppingCard = getShoppingCardInSession(context);
                bool doesItemToAddExistInShoppingCard = false;
                foreach (Models.tbl_order_detail record in shoppingCard)
                {
                    if (record.id_product == itemId)
                    {
                        record.quantity = record.quantity + amount;
                        doesItemToAddExistInShoppingCard = true;
                    }
                }
                if (!doesItemToAddExistInShoppingCard)
                {
                    Models.tbl_order_detail recordInShoppingCard = new Models.tbl_order_detail();
                    recordInShoppingCard.id_product = itemId;
                    recordInShoppingCard.price = price;
                    recordInShoppingCard.quantity = amount;
                    shoppingCard.Add(recordInShoppingCard);
                }
            }

            public List<Models.tbl_order_detail> getShoppingCardInSession(BaseController context)
            {
                return getShoppingCardInSessionByHttpContext(context.HttpContext);
            }

            public List<ShoppingCardItemModel> getShoppingCardItemModelsInSession(BaseController context)
            {
                List<Models.tbl_order_detail> listOrderDetails = getShoppingCardInSessionByHttpContext(context.HttpContext);
                List<ShoppingCardItemModel> result = new List<ShoppingCardItemModel>();
                foreach (var orderDetail in listOrderDetails)
                {
                    Models.tbl_item item = ProductHelper.getInstance().getProductById(context.data, orderDetail.id_product.Value);
                    ShoppingCardItemModel model = new ShoppingCardItemModel();
                    model.id = orderDetail.id_product.Value;
                    model.name = item.name;
                    model.image = item.image;
                    model.quantity = orderDetail.quantity.Value;
                    model.price = item.price.HasValue ? item.price.Value : 0;
                    model.total = model.price * model.quantity;
                    result.Add(model);
                }

                return result;
            }

            public List<Models.tbl_order_detail> getShoppingCardInSessionByHttpContext(HttpContextBase context)
            {
                List<Models.tbl_order_detail> shoppingCard;
                Object objShoppingCard = context.Session[Constants.KEY_SESSION_SHOPPING_CARD];
                if (objShoppingCard != null)
                {
                    shoppingCard = (List<Models.tbl_order_detail>)objShoppingCard;
                }
                else
                {
                    shoppingCard = new List<Models.tbl_order_detail>();
                    context.Session[Constants.KEY_SESSION_SHOPPING_CARD] = shoppingCard;
                }

                return shoppingCard;
            }

            public int getItemsAmountInShoppingCard(HttpContextBase context)
            {
                int result = 0;
                List<Models.tbl_order_detail> shoppingCard = getShoppingCardInSessionByHttpContext(context);
                foreach (Models.tbl_order_detail record in shoppingCard)
                {
                    if (record.quantity.HasValue)
                    {
                        result += record.quantity.Value;
                    }
                }

                return result;
            }
        }
    }
}
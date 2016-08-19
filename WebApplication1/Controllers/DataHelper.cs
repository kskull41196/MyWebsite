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
            public int id, quantity, orderid, modelid;
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

            public void deleteAllSupporters(Models.DataClassesDataContext data)
            {
                data.tbl_supports.DeleteAllOnSubmit(data.tbl_supports);
                data.SubmitChanges();
            }

            public void deleteAllModules(Models.DataClassesDataContext data)
            {
                data.tbl_modules.DeleteAllOnSubmit(data.tbl_modules);
                data.SubmitChanges();
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
                return 22260;
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

            public void deleteAllMembers(Models.DataClassesDataContext data)
            {
                ShoppingCardHelper.getInstance().deleteAllOrderDetails(data);

                data.tbl_members.DeleteAllOnSubmit(data.tbl_members);
                data.SubmitChanges();
            }

            public Models.tbl_member getMemberAccountByEmail(Models.DataClassesDataContext data, string email)
            {
                Models.tbl_member result = data.tbl_members.Where(n => n.email.Equals(email)).Single();
                return result;
            }

            public int getMemberAccountAmount(Models.DataClassesDataContext data)
            {
                return data.tbl_members.Count();
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
                return checkThisMemberAccountExist(data, email, password, false);
            }

            public bool checkThisMemberAccountExist(Models.DataClassesDataContext data, string email, string password, bool checkActive)
            {
                var result = data.tbl_members.Where(a => a.email.Equals(email) && a.password == password);
                if (result.Count() <= 0 || (checkActive && result.Single().status != (int)Constants.AccountStatus.ACTIVE))
                {
                    return false;
                }
                return true;
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
                return checkThisMemberAccountExist(data, email, password, true);
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
                bool doesAccountToAddExist = checkThisMemberAccountExist(data, email, password);
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

            public void deleteAllProductCategory(Models.DataClassesDataContext data)
            {
                deleteAllProduct(data);

                data.tbl_item_categories.DeleteAllOnSubmit(data.tbl_item_categories);
                data.SubmitChanges();
            }

            public void deleteAllProduct(Models.DataClassesDataContext data)
            {
                data.tbl_items.DeleteAllOnSubmit(data.tbl_items);
                data.SubmitChanges();
            }

            public int getProductsAmount(Models.DataClassesDataContext data)
            {
                return data.tbl_items.Count();
            }

            public int getProductCategoryAmount(Models.DataClassesDataContext data)
            {
                return data.tbl_item_categories.Count();
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

            public List<Models.tbl_item> getListProductsByCategory(Models.DataClassesDataContext data, int parent)
            {
                return data.tbl_items.OrderByDescending(a => a.date_added).Where(n => n.parent == parent).ToList();
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


            public void deleteAllNewsCategory(Models.DataClassesDataContext data)
            {
                deleteAllNews(data);

                data.tbl_news_categories.DeleteAllOnSubmit(data.tbl_news_categories);
                data.SubmitChanges();
            }

            public void deleteAllNews(Models.DataClassesDataContext data)
            {
                data.tbl_news.DeleteAllOnSubmit(data.tbl_news);
                data.SubmitChanges();

            }
            public int getNewsAmount(Models.DataClassesDataContext data)
            {
                return data.tbl_news.Count();
            }

            public int getNewsCategoryAmount(Models.DataClassesDataContext data)
            {
                return data.tbl_news_categories.Count();
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

            public List<Models.tbl_new> getListNewsByCategory(Models.DataClassesDataContext data, int parent)
            {
                return data.tbl_news.OrderByDescending(a => a.date_added).Where(n => n.parent == parent).ToList();
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

            public void deleteAllOrderDetails(Models.DataClassesDataContext data)
            {
                data.tbl_order_details.DeleteAllOnSubmit(data.tbl_order_details);
                data.SubmitChanges();
            }

            public void deleteAllOrders(Models.DataClassesDataContext data)
            {
                deleteAllOrderDetails(data);

                data.tbl_orders.DeleteAllOnSubmit(data.tbl_orders);
                data.SubmitChanges();

            }
            public int getOrderAmount(Models.DataClassesDataContext data)
            {
                return data.tbl_orders.Count();
            }

            public int getPaidOrderAmount(Models.DataClassesDataContext data)
            {
                return data.tbl_orders.Where(n => n.status == (int)Constants.OrderStatus.PAID).Count();
            }

            public void saveOrder(BaseController context,
    string emailReceiver, string nameReceiver, string phoneReceiver, string addressReceiver,
    string note, string curency)
            {
                //Get member account by session email
                string emailSender = AccountHelper.getInstance().getLoggingInMemberEmail(context.HttpContext);
                Models.tbl_member member = AccountHelper.getInstance().getMemberAccountByEmail(context.data, emailSender);

                //Get shoppingCard in Sesion
                List<Models.tbl_order_detail> listShoppingCard = getShoppingCardInSession(context);
                int totalAmount = 0;
                foreach (Models.tbl_order_detail record in listShoppingCard.ToList())
                {
                    totalAmount += record.quantity.Value;
                }

                //Save order
                Models.tbl_order order = new Models.tbl_order();
                order.id_customer = member.id;
                order.total_amount = totalAmount;
                order.last_modified = DateTime.Now;
                order.date_added = DateTime.Now;
                order.email_receiver = emailReceiver;
                order.email_customer = emailSender;
                order.name_receiver = nameReceiver;
                order.name_customer = member.name;
                order.phone_receiver = phoneReceiver;
                order.phone_customer = member.phone;
                order.address_receiver = addressReceiver;
                order.address_customer = member.address;
                order.note = note;
                order.curency = (byte)(curency.Equals("USD") ? 1 : 0);
                order.status = (int)Constants.OrderStatus.UNPAID;
                context.data.tbl_orders.InsertOnSubmit(order);
                context.data.SubmitChanges(); //Submit change here to get the id of inserted record.

                //Save order_details
                foreach (Models.tbl_order_detail record in listShoppingCard.ToList())
                {
                    record.id_order = order.id;
                    context.data.tbl_order_details.InsertOnSubmit(record);
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

            public List<ShoppingCardItemModel> getListShoppingCardItemModelFromListOrderDetails(Models.DataClassesDataContext data, List<Models.tbl_order_detail> listOrderDetails)
            {
                List<ShoppingCardItemModel> result = new List<ShoppingCardItemModel>();
                foreach (var orderDetail in listOrderDetails)
                {
                    Models.tbl_item item = ProductHelper.getInstance().getProductById(data, orderDetail.id_product.Value);
                    ShoppingCardItemModel model = new ShoppingCardItemModel();
                    model.id = orderDetail.id_product.Value;
                    model.name = item.name;
                    model.image = item.image;
                    model.quantity = orderDetail.quantity.Value;
                    model.price = item.price.HasValue ? item.price.Value : 0;
                    model.total = model.price * model.quantity;
                    model.orderid = orderDetail.id_order.HasValue ? orderDetail.id_order.Value : 0;
                    model.modelid = orderDetail.id;
                    result.Add(model);
                }

                return result;
            }

            public List<ShoppingCardItemModel> getShoppingCardItemModelsInSession(BaseController context)
            {
                List<Models.tbl_order_detail> listOrderDetails = getShoppingCardInSessionByHttpContext(context.HttpContext);
                return getListShoppingCardItemModelFromListOrderDetails(context.data, listOrderDetails);
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
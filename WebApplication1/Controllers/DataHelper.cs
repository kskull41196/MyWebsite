using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Controllers
{
    public class DataHelper
    {
        private static DataHelper instance;
        public static DataHelper getInstance()
        {
            if (instance == null)
            {
                instance = new DataHelper();
            }
            return instance;
        }

        //Getting data from models
        public Models.tbl_item getProductById(Models.DataClassesDataContext data, int id)
        {
            Models.tbl_item result = data.tbl_items.Where(n => n.id == id).Single();
            return result;
        }

        public Models.tbl_new getNewsById(Models.DataClassesDataContext data, int id)
        {
            Models.tbl_new result = data.tbl_news.Where(n => n.id == id).Single();
            return result;
        }

        public List<Models.tbl_item> getListAllProducts(Models.DataClassesDataContext data)
        {
            return data.tbl_items.OrderByDescending(a => a.date_added).ToList();
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

        public List<Models.tbl_support> getAllSupporters(Models.DataClassesDataContext data)
        {
            return data.tbl_supports.ToList();
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
        public bool loginAdmin(Models.DataClassesDataContext data, string username, string password)
        {
            return checkThisAdminAccountExist(data, username, password);
        }
        public bool loginMember(Models.DataClassesDataContext data, string email, string password)
        {
            return checkThisMemberAccountExist(data, email, password);
        }
        
        public void clearShoppingCard(BaseController context)
        {
            context.Session[Constants.KEY_SESSION_SHOPPING_CARD] = new List<Models.tbl_order_detail>();
        }

        public void addItemsToShoppingCard(BaseController context, int itemId, long price, int amount)
        {
            List<Models.tbl_order_detail> shoppingCard = getShoppingCardInSession(context);
            bool doesItemToAddExistInShoppingCard = false;
            foreach (Models.tbl_order_detail record in shoppingCard)
            {
                if(record.id_product == itemId)
                {
                    record.quantity = record.quantity + amount;
                    doesItemToAddExistInShoppingCard = true;
                }
            }
            if(!doesItemToAddExistInShoppingCard)
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
    }
}
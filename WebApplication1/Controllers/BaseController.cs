using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public abstract class BaseController : Controller
    {
        public DataClassesDataContext data = new DataClassesDataContext();

        //TODO, this class help the other classes having accessibility to use method UpdateMode of Controller 
        public void updateModel(Object model)
        {
            UpdateModel(model);
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            ViewData[Constants.KEY_VIEWDATA_LIST_MODULE] = getAllSupportedModules();
            ViewData[Constants.KEY_VIEWDATA_SHOPPING_CARD_ITEMS_AMOUNT] = getItemsAmountInShoppingCard(requestContext.HttpContext);
            ViewData[Constants.KEY_VIEWDATA_IS_LOGIN] = DataHelper.getInstance().checkIsMemberLoggingIn(HttpContext);
        }

        private List<tbl_module> getAllSupportedModules()
        {
            var result = data.tbl_modules.Where(a => a.type == 1).OrderByDescending(a => a.date_added);
            return result.ToList();
        }

        private int getItemsAmountInShoppingCard(HttpContextBase context)
        {
            int result = 0;
            List<tbl_order_detail> shoppingCard = DataHelper.getInstance().getShoppingCardInSessionByHttpContext(context);
            foreach (tbl_order_detail record in shoppingCard)
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
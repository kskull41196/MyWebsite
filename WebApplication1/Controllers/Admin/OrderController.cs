using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;

namespace WebApplication1.Controllers.Admin
{
    public class OrderController : BaseAdminController
    {
        // GET: Order
        public List<tbl_order_detail> getListOrderDetailById(int id)
        {
            return data.tbl_order_details.OrderByDescending(a => a.id).Where(n => n.id_order == id).ToList();
        }

        private List<tbl_order> getOrder(int count)
        {
            return getOrder(count, null);
        }
        private List<tbl_order> getAllOrder()
        {
            return getOrder(-1, null);
        }
        private tbl_order getOneOrder(int id)
        {
            var item = from ic in data.tbl_orders
                       where ic.id == id
                       select ic;
            if (item == null)
            {
                return new tbl_order();
            }
            return item.Single();
        }
        private tbl_order_detail getOneOrderDetail(int id)
        {
            var item = from ic in data.tbl_order_details
                       where ic.id == id
                       select ic;
            if (item == null)
            {
                return new tbl_order_detail();
            }
            return item.Single();
        }

        public ActionResult OrderSetStatusEnable(int id)
        {
            tbl_order tic = getOneOrder(id);
            tic.status = (byte)(tic.status == 1 ? 0 : 1);
            UpdateModel(tic);
            data.SubmitChanges();
            return RedirectToAction("OrderView");
        }
        private List<tbl_order> getOrder(int count, String keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                var result = data.tbl_orders.Where(a => a.email_customer.Contains(keyword)).OrderByDescending(a => a.date_added);
                if (count != -1)
                    result.Take(count);
                return result.ToList();
            }
            else
            {
                var result = data.tbl_orders.OrderByDescending(a => a.date_added);
                if (count != -1)
                    result.Take(count);
                return result.ToList();
            }
        }

        /*
         * 
         * 
         * 
         */
        public ActionResult Index()
        {
            return OrderView();
        }

        [HttpGet]
        public ActionResult OrderDetail(int id)
        {
            var listOrderDetail = getListOrderDetailById(id);
            return View(URLHelper.URL_ADMIN_ORDER_DETAIL, listOrderDetail);
        }

        [HttpPost]
        public ActionResult OrderDetail(int id, String btnDel)
        {
            if (String.IsNullOrEmpty(btnDel))
            {
                //Delete all
                DataHelper.ShoppingCardHelper.getInstance().deleteAllOrderDetails(data);
            }

            var listOrderDetail = getListOrderDetailById(id);
            return View(URLHelper.URL_ADMIN_ORDER_DETAIL, listOrderDetail);
        }

        /*
         * 
         * 
         * 
         */
        [HttpGet]
        public ActionResult OrderView()
        {
            var listOrder = getOrder(10);
            return View(URLHelper.URL_ADMIN_ORDER, listOrder);
        }
        [HttpPost]
        public ActionResult OrderView(FormCollection form, String btnDel)
        {

            if (String.IsNullOrEmpty(btnDel))
            {
                //Delete all
                DataHelper.ShoppingCardHelper.getInstance().deleteAllOrders(data);

            }
            var keyword = form["keyword"];
            var listOrder = getOrder(10, keyword);
            return View(URLHelper.URL_ADMIN_ORDER, listOrder);
        }

        /*
         * 
         * 
         * 
         */
        public ActionResult OrderDelete(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                ViewBag.AlertError = "Không xoá được!";
            }
            else
            {
                try
                {
                    data.tbl_orders.DeleteOnSubmit(getOneOrder(Int32.Parse(id)));
                    data.SubmitChanges();
                    ViewBag.AlertSuccess = "Xoá thành công!";
                }
                catch (Exception e)
                {
                    ViewBag.AlertError = "Không xoá được";
                }
            }
            return OrderView();
        }

        public ActionResult OrderDetailDelete(String id, int idorder)
        {
            if (String.IsNullOrEmpty(id))
            {
                ViewBag.AlertError = "Không xoá được!";
            }
            else
            {
                try
                {
                    data.tbl_order_details.DeleteOnSubmit(getOneOrderDetail(Int32.Parse(id)));
                    data.SubmitChanges();
                    ViewBag.AlertSuccess = "Xoá thành công!";
                }
                catch (Exception e)
                {
                    ViewBag.AlertError = "Không xoá được";
                }
            }
            return OrderDetail(idorder);
        }
    }
}
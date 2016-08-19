using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;

namespace WebApplication1.Controllers.Admin
{
    public class SupportController : BaseAdminController
    {
        // GET: Supporters
        private List<tbl_support> getSupport(int count)
        {
            return getSupport(count, "");
        }
        private List<tbl_support> getAllSupports()
        {
            return getSupport(-1, "");
        }
        private List<tbl_support> getSupport(int count, String keyword)
        {
            var result = data.tbl_supports;
            if (!String.IsNullOrEmpty(keyword))
            {
                result.Where(a => a.name.Contains(keyword));
                if (count != -1)
                    result.Take(count);
                return result.ToList();
            }
            else
            {
                if (count != -1)
                    result.Take(count);
                return result.ToList();
            }
        }
        private tbl_support getOneSupport(int id)
        {
            var models = from ic in data.tbl_supports
                         where ic.id == id
                         select ic;
            if (models == null)
            {
                return new tbl_support();
            }
            return models.Single();
        }
        /*
         * 
         * 
         * 
         */
        public ActionResult Index()
        {
            return supportView();
        }
        /*
         * 
         * 
         * 
         */
        [HttpGet]
        public ActionResult supportView()
        {
            var listSupport = getSupport(10);
            return View(URLHelper.URL_ADMIN_SUPPORT, listSupport);
        }
        [HttpPost]
        public ActionResult supportView(FormCollection form, String btnDel)
        {
            if (btnDel != null)
            {
                //Delete all
                DataHelper.GeneralHelper.getInstance().deleteAllSupporters(data);
            }

            var keyword = form["keyword"];
            var listSupport = getSupport(10, keyword);
            return View(URLHelper.URL_ADMIN_SUPPORT, listSupport);
        }
        /*
         * 
         * 
         * 
         */
        [HttpGet]
        public ActionResult supportCreate()
        {
            return View(URLHelper.URL_ADMIN_SUPPORT_M, new tbl_support());
        }
        [HttpPost]
        public ActionResult supportCreate(FormCollection form, HttpPostedFileBase fileUpload)
        {
            tbl_support tic = new tbl_support();
            var name = form["name"];
            var yahoo = form["yahoo"];
            var skype = form["skype"];
            var phone = form["phone"];
            bool err = false;
            if (String.IsNullOrEmpty(name))
            {
                err = true;
                ViewData["Error"] += "Vui lòng nhập tên hỗ trợ viên!\n";
            }
            tic.name = name;
            tic.skype = skype;
            tic.yahoo = yahoo;
            tic.phone = phone;
            if (fileUpload != null)
            {
                var fileName = Path.GetFileName(DateTime.Now.Millisecond + fileUpload.FileName);
                var path = Path.Combine(Server.MapPath(URLHelper.URL_IMAGE_PATH + "support/"), fileName);
                if (!System.IO.File.Exists(path))
                {
                    fileUpload.SaveAs(path);
                }
                tic.image = fileName;
            }
            if (err == false)
            {
                data.tbl_supports.InsertOnSubmit(tic);
                data.SubmitChanges();
                return RedirectToAction("supportView");
            }
            else
            {
                return View(URLHelper.URL_ADMIN_SUPPORT_M, tic);
            }
        }
        /*
         *
         *
         * 
         */
        [HttpGet]
        public ActionResult supportEdit(String id)
        {
            return View(URLHelper.URL_ADMIN_SUPPORT_M, getOneSupport(Int32.Parse(id)));
        }
        [HttpPost]
        public ActionResult supportEdit(FormCollection form, HttpPostedFileBase fileUpload)
        {
            var id = form["id"];
            if (id == null)
            {
                return supportCreate(form, fileUpload);
            }
            else
            {
                tbl_support tic = getOneSupport(Int32.Parse(id));
                var name = form["name"];
                var yahoo = form["yahoo"];
                var skype = form["skype"];
                var phone = form["phone"];
                bool err = false;
                if (String.IsNullOrEmpty(name))
                {
                    err = true;
                    ViewData["Error"] += "Vui lòng nhập tên hỗ trợ viên!\n";
                }
                tic.name = name;
                tic.yahoo = yahoo;
                tic.skype = skype;
                tic.phone = phone;
                if (form["chkClearImg"] != null)
                {
                    tic.image = "";
                }
                else if (fileUpload != null)
                {
                    var fileName = Path.GetFileName(DateTime.Now.Millisecond + fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath(URLHelper.URL_IMAGE_PATH + "support/"), fileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fileUpload.SaveAs(path);
                    }
                    String imageOlder = URLHelper.URL_IMAGE_PATH + tic.image;
                    if (System.IO.File.Exists(imageOlder))
                    {
                        System.IO.File.Delete(imageOlder);
                    }
                    tic.image = fileName;
                }
                if (err == false)
                {
                    UpdateModel(tic);
                    data.SubmitChanges();
                    return RedirectToAction("supportView");
                }
                else
                {
                    return View(URLHelper.URL_ADMIN_SUPPORT_M, tic);
                }
            }
        }
        /*
         * 
         * 
         * 
         */
        public ActionResult supportDelete(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                ViewBag.AlertError = "Không xoá được!";
            }
            else
            {
                try
                {
                    data.tbl_supports.DeleteOnSubmit(getOneSupport(Int32.Parse(id)));
                    data.SubmitChanges();
                    ViewBag.AlertSuccess = "Xoá thành công!";
                }
                catch (Exception e)
                {
                    ViewBag.AlertError = "Không xoá được";
                }
            }
            return supportView();
        }
    }
}
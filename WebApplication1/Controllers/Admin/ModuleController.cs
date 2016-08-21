using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Admin
{
    public class ModuleController : BaseAdminController
    {
        // GET: NewsCategory
        private List<tbl_module> getModule(int count)
        {
            return getModule(count, "");
        }
        private List<tbl_module> getAllModule()
        {
            return getModule(-1, "");
        }
        private List<tbl_module> getModule(int count, String keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                var result = data.tbl_modules.Where(a => a.name_visible.Contains(keyword)).OrderByDescending(a => a.date_added);
                if (count != -1)
                    result.Take(count);
                return result.ToList();
            }
            else
            {
                var result = data.tbl_modules.OrderByDescending(a => a.date_added);
                if (count != -1)
                    result.Take(count);
                return result.ToList();
            }
        }
        private tbl_module getOneModule(int id)
        {
            var module = from ic in data.tbl_modules
                         where ic.id == id
                         select ic;
            if (module == null)
            {
                return new tbl_module();
            }
            return module.Single();
        }
        /*
         * 
         * 
         * 
         */
        public ActionResult Index()
        {
            return moduleView();
        }

        public ActionResult moduleSetStatusEnable(int id)
        {
            tbl_module tic = getOneModule(id);
            if (tic.name_visible.Contains("1996"))
            {
                tic.name_visible = tic.name_visible.Replace("1996", "1994");
            }
            else if (tic.name_visible.Contains("1994"))
            {
                tic.name_visible = tic.name_visible.Replace("1994", "1996");
            }
            tic.last_modified = DateTime.Now;
            UpdateModel(tic);
            data.SubmitChanges();
            return RedirectToAction("moduleView");
        }

        /*
         * 
         * 
         * 
         */
        [HttpGet]
        public ActionResult moduleView()
        {
            var listModule = getModule(10);
            return View(URLHelper.URL_ADMIN_MODULE, listModule);
        }
        [HttpPost]
        public ActionResult moduleView(FormCollection form, String btnDel)
        {
            if (btnDel != null)
            {
                //Delete all
                DataHelper.GeneralHelper.getInstance().deleteAllModules(data);
            }

            var keyword = form["keyword"];
            var listModule = getModule(10, keyword);
            return View(URLHelper.URL_ADMIN_MODULE, listModule);
        }
        /*
         * 
         * 
         * 
         */
        [HttpGet]
        public ActionResult moduleCreate()
        {
            return View(URLHelper.URL_ADMIN_MODULE_M, new Tuple<tbl_module, List<tbl_module>>(new tbl_module(), getAllModule()));
        }
        [HttpPost]
        public ActionResult moduleCreate(FormCollection form)
        {
            tbl_module m = new tbl_module();
            var name_visible = form["name_visible"];
            var name_partial = form["name_partial"];
            var type = form["type"];
            bool err = false;
            if (String.IsNullOrEmpty(name_visible))
            {
                err = true;
                ViewData["Error"] += "Vui lòng nhập tên module!\n";
            }
            if (String.IsNullOrEmpty(name_partial))
            {
                err = true;
                ViewData["Error"] += "Vui lòng nhập tên Partial!\n";
            }
            if (type == null)
                m.type = 1;
            else
                m.type = Int32.Parse(type);
            m.name_partial = name_partial;
            m.name_visible = name_visible + "_1994";
            m.date_added = DateTime.Now;
            m.last_modified = DateTime.Now;
            if (err == false)
            {
                data.tbl_modules.InsertOnSubmit(m);
                data.SubmitChanges();
                return RedirectToAction("moduleView");
            }
            else
            {
                return View(URLHelper.URL_ADMIN_MODULE_M, new Tuple<tbl_module, List<tbl_module>>(m, getAllModule()));
            }
        }
        /*
         *
         *
         * 
         */
        [HttpGet]
        public ActionResult moduleEdit(String id)
        {
            return View(URLHelper.URL_ADMIN_MODULE_M, new Tuple<tbl_module, List<tbl_module>>(getOneModule(Int32.Parse(id)), getAllModule()));
        }
        [HttpPost]
        public ActionResult moduleEdit(FormCollection form)
        {
            var id = form["id"];
            if (id == null)
            {
                return moduleCreate(form);
            }
            else
            {
                tbl_module m = getOneModule(Int32.Parse(id));
                var name_visible = form["name_visible"];
                var name_partial = form["name_partial"];
                var type = form["type"];
                bool err = false;
                if (String.IsNullOrEmpty(name_visible))
                {
                    err = true;
                    ViewData["Error"] += "Vui lòng nhập tên module!\n";
                }
                if (String.IsNullOrEmpty(name_partial))
                {
                    err = true;
                    ViewData["Error"] += "Vui lòng nhập tên Partial!\n";
                }
                if (!name_visible.Contains("_1994") || !name_visible.Contains("_1996"))
                {
                    name_visible = name_visible + "_1994";
                }
                m.name_visible = name_visible;
                m.name_partial = name_partial;
                if (type == null)
                    m.type = 1;
                else
                    m.type = Int32.Parse(type);
                m.date_added = DateTime.Now;
                if (err == false)
                {
                    UpdateModel(m);
                    data.SubmitChanges();
                    return RedirectToAction("moduleView");
                }
                else
                {
                    return View(URLHelper.URL_ADMIN_NEWS_CATEGORY_M, new Tuple<tbl_module, List<tbl_module>>(m, getAllModule()));
                }
            }
        }
        /*
         * 
         * 
         * 
         */
        public ActionResult moduleDelete(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                ViewBag.AlertError = "Không xoá được!";
            }
            else
            {
                try
                {
                    data.tbl_modules.DeleteOnSubmit(getOneModule(Int32.Parse(id)));
                    data.SubmitChanges();
                    ViewBag.AlertSuccess = "Xoá thành công!";
                }
                catch (Exception e)
                {
                    ViewBag.AlertError = "Không xoá được";
                }
            }
            return moduleView();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;

namespace WebApplication1.Controllers.Admin
{
    public class SlideController : BaseAdminController
    {
        // GET: Image
        private List<tbl_image> getAllImage()
        {
            return getImage(-1);
        }
        private List<tbl_image> getImage(int count)
        {
            var result = data.tbl_images.OrderByDescending(a => a.date_added);
            if (count != -1)
                result.Take(count);
            return result.ToList();
        }
        private tbl_image getOneImage(int id)
        {
            var image = from ic in data.tbl_images
                        where ic.id == id
                        select ic;
            if (image == null)
            {
                return new tbl_image();
            }
            return image.Single();
        }
        /*
         * 
         * 
         * 
         */
        public ActionResult Index()
        {
            return imageView();
        }


        /*
         * 
         * 
         * 
         */
        [HttpGet]
        public ActionResult imageView()
        {
            var listImage = getImage(50);
            return View(URLHelper.URL_ADMIN_IMAGE, listImage);
        }
        [HttpPost]
        public ActionResult imageView(FormCollection form, HttpPostedFileBase fileUpload, String btnDel)
        {

            if (btnDel != null)
            {
                //Delete all
                DataHelper.GeneralHelper.getInstance().deleteAllImages(data);
            }
            else if (fileUpload != null)
            {
                tbl_image tic = new tbl_image();
                var fileName = Path.GetFileName(DateTime.Now.Millisecond + fileUpload.FileName);
                var path = Path.Combine(Server.MapPath(URLHelper.URL_IMAGE_PATH + "slide/"), fileName);
                if (!System.IO.File.Exists(path))
                {
                    fileUpload.SaveAs(path);
                }
                tic.image = fileName;
                tic.name = fileName;
                data.tbl_images.InsertOnSubmit(tic);
                data.SubmitChanges();

            }

            var listImage = getImage(10);
            return View(URLHelper.URL_ADMIN_IMAGE, listImage);
        }


        public ActionResult imageDelete(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                ViewBag.AlertError = "Không xoá được!";
            }
            else
            {
                try
                {
                    data.tbl_images.DeleteOnSubmit(getOneImage(Int32.Parse(id)));
                    data.SubmitChanges();
                    ViewBag.AlertSuccess = "Xoá thành công!";
                }
                catch (Exception e)
                {
                    ViewBag.AlertError = "Không xoá được";
                }
            }
            return imageView();
        }
    }
}
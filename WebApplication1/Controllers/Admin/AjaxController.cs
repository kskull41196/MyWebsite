using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;

namespace WebApplication1.Controllers.Admin
{
    public class AjaxController : BaseAdminController
    {
        public Object ToolAjax() {
            return true;
        }
        [HttpPost]
        public ActionResult ToolAjax(WebApplication1.Models.DataAjax data)
        {
            System.Threading.Thread.Sleep(2000);  /*simulating slow connection*/
            /*Do something with object person*/
            if (data.cmd == "BTN_ACTIVE_STAT")
            {
                if (data.table != null && data.field != null && data.item != null)
                {

                }
            }

            return Json(new { msg = "Successfully added " + data.field });
        }
    }
}
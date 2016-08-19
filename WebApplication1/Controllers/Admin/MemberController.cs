using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;

namespace WebApplication1.Controllers.Admin
{
    public class MemberController : BaseAdminController
    {
        // GET: Member
        private List<tbl_member> getMember(int count)
        {
            return getMember(count, "");
        }
        private List<tbl_member> getAllMember()
        {
            return getMember(-1, "");
        }
        private List<tbl_member> getMember(int count, String keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                var result = data.tbl_members.Where(a => a.email.Contains(keyword)).OrderByDescending(a => a.date_added);
                if (count != -1)
                    result.Take(count);
                return result.ToList();
            }
            else
            {
                var result = data.tbl_members.OrderByDescending(a => a.date_added);
                if (count != -1)
                    result.Take(count);
                return result.ToList();
            }
        }
        private tbl_member getOneMember(int id)
        {
            var Member = from ic in data.tbl_members
                         where ic.id == id
                               select ic;
            if (Member == null)
            {
                return new tbl_member();
            }
            return Member.Single();
        }
        /*
         * 
         * 
         * 
         */
        public ActionResult Index()
        {
            return MemberView();
        }
        /*
         * 
         * 
         * 
         */
        [HttpGet]
        public ActionResult MemberView()
        {
            var listMember = getMember(10);
            return View(URLHelper.URL_ADMIN_MEMBER, listMember);
        }
        [HttpPost]
        public ActionResult MemberView(FormCollection form, String btnDel)
        {
            if (btnDel != null)
            {
                //Delete all
                DataHelper.AccountHelper.getInstance().deleteAllMembers(data);
            }

            var keyword = form["keyword"];
            var listMember = getMember(10, keyword);
            return View(URLHelper.URL_ADMIN_MEMBER, listMember);
        }
    
        public ActionResult MemberDelete(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                ViewBag.AlertError = "Không xoá được!";
            }
            else
            {
                try
                {
                    data.tbl_members.DeleteOnSubmit(getOneMember(Int32.Parse(id)));
                    data.SubmitChanges();
                    ViewBag.AlertSuccess = "Xoá thành công!";
                }
                catch (Exception e)
                {
                    ViewBag.AlertError = "Không xoá được";
                }
            }
            return MemberView();
        }
    }
}
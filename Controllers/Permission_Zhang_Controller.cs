using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace SXXM_ZhangNaWei_01.Controllers
{
    public class Permission_Zhang_Controller : Controller
    {
        PermissionEntities perEntities = new PermissionEntities();
        // GET: Permission_Zhang_
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Succeed(Permission per)
        {
            PermissionEntities perEntities = new PermissionEntities();
            perEntities.Permission.Add(per);
            perEntities.SaveChanges();
            return Redirect("/Permission_Zhang_/List");
            //return View();
        }

        private int GetCount()
        {

            int counts = perEntities.Permission.Count();
            return Convert.ToInt32(counts);
           
        }

        public ActionResult List(int p = 1)
        {
            PermissionEntities perEntities = new PermissionEntities();
            int pagesize = 10;
            int count = GetCount();
            string pagerInfo = Pager.CreatePageNums(count, pagesize, p);
            ViewBag.pager = pagerInfo;
            //第二页数据
            var pagerList = perEntities.Permission.OrderBy(x => x.ID).Skip((p - 1) * pagesize).Take(10).ToList();

            return View(pagerList);
        }

        public ActionResult Delete(Permission per)
        {

            PermissionEntities perEntities = new PermissionEntities();

            System.Data.Entity.Infrastructure.DbEntityEntry entityEntry = perEntities.Entry(per);
            entityEntry.State = System.Data.Entity.EntityState.Deleted;
            perEntities.SaveChanges();

            return Redirect("/Permission_Zhang_/List");
        }

        [HttpGet]
        public ActionResult Updata(int ID)
        {
            PermissionEntities perEntities = new PermissionEntities();
            var list = perEntities.Permission.Where(x => x.ID == ID).First();

            return View(list);
        }

        [HttpPost]
        public ActionResult Updata(Permission per)
        {
            if (per.Name != "")
            {
                //声明数据库上下文类
                PermissionEntities perEntities = new PermissionEntities();
                System.Data.Entity.Infrastructure.DbEntityEntry entityEntry = perEntities.Entry(per);
                entityEntry.State = System.Data.Entity.EntityState.Modified;
                perEntities.SaveChanges();
            }
            else
            {
                return Content("名称为空");
            }


            return Redirect("/Permission_Zhang_/List");
        }
    }
}
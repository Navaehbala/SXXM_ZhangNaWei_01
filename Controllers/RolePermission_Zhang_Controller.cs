using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SXXM_ZhangNaWei_01.Controllers
{
    public class RolePermission_Zhang_Controller : Controller
    {
        PermissionEntities perEntities = new PermissionEntities();
        public static int sId = 0;
        // GET: RolePermission_Zhang_
        public ActionResult Index()
        {
            List<Permission> permissionlist = perEntities.Permission.ToList();
            List<RoleTable> rolelist = perEntities.RoleTable.ToList();
            ViewBag.perList = permissionlist;
            ViewBag.poList = rolelist;
            return View();
        }


        public ActionResult Succeed(RolePermission pope)
        {
            PermissionEntities perEntities = new PermissionEntities();
            perEntities.RolePermission.Add(pope);
            perEntities.SaveChanges();
            return Redirect("/RolePermission_Zhang_/List");
        }

        private int GetCount()
        {

            int counts = perEntities.RolePermission.Count();
            return Convert.ToInt32(counts);

        }

        public ActionResult List(int p = 1)
        {
            PermissionEntities perEntities = new PermissionEntities();
            int pagesize = 10;
            int count = GetCount();
            string pagerInfo = Pager3.CreatePageNums(count, pagesize, p);
            ViewBag.pager = pagerInfo;

            //第二页数据
            // var pagerList = perEntities.RolePermission.OrderBy(x => x.ID).Skip((p - 1) * pagesize).Take(10).ToList();

            var query = perEntities.RolePermission.Join(perEntities.RoleTable,
                rp=>rp.RoleID,
                r=>r.ID,
                (rp,r)=>new { 
                    rp.ID,
                    RoleName=r.Name,
                    rp.PermissionID
                });

            var list=query.Join(perEntities.Permission,rp=>rp.PermissionID,g=>g.ID,(rp,g)=>new ViewModels.RolePermissionView()
                {
                    ID=rp.ID,
                    RoleName=rp.RoleName,
                    PermissionName=g.Name

                 }).OrderBy(x => x.ID).Skip((p - 1) * pagesize).Take(10).ToList();

            return View(list);
        }

        public ActionResult Delete(RolePermission pope)
        {

            PermissionEntities perEntities = new PermissionEntities();

            System.Data.Entity.Infrastructure.DbEntityEntry entityEntry = perEntities.Entry(pope);
            entityEntry.State = System.Data.Entity.EntityState.Deleted;
            perEntities.SaveChanges();

            return Redirect("/RolePermission_Zhang_/List");
        }

        [HttpGet]
        public ActionResult Updata(int ID)
        {
            PermissionEntities perEntities = new PermissionEntities();
            var list = perEntities.RolePermission.Where(x => x.ID == ID).First();
            List<Permission> permissionlist = perEntities.Permission.ToList();
            List<RoleTable> rolelist = perEntities.RoleTable.ToList();
            ViewBag.perList = permissionlist;
            ViewBag.poList = rolelist;
            sId = ID;
            return View(list);
        }

        [HttpPost]
        public ActionResult Updata(RolePermission pope)
        {
            if (pope.RoleID != null || pope.PermissionID!=null)
            {
                pope.ID = sId;
                //声明数据库上下文类
                PermissionEntities perEntities = new PermissionEntities();
                System.Data.Entity.Infrastructure.DbEntityEntry entityEntry = perEntities.Entry(pope);
                entityEntry.State = System.Data.Entity.EntityState.Modified;
                perEntities.SaveChanges();
            }
            else
            {
                return Content("名称为空");
            }


            return Redirect("/RolePermission_Zhang_/List");
        }

    }
}
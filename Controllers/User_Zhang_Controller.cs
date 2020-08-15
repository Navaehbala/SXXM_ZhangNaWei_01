using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SXXM_ZhangNaWei_01.Controllers
{
    public class User_Zhang_Controller : Controller
    {
        PermissionEntities perEntities = new PermissionEntities();
        public static int sId=0;
        // GET: User_Zhang_
        public ActionResult Index()
        {
            List<RoleTable> rolelist = perEntities.RoleTable.ToList();
            ViewBag.poList = rolelist;
            return View();
        }
        public ActionResult Succeed(User user)
        {
            PermissionEntities perEntities = new PermissionEntities();
            perEntities.User.Add(user);
            perEntities.SaveChanges();
            return Redirect("/User_Zhang_/List");
        }

        private int GetCount()
        {

            int counts = perEntities.User.Count();
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
            //var pagerList = perEntities.User.OrderBy(x => x.ID).Skip((p - 1) * pagesize).Take(10).ToList();
            var query = perEntities.User.Join(perEntities.RoleTable,
                rp => rp.RoleID,
                r => r.ID,
                (rp, r) => new ViewModels.RoleUser{
                    ID=rp.ID,
                    RoleName = r.Name,
                    UserName=rp.Name,
                    UserPwd=rp.Pwd,
                    UserSex=rp.Sex,
                    UserDepartment=rp.Department,
                    UserPhone=rp.Phone
                }).OrderBy(x => x.ID).Skip((p - 1) * pagesize).Take(10).ToList();


            return View(query);
        }

        public ActionResult Delete(User user)
        {

            PermissionEntities perEntities = new PermissionEntities();

            System.Data.Entity.Infrastructure.DbEntityEntry entityEntry = perEntities.Entry(user);
            entityEntry.State = System.Data.Entity.EntityState.Deleted;
            perEntities.SaveChanges();

            return Redirect("/User_Zhang_/List");
        }

        [HttpGet]
        public ActionResult Updata(int ID)
        {
            PermissionEntities perEntities = new PermissionEntities();
            var list = perEntities.User.Where(x => x.ID == ID).First();
            List<RoleTable> rolelist = perEntities.RoleTable.ToList();
            ViewBag.poList = rolelist;
            sId = ID;
            return View(list);
        }

        [HttpPost]
        public ActionResult Updata(User user)
        {
            if (user.Name != "")
            {
                user.ID = sId;
                //声明数据库上下文类
                PermissionEntities perEntities = new PermissionEntities();
                System.Data.Entity.Infrastructure.DbEntityEntry entityEntry = perEntities.Entry(user);
                entityEntry.State = System.Data.Entity.EntityState.Modified;
                perEntities.SaveChanges();
            }
            else
            {
                return Content("名称为空");
            }


            return Redirect("/User_Zhang_/List");
        }
    }
}
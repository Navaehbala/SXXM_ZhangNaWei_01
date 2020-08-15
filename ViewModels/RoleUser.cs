using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SXXM_ZhangNaWei_01.ViewModels
{
    public class RoleUser
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string UserSex { get; set; }
        public string UserDepartment { get; set; }
        public string UserPhone { get; set; }
    }
}
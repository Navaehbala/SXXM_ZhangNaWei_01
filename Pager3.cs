using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SXXM_ZhangNaWei_01
{
    public class Pager3
    {
        public static string CreatePageNums(int count, int pagesize, int p)
        {


            int totolpage = (int)Math.Ceiling((double)count / pagesize);

            int startPage = 0;
            int endPage = 0;
            if (totolpage <= 10)
            {
                startPage = 1;
                endPage = totolpage;
            }
            else
            {
                if (p <= 6)
                {
                    startPage = 1;
                    endPage = 10;
                }
                else
                {
                    startPage = p - 5;
                    endPage = p + 4;
                    if (endPage >= totolpage)
                    {
                        endPage = totolpage;
                    }
                }

            }
            string pageNums = "";
            for (int i = startPage; i <= endPage; i++)
            {

                if (i == p)
                {
                    //<li class="active"><a href="#">1 <span class="sr-only">(current)</span></a></li>
                    pageNums += $" <li class='active'><a href='/RolePermission_Zhang_/List?p={i}'>{i}<span class='sr-only'>(current)</span></a></li>";
                }
                else
                {
                    pageNums += $" <li><a href='/RolePermission_Zhang_/List?p={i}'>{i}</a></li>";
                }
            }


            string prve = p == 1 ? "" : $"<li><a aria-label='Previous' href='/RolePermission_Zhang_/List?p={p - 1}'><span aria-hidden='true'>«</span></a></li>";
            string next = p == totolpage ? "" : $"<li><a aria-label='Next' href='/RolePermission_Zhang_/List?p={p + 1}'><span aria-hidden='true'>»</span></a></li>";

            string pagerInfo = prve + pageNums + next;

            return pagerInfo;
        }
    }
}
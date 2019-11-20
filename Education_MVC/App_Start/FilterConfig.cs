using Education_MVC.App_Start;
using System.Web;
using System.Web.Mvc;

namespace Education_MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomErrorHandler());
        }
    }
}

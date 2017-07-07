using System.Web.Mvc;
using PayU.Client.Filters;

namespace PayU.Client
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomAuthorize());
        }
    }
}
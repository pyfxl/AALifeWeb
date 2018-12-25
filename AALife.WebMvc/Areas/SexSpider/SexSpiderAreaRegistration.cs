using System.Web.Mvc;

namespace AALife.WebMvc.Areas.SexSpider
{
    public class SexSpiderAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SexSpider";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SexSpider_default",
                "SexSpider/{controller}/{action}/{id}",
                new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
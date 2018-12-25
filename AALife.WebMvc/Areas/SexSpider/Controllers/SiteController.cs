using SexSpider.Core.Helper;
using SexSpider.Core.Models;
using SexSpider.Core.Services;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.SexSpider.Controllers
{
    public class SiteController : Controller
    {
        private readonly SiteService service = new SiteService();

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> List(int id)
        {
            var site = await service.Get(id);
            return View(site);
        }

        public async Task<ActionResult> Image(int id, string url)
        {
            var decUrl = System.Net.WebUtility.UrlDecode(url);
            var site = await service.Get(id);
            var list = new List<ImageModel>();
            if (site.PageLevel == 0)
            {
                list = SiteHelper.GetListImage(site, decUrl).ToList();
            }
            else
            {
                list = SiteHelper.GetListImagePage(site, decUrl).ToList();
            }

            return View(list);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using SexSpider.Core.Helper;
using SexSpider.Core.Models;
using SexSpider.Core.Services;

namespace AALife.WebMvc.Areas.SexSpider.Controllers
{
    [RoutePrefix("api/sexspider/imageapi")]
    public class ImageApiController : ApiController
    {
        private readonly SiteService service = new SiteService();

        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id, string url)
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

            return new JsonLowercase(list, Request);
        }
    }
}
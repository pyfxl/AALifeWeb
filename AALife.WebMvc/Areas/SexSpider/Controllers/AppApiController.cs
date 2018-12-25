using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using SexSpider.Core.Models;
using SexSpider.Core.Services;

namespace AALife.WebMvc.Areas.SexSpider.Controllers
{
    [RoutePrefix("api/sexspider/appapi")]
    public class AppApiController : ApiController
    {
        private readonly SiteService service = new SiteService();

        [HttpGet]
        public async Task<IHttpActionResult> GetList()
        {
            var list = await service.Get();

            var model = new SexSpiderModel();
            model.site_list = list.ToList();
            model.ext_dic = service.GetExtDic();
            model.stop_dic = service.GetStopDic();
            model.del_dic = service.GetDelDic();

            return Ok(model);
        }

    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using SexSpider.Core.Models;
using SexSpider.Core.Services;

namespace AALife.WebMvc.Areas.SexSpider.Controllers
{
    [RoutePrefix("api/sexspider/siteapi")]
    public class SiteApiController : ApiController
    {
        private readonly SiteService service = new SiteService();

        [HttpGet]
        public async Task<IHttpActionResult> Get() 
        {
            var list = await service.Get();
            return new JsonLowercase(list, Request);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(IEnumerable<SexSpiders> models)
        {
            await service.Create(models);
            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(IEnumerable<SexSpiders> models)
        {
            await service.Delete(models);
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(IEnumerable<SexSpiders> models)
        {
            await service.Update(models);
            return Ok();
        }

    }
}
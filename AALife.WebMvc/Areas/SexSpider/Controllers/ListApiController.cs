using SexSpider.Core.Helper;
using SexSpider.Core.Models;
using SexSpider.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace AALife.WebMvc.Areas.SexSpider.Controllers
{
    [RoutePrefix("api/sexspider/listapi")]
    public class ListApiController : ApiController
    {
        private readonly SiteService service = new SiteService();

        [Route("{id}/{page}")]
        public async Task<IHttpActionResult> Get(int id, int page) 
        {
            var site = await service.Get(id);
            string siteLink = site.SiteLink; // bakup sitelink
            SetNextPage(site, page);
            var list = SiteHelper.GetSiteList(site);            
            var result = list.ToList();

            site.SiteLink = siteLink;
            await SaveLastStart(result, site);

            return Ok(result);
        }
            
        /// <summary>
        /// 设置下一页url
        /// </summary>
        private void SetNextPage(SexSpiders site, int page)
        {
            if (page > 1)
            {
                string link = site.SiteLink.Substring(0, site.SiteLink.LastIndexOf("/") + 1);
                if (site.LastStart != null && site.LastStart != "") 
                {
                    site.SiteLink = link + site.ListPage.Replace("(*)", page.ToString()).Replace("(%)", site.LastStart);
                } 
                else 
                {
                    site.SiteLink = link + site.ListPage.Replace("(*)", page.ToString());
                }
            }
        }

        /// <summary>
        /// 某些json类型，保存下一页标记
        /// </summary>
        private async Task SaveLastStart(List<ListModel> list, SexSpiders site) 
        {
            if (!list.Any()) return;
            string lastStart = list.LastOrDefault().LastStart;
            if (lastStart != "") 
            {
                site.LastStart = lastStart;
                await service.Update(site);
            }
        }
    }
}
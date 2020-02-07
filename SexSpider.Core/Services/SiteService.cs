using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using SexSpider.Core.Models;

namespace SexSpider.Core.Services
{
    public class SiteService
    {
        private readonly DataContext db = new DataContext();

        public SiteService()
        {
        }

        /// <summary>
        /// 查
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SexSpiders>> Get() 
        {
            var list = await db.SexSpiders.OrderBy(a => a.SiteRank).ToListAsync();
            return list;
        }

        /// <summary>
        /// 查id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SexSpiders> Get(int id)
        {
            var item = await db.SexSpiders.FindAsync(id);
            return item;
        }

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task Create(IEnumerable<SexSpiders> models)
        {
            db.SexSpiders.AddRange(models);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task Update(IEnumerable<SexSpiders> models)
        {
            await Task.Run(()=> 
            {
                models.ToList().ForEach(model =>
                {
                    Update(model);
                });
            });

            await Task.WhenAll();
        }

        /// <summary>
        /// 改model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SexSpiders model)
        {
            db.SexSpiders.Attach(model);
            DbEntityEntry<SexSpiders> entry = db.Entry(model);
            entry.State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task Delete(IEnumerable<SexSpiders> models)
        {
            db.SexSpiders.RemoveRange(models);
            await db.SaveChangesAsync();
        }
        
        /// <summary>
        /// 取外部字典
        /// </summary>
        /// <returns></returns>
        public List<string> GetExtDic()
        {
            string[] dics = "内射,內射,车震,大屁股,人妻,美臀,肥臀,翘臀,换妻,后入,後入,豐滿,水多,炮友,網友,白漿,闺蜜,陰道,少婦,熟婦,賓館,鮑魚,極品,鄰居".Split(',');

            return dics.ToList();
        }

        /// <summary>
        /// 取停止词典
        /// </summary>
        /// <returns></returns>
        public List<string> GetStopDic()
        {
            string[] dics = "的,了,在,是,我,有,和,就,不,人,都,一,一个,上,也,很,到,说,要,去,你,会,着,没有,看,好,自己,这".Split(',');

            return dics.ToList();
        }

        /// <summary>
        /// 取删除词典
        /// </summary>
        /// <returns></returns>
        public List<string> GetDelDic()
        {
            string[] dics = "偷拍自拍".Split(',');

            return dics.ToList();
        }

    }
}
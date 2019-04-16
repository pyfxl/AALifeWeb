using AALife.Core.Infrastructure.Mapper;
using AALife.Data;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.jqGrid;
using System;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class CardsApiController : BaseApiController
    {
        private readonly ICardService _cardService;

        public CardsApiController(ICardService cardService)
        {
            this._cardService = cardService;
        }

        // GET api/<controller>
        public IHttpActionResult Get(Guid id)
        {
            var result = _cardService.GetAll(id);
            var grid = new DataSourceResult
            {
                rows = result,
                records = result.Count()
            };
            return Json(grid);
        }

        // POST api/<controller>
        public IHttpActionResult Post(Guid id, CardTable model)
        {
            model.LiveOn();
            model.UserId = id;

            _cardService.Add(model);
            _cardService.ClearCache(id);

            return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(Guid id, CardTable model)
        {
            var item = _cardService.Get(model.Id);

            AutoMapperConfiguration.Mapper.Map(model, item);

            item.LiveOn();
            
            _cardService.Update(item);
            _cardService.ClearCache(id);

            return Ok();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var item = _cardService.Get(id);
            item.LiveOff();

            _cardService.Update(item);
            _cardService.ClearCache(item.UserId);

            return Ok();
        }

        #region 其他方法 

        // GET api/<controller>
        [Route("api/v1/cardnamesapi")]
        public IHttpActionResult GetCardNames(string term)
        {
            if (string.IsNullOrWhiteSpace(term)) return Json("");

            var all = _cardService.FindAll(a => a.CardName.Contains(term))
                .GroupBy(a => new { a.Id, a.CardName })
                .Select(a => new { a.Key.Id, a.Key.CardName, Index = a.Key.CardName.IndexOf(term) })
                .OrderBy(a => a.Index)
                //.Skip(0).Take(50)
                .ToList();

            var result = all.GroupBy(a => a.CardName)
                .Select(a => new { value = string.Join(", ", all.Where(b => b.CardName == a.Key).Select(b => b.Id).ToArray()), text = a.Key })
                .ToList();

            return Json(result);
        }

        #endregion

    }
}
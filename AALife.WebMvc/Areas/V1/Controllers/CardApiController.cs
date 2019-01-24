using AALife.BLL;
using AALife.Core;
using AALife.Core.Domain;
using AALife.Core.Services;
using AALife.WebMvc.jqGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class CardApiController : ApiController
    {
        private readonly ICardService _cardService;

        public CardApiController(ICardService cardService)
        {
            this._cardService = cardService;
        }

        // GET api/<controller>
        public IHttpActionResult Get(int id)
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
        public IHttpActionResult Post(int id, CardTable model)
        {
            model.UpdateField();
            model.UserId = id;
            model.CardId = _cardService.GetMaxId(id);

            _cardService.Add(model);

            _cardService.ClearCache(id);

            return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, CardTable model)
        {
            var item = _cardService.Get(model.Id);
            item.UpdateField();
            item.CardName = model.CardName;
            item.Image = model.Image;
            item.CardNumber = model.CardNumber;
            item.MoneyStart = model.MoneyStart;

            _cardService.Update(item);

            _cardService.ClearCache(id);

            return Ok();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var item = _cardService.Get(id);
            item.UpdateField(0);

            _cardService.Update(item);

            //要使用userid
            _cardService.ClearCache(item.UserId);

            return Ok();
        }
        
    }
}
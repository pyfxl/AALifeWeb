using AALife.BLL;
using AALife.Core.Services;
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
            var result = _cardService.GetAllCard(id);
            return Json(result);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
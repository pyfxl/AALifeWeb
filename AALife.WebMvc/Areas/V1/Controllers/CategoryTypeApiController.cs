using AALife.BLL;
using AALife.Core.Services;
using AALife.Model;
using AALife.Service.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class CategoryTypeApiController : ApiController
    {
        private readonly ICategoryTypeService _categoryTypeService;

        public CategoryTypeApiController(ICategoryTypeService categoryTypeService)
        {
            this._categoryTypeService = categoryTypeService;
        }

        // GET api/<controller>
        public IHttpActionResult Get(int id)
        {
            var result = _categoryTypeService.GetAllCategoryType(id);
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
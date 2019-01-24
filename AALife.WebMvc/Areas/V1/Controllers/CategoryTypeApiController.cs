using AALife.BLL;
using AALife.Core;
using AALife.Core.Caching;
using AALife.Core.Domain;
using AALife.Core.Services;
using AALife.Model;
using AALife.Service.Domain.ViewModel;
using AALife.WebMvc.jqGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class CategoryTypeApiController : BaseApiController
    {
        private readonly ICategoryTypeService _categoryTypeService;

        public CategoryTypeApiController(ICategoryTypeService categoryTypeService)
        {
            this._categoryTypeService = categoryTypeService;
        }

        // GET api/<controller>
        public IHttpActionResult Get(int id)
        {
            var result = _categoryTypeService.GetAll(id);
            var grid = new DataSourceResult
            {
                rows = result,
                records = result.Count()
            };
            return Json(grid);
        }

        // POST api/<controller>
        public IHttpActionResult Post(int id, CategoryTypeTable model)
        {
            model.UpdateField();
            model.UserId = id;
            model.CategoryTypeId = _categoryTypeService.GetMaxId(id);

            _categoryTypeService.Add(model);

            _categoryTypeService.ClearCache(id);

            return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, CategoryTypeTable model)
        {
            var item = _categoryTypeService.Get(model.Id);
            item.UpdateField();
            item.CategoryTypeName = model.CategoryTypeName;
            item.Image = model.Image;

            _categoryTypeService.Update(item);

            _categoryTypeService.ClearCache(id);

            return Ok();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var item = _categoryTypeService.Get(id);
            item.UpdateField(0);

            _categoryTypeService.Update(item);

            //要使用userid
            _categoryTypeService.ClearCache(item.UserId);

            return Ok();
        }

    }
}
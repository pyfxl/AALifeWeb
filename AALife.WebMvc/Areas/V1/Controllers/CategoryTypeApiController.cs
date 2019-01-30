using AALife.Data;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.jqGrid;
using System.Linq;
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
            model.LiveOn();
            model.UserId = id;

            _categoryTypeService.Add(model);
            _categoryTypeService.ClearCache(id);

            return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, CategoryTypeTable model)
        {
            var item = _categoryTypeService.Get(model.Id);
            item.LiveOn();
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
            item.LiveOff();

            _categoryTypeService.Update(item);
            _categoryTypeService.ClearCache(item.UserId);

            return Ok();
        }

    }
}
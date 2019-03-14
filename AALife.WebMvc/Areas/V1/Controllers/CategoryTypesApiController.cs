using AALife.Core.Infrastructure.Kendoui;
using AALife.Data;
using AALife.Data.Domain;
using AALife.Data.Services;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class CategoryTypesApiController : BaseApiController
    {
        private readonly ICategoryTypeService _categoryTypeService;

        public CategoryTypesApiController(ICategoryTypeService categoryTypeService)
        {
            this._categoryTypeService = categoryTypeService;
        }

        // GET api/<controller>
        public IHttpActionResult Get(int id)
        {
            var result = _categoryTypeService.GetAll(id);
            var grid = new jqGrid.DataSourceResult
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

        #region 其他方法 

        // GET api/<controller>
        [Route("api/v1/categorytypenamesapi")]
        public IHttpActionResult GetCategoryTypeNames(string term)
        {
            if (string.IsNullOrWhiteSpace(term)) return Json("");

            var all = _categoryTypeService.FindAll(a => a.CategoryTypeName.Contains(term))
                .GroupBy(a => new { a.Id, a.CategoryTypeName })
                .Select(a => new { a.Key.Id, a.Key.CategoryTypeName, Index = a.Key.CategoryTypeName.IndexOf(term) })
                .OrderBy(a => a.Index)
                //.Skip(0).Take(50)
                .ToList();

            var result = all.GroupBy(a => a.CategoryTypeName)
                .Select(a => new { value = string.Join(", ", all.Where(b => b.CategoryTypeName == a.Key).Select(b => b.Id).ToArray()), text = a.Key })
                .ToList();

            return Json(result);
        }
        
        #endregion

    }
}
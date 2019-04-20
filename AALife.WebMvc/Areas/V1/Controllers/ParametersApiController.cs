using AALife.Core.Domain.Configuration;
using AALife.Core.Infrastructure.Kendoui;
using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.WebMvc.Infrastructure.Mapper;
using AALife.WebMvc.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class ParametersApiController : BaseApiController
    {
        private readonly IParameterService _parameterService;
        private readonly ICustomerActivityService _customerActivityService;

        public ParametersApiController(IParameterService parameterService,
            ICustomerActivityService customerActivityService)
        {
            this._parameterService = parameterService;
            this._customerActivityService = customerActivityService;
        }

        // GET: api/Parameters
        public IHttpActionResult Get()
        {
            var result = _parameterService.Get();

            result = result.OrderBy(a => a.OrderNo);

            return Json(result);
        }

        // GET: api/Parameters/5
        public IHttpActionResult Get(int id)
        {
            var result = _parameterService.FindAll(a => a.ParentId == id);

            result = result.OrderBy(a => a.OrderNo);

            var grid = new DataSourceResult
            {
                Data = result,
                Total = result.Count()
            };

            return Json(grid);
        }

        // POST: api/Parameters
        public IHttpActionResult Post(int id, [FromBody]IEnumerable<Parameter> models)
        {
            var parent = _parameterService.Get(id);
            models.ToList().ForEach(a =>
            {
                a.OrderNo = a.GetOrderNo(parent);
                a.ParentId = id;

                _parameterService.Add(a);
            });

            return Json(HttpStatusCode.OK);
        }

        // PUT: api/Parameters/5
        public IHttpActionResult Put(int id, [FromBody]IEnumerable<Parameter> models)
        {
            var parent = _parameterService.Get(id);
            models.ToList().ForEach(a =>
            {
                var parameter = _parameterService.Get(a.Id);

                parameter.Name = a.Name;
                parameter.Rank = a.Rank;
                parameter.IsDefault = a.IsDefault;
                parameter.IsLeaf = a.IsLeaf;
                parameter.OrderNo = a.GetOrderNo(parent);
                parameter.Notes = a.Notes;

                _parameterService.Update(a);
            });
            
            return Json(HttpStatusCode.OK);
        }

        // DELETE: api/Parameters/5
        public IHttpActionResult Delete([FromBody]IEnumerable<Parameter> models)
        {
            models.ToList().ForEach(a =>
            {
                _parameterService.Delete(a.Id);
            });
            
            return Json(HttpStatusCode.OK);
        }

        #region 其它方法 

        // 根据Key获取参数
        [Route("api/v1/paramsbynameapi/{name}")]
        public IHttpActionResult GetParamsByName(string name)
        {
            var result = _parameterService.GetParamsByName(name);

            var data = result.Select(x =>
            {
                var m = x.MapTo<Parameter, ParameterViewModel>();
                return m;
            });

            return Json(data);
        }

        #endregion
    }
}

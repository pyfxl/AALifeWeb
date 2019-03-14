using AALife.Core.Domain.Configuration;
using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Core.Infrastructure.Kendoui;
using AALife.WebMvc.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AALife.WebMvc.Infrastructure.Mapper;

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
                Data = result.AsEnumerable().Select(x =>
                {
                    var m = x.MapTo<Parameter, ParameterViewModel>();
                    return m;
                }),
                Total = result.Count()
            };

            return Json(grid);
        }

        // POST: api/Parameters
        public IHttpActionResult Post([FromBody]Parameter model)
        {
            if (ModelState.IsValid)
            {
                var parent = _parameterService.Get(model.ParentId.Value);

                model.OrderNo = model.GetOrderNo(parent);

                _parameterService.Add(model);
            }

            return Json(HttpStatusCode.OK);
        }

        // PUT: api/Parameters/5
        public IHttpActionResult Put([FromBody]Parameter model)
        {
            if (ModelState.IsValid)
            {
                var parent = _parameterService.Get(model.ParentId.Value);
                var parameter = _parameterService.Get(model.Id);

                parameter.Name = model.Name;
                parameter.Rank = model.Rank;
                parameter.OrderNo = model.GetOrderNo(parent);

                _parameterService.Update(parameter);
            }

            return Json(HttpStatusCode.OK);
        }

        // DELETE: api/Parameters/5
        public IHttpActionResult Delete(int id)
        {
            _parameterService.Delete(id);

            return Json(HttpStatusCode.OK);
        }

        #region 其它方法 

        // 获取列表树
        [Route("api/v1/parametertreesapi")]
        public IHttpActionResult GetParameterTrees()
        {
            var tree = SortForTree();

            return Json(tree);
        }

        private List<TreeViewModel> SortForTree(int? parentId = null)
        {
            var model = new List<TreeViewModel>();
            foreach (var p in _parameterService.FindAll(a => a.ParentId == parentId && a.IsLeaf == null).OrderBy(a => a.OrderNo))
            {
                var pm = new TreeViewModel
                {
                    id = p.Id,
                    text = p.Name,
                    parentId = p.ParentId.GetValueOrDefault(),
                    value = p.Value,
                    systemName = p.SystemName,
                    rank = p.Rank
                };
                pm.items.AddRange(SortForTree(p.Id));
                pm.hasChildren = pm.items.Count > 0;
                model.Add(pm);
            }
            return model;
        }

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

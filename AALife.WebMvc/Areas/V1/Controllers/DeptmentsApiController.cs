using AALife.Core.Infrastructure.Kendoui;
using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using AALife.WebMvc.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class DeptmentsApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public DeptmentsApiController(IUserService userService, 
            IUserDeptmentService userDeptmentService,
            ICustomerActivityService customerActivityService,
            IParameterService parameterService)
        {
            this._userService = userService;
            this._userDeptmentService = userDeptmentService;
            this._customerActivityService = customerActivityService;
            this._parameterService = parameterService;
        }

        // GET: api/Deptments
        public IHttpActionResult Get()
        {
            var result = _userDeptmentService.Get();

            var grid = result.AsEnumerable().Select(x =>
            {
                var m = x.MapTo<UserDeptment, UserDeptmentModel>();
                m.ParentName = x.Parent != null ? x.Parent.Name : "";
                return m;
            });

            return Json(grid);
        }

        // GET: api/Deptments/5
        public IHttpActionResult Get(int id)
        {
            var result = _userDeptmentService.FindAll(a => a.ParentId == id);

            var grid = new DataSourceResult
            {
                Data = result.AsEnumerable().Select(x =>
                {
                    var m = x.MapTo<UserDeptment, UserDeptmentModel>();
                    m.ParentName = x.Parent.Name;
                    return m;
                }),
                Total = result.Count()
            };

            return Json(grid);
        }

        //// POST: api/Deptments
        //public IHttpActionResult Post([FromBody]UserDeptment model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var parent = _userDeptmentService.Get(model.ParentId.Value);

        //        _userDeptmentService.Add(model);
        //    }

        //    return Json(HttpStatusCode.OK);
        //}

        // POST: api/Deptments
        public IHttpActionResult Post(IEnumerable<UserDeptment> models)
        {
            if (ModelState.IsValid)
            {
                _userDeptmentService.Add(models);
            }

            return Json(HttpStatusCode.OK);
        }

        //// PUT: api/Deptments/5
        //public IHttpActionResult Put([FromBody]UserDeptment model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var parameter = _userDeptmentService.Get(model.Id);

        //        parameter.Name = model.Name;

        //        _userDeptmentService.Update(parameter);
        //    }

        //    return Json(HttpStatusCode.OK);
        //}

        // PUT: api/Deptments/5
        public IHttpActionResult Put(IEnumerable<UserDeptment> models)
        {
            if (ModelState.IsValid)
            {
                var items = new List<UserDeptment>();
                models.ToList().ForEach(a =>
                {
                    var item = _userDeptmentService.Get(a.Id);
                    item.Name = a.Name;
                    item.ParentId = a.ParentId;
                    items.Add(item);
                });

                _userDeptmentService.Update(items);
            }

            return Json(HttpStatusCode.OK);
        }

        // DELETE: api/Deptments/5
        public IHttpActionResult Delete(int id)
        {
            _userDeptmentService.Delete(id);

            return Json(HttpStatusCode.OK);
        }

        #region 其它方法 

        // 获取列表树
        [Route("api/v1/deptmenttreesapi/{id}")]
        public IHttpActionResult GetDeptmentTrees(int id)
        {
            var tree = SortForTree(id);

            return Json(tree);
        }

        // 获取列表树
        [Route("api/v1/deptmenttreesapi")]
        public IHttpActionResult GetDeptmentTrees()
        {
            var tree = SortForTree();

            return Json(tree);
        }

        private List<TreeViewModel> SortForTree(int? id = null, int? parentId = null)
        {
            var userDeptment = new List<UserDeptment>();
            if(id != null)
                userDeptment = _userService.Get(id.GetValueOrDefault()).UserDeptments.ToList();

            var model = new List<TreeViewModel>();
            foreach (var p in _userDeptmentService.FindAll(a => a.ParentId == parentId))
            {
                var pm = new TreeViewModel
                {
                    id = p.Id,
                    text = p.Name,
                    parentId = p.ParentId.GetValueOrDefault(),
                    value = p.Name,
                    name = p.Name,
                    isChecked = userDeptment.Any() ? userDeptment.Any(a => a.Id == p.Id) : false
                };
                pm.items.AddRange(SortForTree(id, p.Id));
                pm.hasChildren = pm.items.Count > 0;
                model.Add(pm);
            }
            return model;
        }

        #endregion
    }
}

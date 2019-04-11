using AALife.Core.Domain.Logging;
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
                m.Parent = x.Parent;
                m.CategoryName = _parameterService.GetParamsByName("DeptmentCategory").First(a => a.Value == x.Category).Name;
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
                    m.Parent = x.Parent;
                    return m;
                }),
                Total = result.Count()
            };

            return Json(grid);
        }

        // POST: api/Deptments
        public IHttpActionResult Post(UserDeptment model)
        {
            if (ModelState.IsValid)
            {
                if (model.Parent != null)
                {
                    model.ParentId = model.Parent.Id;
                    model.Parent = null;
                }

                _userDeptmentService.Add(model);
            }
            
            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入部门记录。{0}", model.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // PUT: api/Deptments/5
        public IHttpActionResult Put(UserDeptment model)
        {
            if (ModelState.IsValid)
            {
                var item = _userDeptmentService.Get(model.Id);
                item.Name = model.Name;
                item.Category = model.Category;
                item.Notes = model.Notes;
                if (model.Parent != null)
                {
                    item.ParentId = model.Parent.Id;
                    model.Parent = null;
                }

                _userDeptmentService.Update(item);
            }

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Update, "更新部门记录。{0}", model.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // DELETE: api/Deptments/5
        public IHttpActionResult Delete(UserDeptment model)
        {
            _userDeptmentService.Delete(model.Id);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Delete, "删除部门记录。{0}", model.ToJson());

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

        // 树方法
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

        // 面包导航
        [Route("api/v1/deptmentbreadcrumbapi")]
        public IHttpActionResult GetFormattedBreadCrumb ()
        {
            var results = new List<UserDeptmentModel>();

            var deptments = _userDeptmentService.Get();

            //deptments = deptments.OrderBy(a => a.ParentId);

            foreach (var pr in deptments)
            {
                results.Add(new UserDeptmentModel
                {
                    Id = pr.Id,
                    Name = _userDeptmentService.GetFormattedBreadCrumb(pr),
                    Category = pr.Category,
                    Notes = pr.Notes
                });
            }

            results = results.OrderBy(a => a.Name).ToList();

            return Json(results);
        }

        // 获取组织类型
        [Route("api/v1/deptmentcategoryapi")]
        public IHttpActionResult GetDeptmentCategory()
        {
            var result = _parameterService.GetParamsByName("DeptmentCategory");

            return Json(result);
        }

        #endregion
    }
}

using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using AALife.WebMvc.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class DeptmentsTreeApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public DeptmentsTreeApiController(IUserService userService, 
            IUserDeptmentService userDeptmentService,
            ICustomerActivityService customerActivityService,
            IParameterService parameterService)
        {
            this._userService = userService;
            this._userDeptmentService = userDeptmentService;
            this._customerActivityService = customerActivityService;
            this._parameterService = parameterService;
        }

        // GET: api/Deptments/5
        public IHttpActionResult Get(Guid? id = null)
        {
            var result = _userDeptmentService.FindAll(a => a.ParentId == id);

            var grid = result.AsEnumerable().Select(x =>
            {
                var pm = new TreeViewModel
                {
                    id = x.Id,
                    text = x.Name,
                    parentId = x.ParentId,
                    value = x.Name,
                    name = x.Name
                };
                pm.hasChildren = _userDeptmentService.IsExists(a => a.ParentId == pm.id);
                pm.expanded = false;
                return pm;
            });

            return Json(grid);
        }

        #region 其它方法 

        // 组织岗位树
        [Route("api/v1/deptmentspositiontreeapi")]
        public IHttpActionResult GetDeptmentsPositionTree(Guid? id = null)
        {
            var result = _userDeptmentService.FindAll(a => a.ParentId == id);

            //下级组织
            var grid = result.AsEnumerable().Select(x =>
            {
                var hasChildren = _userDeptmentService.IsExists(a => a.ParentId == x.Id);
                var pm = new TreeViewModel
                {
                    id = x.Id,
                    text = x.Name,
                    parentId = x.ParentId,
                    value = x.Name,
                    name = x.Name,
                    code = x.Code
                };
                pm.hasChildren = hasChildren ? true : x.Positions.Any();
                pm.expanded = false;
                pm.isDeptment = true;
                return pm;
            }).ToList();

            //取当前岗位
            if (id != null)
            {
                var deptment = _userDeptmentService.Get(id.Value);
                deptment.Positions.ToList().ForEach(x =>
                {
                    var pm = new TreeViewModel
                    {
                        id = x.Id,
                        text = x.Name,
                        parentId = x.ParentId,
                        value = x.Name,
                        name = x.Name,
                        deptmentId = deptment.Id
                    };
                    pm.isPosition = true;
                    grid.Add(pm);
                });
            }

            return Json(grid);
        }

        private List<TreeViewModel> SortForTree(Guid? id = null, Guid? parentId = null)
        {
            var userDeptment = new List<UserDeptment>();
            if (id != null)
                userDeptment = _userService.Get(id.GetValueOrDefault()).UserDeptments.ToList();

            var model = new List<TreeViewModel>();
            foreach (var p in _userDeptmentService.FindAll(a => a.ParentId == parentId))
            {
                var pm = new TreeViewModel
                {
                    id = p.Id,
                    text = p.Name,
                    parentId = p.ParentId,
                    value = p.Name,
                    name = p.Name,
                    //isChecked = userDeptment.Any() ? userDeptment.Any(a => a.Id == p.Id) : false
                };
                //pm.items.AddRange(SortForTree(id, p.Id));
                pm.hasChildren = true;
                pm.expanded = false;
                model.Add(pm);
            }
            return model;
        }

        private string GetTreeNameChild(UserDeptment dept)
        {
            if (dept.Positions.Count() > 0)
            {
                return string.Format("{0} ({1})", dept.Name, dept.Positions.Count());
            }

            return dept.Name;
        }

        #endregion
    }
}

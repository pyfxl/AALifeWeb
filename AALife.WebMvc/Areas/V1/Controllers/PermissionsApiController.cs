using AALife.Core.Domain.Logging;
using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using AALife.WebMvc.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class PermissionsApiController : BaseApiController
    {
        private readonly IUserPermissionService _permissionService;
        private readonly IUserRoleService _userRoleService;
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly IUserPositionService _userPositionService;
        private readonly ICustomerActivityService _customerActivityService;

        public PermissionsApiController(IUserPermissionService permissionService,
            IUserRoleService userRoleService,
            IUserDeptmentService userDeptmentService,
            IUserPositionService userPositionService,
            ICustomerActivityService customerActivityService)
        {
            this._permissionService = permissionService;
            this._userRoleService = userRoleService;
            this._userDeptmentService = userDeptmentService;
            this._userPositionService = userPositionService;
            this._customerActivityService = customerActivityService;
        }


        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var result = _permissionService.Get();
            
            result = result.OrderBy(a => a.OrderNo);

            var model = result.AsEnumerable().Select(x => 
            {
                return x.ToModel();
            });

            return Json(model);
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]UserPermission model)
        {
            if (!ModelState.IsValid)
                return ErrorForKendoGridJson(ModelState);

            //admin role
            var adminRole = _userRoleService.GetUserRoleBySystemName(UserRoleNames.Administrators);
            var parent = _permissionService.Get(model.ParentId.GetValueOrDefault());

            //add role
            model.UserRoles.Add(adminRole);
            model.OrderNo = model.GetOrderNo(parent);

            //insert
            _permissionService.Add(model);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入权限记录。{0}", model.ToJson());
            
            return Json(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]UserPermission model)
        {
            if (!ModelState.IsValid)
                return ErrorForKendoGridJson(ModelState);

            //var parent = _permissionService.Get(model.ParentId.GetValueOrDefault());
            var permission = _permissionService.Get(model.Id);

            permission.Name = model.Name;
            permission.AreaName = model.AreaName;
            permission.ControllerName = model.ControllerName;
            permission.ActionName = model.ActionName;
            permission.IconName = model.IconName;
            permission.Rank = model.Rank;
            permission.IsButton = model.IsButton;
            //permission.OrderNo = model.GetOrderNo(permission.ParentRecord);

            //更新子列表排序
            //permission.ChildRecords.ToList().ForEach(x =>
            //{
            //    x.OrderNo = x.GetOrderNo(permission);
            //});

            //更新子列表排序
            Action<UserPermission> action = null;
            action = (item) =>
            {
                item.OrderNo = item.GetOrderNo(item.Parent);
                foreach (var it in item.Children)
                {
                    action(it);
                }
            };

            //调用
            action(permission);

            //update
            _permissionService.Update(permission);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Update, "更新权限记录。{0}", model.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete([FromBody]IEnumerable<UserPermission> models)
        {
            var permissions = new List<UserPermission>();

            models.OrderByDescending(a => a.ParentId).ToList().ForEach(x =>
            {
                var m = _permissionService.Get(x.Id);
                m.UserRoles.Clear();

                permissions.Add(m);
            });

            //delete
            _permissionService.Delete(permissions);

            return Json(HttpStatusCode.OK);
        }

        #region 其它方法

        // 更新角色权限
        [Route("api/v1/permissionsroleupdateapi")]
        public void PermissionUpdate(dynamic param)
        {
            var role = _userRoleService.Get((Guid)param.id);
            var permission = _permissionService.Get((Guid)param.pid);

            if (permission.UserRoles.Contains(role))
            {
                permission.UserRoles.Remove(role);
            }
            else
            {
                permission.UserRoles.Add(role);
            }

            _permissionService.Update(permission);
        }

        // 获取权限列表树
        [Route("api/v1/permissiontreesapi")]
        public IHttpActionResult GetPermissionTrees()
        {
            var permissionTree = SortPermissionForTree();

            return Json(permissionTree);
        }

        private List<PermissionTreeModel> SortPermissionForTree(Guid parentId = default(Guid))
        {
            var model = new List<PermissionTreeModel>();
            foreach (var p in _permissionService.FindAll(a => a.ParentId == parentId).OrderBy(a => a.OrderNo))
            {
                var pm = new PermissionTreeModel
                {
                    Id = p.Id,
                    text = p.Name,
                };
                pm.items.AddRange(SortPermissionForTree(p.Id));
                pm.hasChildren = pm.items.Count > 0;
                model.Add(pm);
            }
            return model;
        }

        #endregion
    }
}
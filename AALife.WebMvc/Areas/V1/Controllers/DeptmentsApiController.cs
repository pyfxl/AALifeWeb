using AALife.Core.Domain.Logging;
using AALife.Core.Infrastructure.Kendoui;
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
    /// <summary>
    /// 组织管理接口
    /// </summary>
    public class DeptmentsApiController : BaseApiController
    {
        private readonly IUserPermissionService _permissionService;
        private readonly IUserPositionService _userPositionService;
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly ICustomerActivityService _customerActivityService;

        public DeptmentsApiController(IUserPermissionService permissionService, 
            IUserPositionService userPositionService,
            IUserDeptmentService userDeptmentService,
            ICustomerActivityService customerActivityService)
        {
            this._permissionService = permissionService;
            this._userPositionService = userPositionService;
            this._userDeptmentService = userDeptmentService;
            this._customerActivityService = customerActivityService;
        }

        // GET api/<controller>
        public IHttpActionResult Get(Guid? id)
        {
            var deptments = _userDeptmentService.FindAll(x => x.ParentId == id);

            //没有下级显示自身
            if (deptments == null || !deptments.Any())
            {
                deptments = _userDeptmentService.FindAll(x => x.Id == id);
            }

            //没有找到就找岗位的组织
            if (deptments == null || !deptments.Any())
            {
                var position = _userPositionService.Get(id.Value);
                deptments = _userDeptmentService.FindAll(x => x.Id == position.DeptmentId);
            }

            var grid = new DataSourceResult
            {
                Data = deptments.ToList().Select(x => 
                {
                    var m = x.MapTo<UserDeptment, UserDeptmentModel>();
                    m.Parent = x.Parent;
                    return m;
                }),
                Total = deptments.Count()
            };

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Query, "浏览部门记录。{0}", id);

            return Json(grid);
        }

        // POST api/<controller>
        public IHttpActionResult Post(Guid id, IEnumerable<UserDeptment> models)
        {
            if (!ModelState.IsValid)
                return ErrorForKendoGridJson(ModelState);

            models.ToList().ForEach(x =>
            {
                x.Id = Guid.NewGuid();
                x.ParentId = id;
                if (x.Parent != null)
                {
                    x.ParentId = x.Parent.Id;
                    x.Parent = null;
                }
            });

            //insert
            _userDeptmentService.Add(models);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入部门记录。{0}", models.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(Guid id, IEnumerable<UserDeptment> models)
        {
            if (!ModelState.IsValid)
                return ErrorForKendoGridJson(ModelState);

            var deptments = new List<UserDeptment>();
            models.ToList().ForEach(x =>
            {
                var deptment = _userDeptmentService.Get(x.Id);
                deptment.Name = x.Name;
                deptment.Code = x.Code;
                deptment.Notes = x.Notes;
                if (x.Parent != null)
                {
                    deptment.ParentId = x.Parent.Id;
                }
                deptments.Add(deptment);
            });

            //update
            _userDeptmentService.Update(deptments);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Update, "更新部门记录。{0}", models.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(IEnumerable<UserDeptment> models)
        {
            var deptments = new List<UserDeptment>();
            models.ToList().ForEach(x =>
            {
                var deptment = _userDeptmentService.Get(x.Id);
                deptments.Add(deptment);
            });

            //delete
            _userDeptmentService.Delete(deptments);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Delete, "删除部门记录。{0}", models.ToJson());

            return Json(HttpStatusCode.OK);
        }

        #region 其它方法

        // 更新组织权限
        [Route("api/v1/deptmentspermissionapi")]
        public void DeptmentsPermission(Guid id, IEnumerable<dynamic> models)
        {
            var deptment = _userDeptmentService.Get(id);

            //清除所有
            deptment.UserPermissions.Clear();

            models.ToList().ForEach(x =>
            {
                Guid pid = (Guid)x.id;
                var permission = _permissionService.Get(pid);

                deptment.UserPermissions.Add(permission);
            });

            _userDeptmentService.Update(deptment);
        }

        #endregion
    }
}
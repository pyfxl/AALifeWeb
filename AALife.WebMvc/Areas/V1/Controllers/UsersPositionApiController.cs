using AALife.Core.Domain.Logging;
using AALife.Core.Infrastructure.Kendoui;
using AALife.Core.Services.Configuration;
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
    /// 根据用户找岗位接口
    /// </summary>
    public class UsersPositionApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly IUserPositionService _userPositionService;
        private readonly IUsersPositionsService _usersPositionsService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public UsersPositionApiController(IUserService userService,
            IUserDeptmentService userDeptmentService,
            IUserPositionService userPositionService,
            IUsersPositionsService usersPositionsService,
            ICustomerActivityService customerActivityService,
            IParameterService parameterService)
        {
            this._userService = userService;
            this._userDeptmentService = userDeptmentService;
            this._userPositionService = userPositionService;
            this._usersPositionsService = usersPositionsService;
            this._customerActivityService = customerActivityService;
            this._parameterService = parameterService;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(Guid id)
        {
            var userPositions = _usersPositionsService.FindAll(a => a.User.Id == id);

            var grid = new DataSourceResult
            {
                Data = userPositions.Select(x => new UserPositionModel
                {
                    Id = x.Position.Id,
                    Name = x.Position.Name,
                    Code = x.Position.Code,
                    Notes = x.Position.Notes,
                    ParentId = x.Position.ParentId,
                    Parent = x.Position.Parent,
                    TitleId = x.Position.TitleId,
                    Title = x.Position.Title,
                    IsMainPosition = x.IsMainPosition,
                    IsDeptmentLeader = x.IsDeptmentLeader
                }),
                Total = userPositions.Count()
            };

            return Json(grid);
        }

        // POST api/<controller>/5
        public IHttpActionResult Post(Guid id, [FromBody]IEnumerable<UserPosition> models)
        {
            //user
            var user = _userService.Get(id);

            var positions = user.UsersPositions.Select(a => a.Position);

            models.ToList().ForEach(x =>
            {
                var position = _userPositionService.Get(x.Id);

                if (!positions.Contains(position))
                {
                    positions.ToList().Add(position);

                    var deptment = _userDeptmentService.Get(position.DeptmentId);

                    user.UserDeptments.Add(deptment);
                }
            });

            //insert
            _userService.Update(user);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入岗位记录。{0}", models.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(Guid id, [FromBody]IEnumerable<UserPosition> models)
        {
            //user
            var user = _userService.Get(id);

            models.ToList().ForEach(x =>
            {
                var position = _userPositionService.Get(x.Id);

                //if (user.UserPositions.Contains(position))
                //{
                //    user.UserPositions.Remove(position);

                //    var deptment = _userDeptmentService.Get(position.DeptmentId);

                //    user.UserDeptments.Remove(deptment);
                //}
            });

            //insert
            _userService.Update(user);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入岗位记录。{0}", models.ToJson());

            return Json(HttpStatusCode.OK);
        }

        #region 其它方法

        // 更新用户主岗位
        [Route("api/v1/usersmainpositionapi")]
        public void UpdateUsersMainPosition(dynamic param)
        {
            var id = (Guid)param.id;
            var pid = (Guid)param.pid;

            //当前岗位
            var userPosition = _usersPositionsService.Find(a => a.User.Id == id && a.Position.Id == pid);

            userPosition.IsMainPosition = !userPosition.IsMainPosition;
            
            //主岗位
            var positions = _usersPositionsService.FindAll(a => a.User.Id == id && a.IsMainPosition.GetValueOrDefault());

            //如果没有主岗位，抛异常
            if (positions.Count() <= 1 && !userPosition.IsMainPosition.Value)
                throw new Exception("主岗位必须有！");

            //update
            _usersPositionsService.Update(userPosition);

        }

        // 更新岗位负责人
        [Route("api/v1/usersdeptmentleaderapi")]
        public void UpdateUsersDeptmentLeader(dynamic param)
        {
            var id = (Guid)param.id;
            var pid = (Guid)param.pid;

            //当前岗位
            var userPosition = _usersPositionsService.Find(a => a.User.Id == id && a.Position.Id == pid);

            userPosition.IsDeptmentLeader = !userPosition.IsDeptmentLeader;

            //update
            _usersPositionsService.Update(userPosition);

        }

        #endregion

    }
}
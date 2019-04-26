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
    public class RolesApiController : BaseApiController
    {
        private readonly IUserRoleService _userRoleService;
        private readonly ICustomerActivityService _customerActivityService;

        public RolesApiController(IUserRoleService userRoleService,
            ICustomerActivityService customerActivityService)
        {
            this._userRoleService = userRoleService;
            this._customerActivityService = customerActivityService;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var result = _userRoleService.Get();

            var grid = new DataSourceResult
            {
                Data = result,
                Total = result.Count()
            };

            return Json(grid);
        }

        // POST api/<controller>
        public IHttpActionResult Post(IEnumerable<UserRole> models)
        {
            if (ModelState.IsValid)
            {
                models.ToList().ForEach(x =>
                {
                    x.Id = Guid.NewGuid();
                });

                //insert
                _userRoleService.Add(models);
            }

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入角色记录。{0}", models.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(IEnumerable<UserRole> models)
        {
            if (ModelState.IsValid)
            {
                var items = new List<UserRole>();
                models.ToList().ForEach(a =>
                {
                    var item = _userRoleService.Get(a.Id);
                    item.Name = a.Name;
                    item.SystemName = a.SystemName;
                    item.Notes = a.Notes;
                    items.Add(item);
                });

                _userRoleService.Update(items);
            }

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Update, "更新角色记录。{0}", models.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(UserRole model)
        {
            _userRoleService.Delete(model.Id);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Delete, "删除角色记录。{0}", model.ToJson());

            return Json(HttpStatusCode.OK);
        }
    }
}
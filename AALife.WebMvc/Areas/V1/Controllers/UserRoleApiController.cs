using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using AALife.Data.Services;
using Kendo.DynamicLinq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class UserRoleApiController : BaseApiController
    {
        private readonly IUserRoleService _userRoleService;
        private readonly ICustomerActivityService _customerActivityService;

        public UserRoleApiController(IUserRoleService userRoleService,
            ICustomerActivityService customerActivityService)
        {
            this._userRoleService = userRoleService;
            this._customerActivityService = customerActivityService;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id, [FromUri]DataSourceRequest request)
        {
            var result = _userRoleService.Get(id).UserTables
                .AsQueryable()
                .OrderByDescending(a => a.Id);

            return Json(result.ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter));
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]IEnumerable<UserTable> models)
        {
            var role = _userRoleService.Get(id);
            models.ToList().ForEach(a =>
            {
                role.UserTables.Add(a);
            });

            _userRoleService.Update(role);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
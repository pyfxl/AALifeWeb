using AALife.Core.Services.Logging;
using AALife.Data.Services;
using Kendo.DynamicLinq;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class RoleApiController : BaseApiController
    {
        private readonly IUserRoleService _userRoleService;
        private readonly ICustomerActivityService _customerActivityService;

        public RoleApiController(IUserRoleService userRoleService,
            ICustomerActivityService customerActivityService)
        {
            this._userRoleService = userRoleService;
            this._customerActivityService = customerActivityService;
        }

        // GET api/<controller>
        [Route("api/v1/rolesapi")]
        public IHttpActionResult Get()
        {
            var result = _userRoleService.FindAll(a => a.Live == 1);

            var grid = new DataSourceResult
            {
                Data = result,
                Total = result.Count()
            };

            return Json(grid);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
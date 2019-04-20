using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class RolesTreeApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public RolesTreeApiController(IUserService userService,
            IUserRoleService userRoleService,
            ICustomerActivityService customerActivityService,
            IParameterService parameterService)
        {
            this._userService = userService;
            this._userRoleService = userRoleService;
            this._customerActivityService = customerActivityService;
            this._parameterService = parameterService;
        }

        // GET: api/Roles
        public IHttpActionResult Get()
        {
            var tree = SortForTree();

            return Json(tree);
        }

        // GET: api/Roles/5
        public IHttpActionResult Get(Guid id)
        {
            var tree = SortForTree(id);

            return Json(tree);
        }

        #region 其它方法 

        private List<TreeViewModel> SortForTree(Guid? id = null, Guid? parentId = null)
        {
            var userRole = new List<UserRole>();
            if(id != null)
                userRole = _userService.Get(id.GetValueOrDefault()).UserRoles.ToList();

            var model = new List<TreeViewModel>();
            foreach (var p in _userRoleService.FindAll(a => a.Id != default(Guid)))
            {
                var pm = new TreeViewModel
                {
                    Id = p.Id,
                    text = p.Name,
                    value = p.Name,
                    isChecked = userRole.Any() ? userRole.Any(a => a.Id == p.Id) : false
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

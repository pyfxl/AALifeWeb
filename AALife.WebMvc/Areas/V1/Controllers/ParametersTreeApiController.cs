using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.WebMvc.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class ParametersTreeApiController : BaseApiController
    {
        private readonly IParameterService _parameterService;
        private readonly ICustomerActivityService _customerActivityService;

        public ParametersTreeApiController(IParameterService parameterService,
            ICustomerActivityService customerActivityService)
        {
            this._parameterService = parameterService;
            this._customerActivityService = customerActivityService;
        }

        // GET: api/Parameters
        public IHttpActionResult Get()
        {
            var tree = SortForTree();

            return Json(tree);
        }

        #region 其它方法 

        private List<TreeViewModel> SortForTree(int? parentId = null)
        {
            var model = new List<TreeViewModel>();
            foreach (var p in _parameterService.FindAll(a => a.ParentId == parentId && a.IsLeaf == null).OrderBy(a => a.OrderNo))
            {
                var pm = new TreeViewModel
                {
                    id = p.Id,
                    text = p.Name,
                    parentId = p.ParentId.GetValueOrDefault(),
                    value = p.Value,
                    systemName = p.SystemName,
                    rank = p.Rank
                };
                pm.items.AddRange(SortForTree(p.Id));
                pm.hasChildren = pm.items.Count > 0;
                model.Add(pm);
            }
            return model;
        }

        #endregion
    }
}

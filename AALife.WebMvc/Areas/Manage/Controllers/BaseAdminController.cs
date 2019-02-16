using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    [Authorize]
    public abstract partial class BaseAdminController : BaseController
    {

    }
}
using AALife.Core.Infrastructure;
using AALife.Data.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.WebPages;

namespace AALife.WebMvc
{
    public static class HtmlExtensions
    {
      
        /// <summary>
        /// 生成按钮
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="areaName"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static MvcHtmlString CreateButton(this HtmlHelper helper, string areaName, string controllerName, string actionName, int userId)
        {
            var permissionService = EngineContext.Current.Resolve<IUserPermissionService>();

            var parent = permissionService.Find(a => a.AreaName == areaName && a.ControllerName == controllerName && a.ActionName == actionName);

            if(parent != null)
            {
                var permissions = permissionService.FindAll(a => a.ParentId == parent.Id);

                if (permissions.Any())
                {
                    var stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("<div class=\"k-header k-grid-toolbar\">");
                    permissions.ToList().ForEach(a=> 
                    {
                        stringBuilder.AppendFormat("<a role=\"button\" class=\"k-button k-button-icontext k-grid-{0}\" href=\"javascript:k_func_{0}();\"><span class=\"k-icon {1}\"></span>{2}</a>", a.ActionName.ToLower(), a.IconName, a.Name);
                    });
                    stringBuilder.AppendLine("</div>");

                    return MvcHtmlString.Create(stringBuilder.ToString());
                }
            }

            return MvcHtmlString.Create("");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class SexSpider_GetListData4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Business.SiteService service = new Business.SiteService();

        StringBuilder result = new StringBuilder();

        //max siteid 61

        result.Append("{");

        //网站列表
        result.Append("\"site_list\":");
        result.Append(service.GetSiteList());
        
        //外部词典
        result.Append(",\"ext_dic\":");
        result.Append(service.GetExtDic());

        //停止词典
        result.Append(",\"stop_dic\":");        
        result.Append(service.GetStopDic());

        //删除词典
        result.Append(",\"del_dic\":[]");

        result.Append("}");

        Response.Write(result.ToString());
        Response.End();
    }
    
}
using AALife.BLL;
using NLog;
using System;
using System.Data;

public partial class AALifeWeb_SyncGetItemListWeb : SyncBase
{
    private ItemTableBLL bll = new ItemTableBLL();
    private UserTableBLL user_bll = new UserTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Request.Form["userid"]);
        int type = Convert.ToInt32(Request.Form["type"]);

        //写日志
        log.Info(string.Format(" UserID:{0} | Type:{1}", userId, type));

        DataTable dt = new DataTable();
        dt = bll.GetItemListWithSync(userId);
        /*using (TransactionScope ts = new TransactionScope())
        {
            if (type == 1)
            {
                user_bll.UpdateSyncByUserId(userId);
            }

            dt = bll.GetItemListWithSync(userId);

            ts.Complete();
        }*/

        string result = "{";
        if (dt.Rows.Count > 0)
        {
            result += "\"itemlist\":[";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result += "{\"itemid\":\"" + dt.Rows[i]["ItemID"].ToString() + "\",";
                result += "\"itemappid\":\"" + dt.Rows[i]["ItemAppID"].ToString() + "\",";
                result += "\"itemname\":\"" + dt.Rows[i]["ItemName"].ToString() + "\",";
                result += "\"catid\":\"" + dt.Rows[i]["CategoryTypeID"].ToString() + "\",";
                result += "\"itemprice\":\"" + dt.Rows[i]["ItemPrice"].ToString() + "\",";
                result += "\"itembuydate\":\"" + Convert.ToDateTime(dt.Rows[i]["ItemBuyDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "\",";
                result += "\"recommend\":\"" + dt.Rows[i]["Recommend"].ToString() + "\",";
                result += "\"regionid\":\"" + dt.Rows[i]["RegionID"].ToString() + "\",";
                result += "\"regiontype\":\"" + dt.Rows[i]["RegionType"].ToString() + "\",";
                result += "\"itemtype\":\"" + dt.Rows[i]["ItemType"].ToString() + "\",";
                result += "\"ztid\":\"" + dt.Rows[i]["ZhuanTiID"].ToString() + "\",";
                result += "\"cardid\":\"" + dt.Rows[i]["CardID"].ToString() + "\",";
                result += "\"remark\":\"" + dt.Rows[i]["Remark"].ToString() + "\"},";
            }
            result = result.Substring(0, result.Length - 1);
            result += "]";
        }
        else
        {
            result += "\"itemlist\":[]";
        }
        result += "}";

        Response.Write(result);
        Response.End();
    }
}
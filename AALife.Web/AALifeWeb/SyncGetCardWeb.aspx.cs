using AALife.BLL;
using System;
using System.Data;

public partial class AALifeWeb_SyncGetCardWeb : System.Web.UI.Page
{
    private CardTableBLL bll = new CardTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Request.Form["userid"]);
        string isUpdate = Request.Form["isupdate"] ?? "0";

        DataTable dt = bll.GetCardListWithSync(userId);

        string result = "{";
        if (dt.Rows.Count > 0)
        {
            result += "\"cardlist\":[";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string cardMoney = dt.Rows[i]["CardMoney"].ToString();
                if (isUpdate == "1")
                {
                    cardMoney = dt.Rows[i]["MoneyStart"].ToString();
                }

                result += "{\"cardid\":\"" + dt.Rows[i]["CDID"].ToString() + "\",";
                result += "\"cardname\":\"" + dt.Rows[i]["CardName"].ToString() + "\",";
                result += "\"cardmoney\":\"" + cardMoney + "\",";
                result += "\"cardlive\":\"" + dt.Rows[i]["CardLive"].ToString() + "\"},";
            }
            result = result.Substring(0, result.Length - 1);
            result += "]";
        }
        else
        {
            result += "\"cardlist\":[]";
        }
        result += "}";

        Response.Write(result);
        Response.End();
    }
}
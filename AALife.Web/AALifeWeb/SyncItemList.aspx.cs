using AALife.BLL;
using AALife.Model;
using NLog;
using System;
using System.Data;

public partial class AALifeWeb_SyncItemList : System.Web.UI.Page
{
    public static Logger log = LogManager.GetCurrentClassLogger();
    private ItemTableBLL bll = new ItemTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        string itemName = Request.Form["itemname"].ToString();
        int itemAppId = Convert.ToInt32(Request.Form["itemid"]);
        int itemId = Convert.ToInt32(Request.Form["itemwebid"]);
        int catId = Convert.ToInt32(Request.Form["catid"]);
        decimal itemPrice = Convert.ToDecimal(Request.Form["itemprice"]);
        DateTime itemBuyDate = Convert.ToDateTime(Request.Form["itembuydate"]);
        int userId = Convert.ToInt32(Request.Form["userid"]);
        byte recommend = Convert.ToByte(Request.Form["recommend"]);
        int regionId = Convert.ToInt32(Request.Form["regionid"]);
        string regionType = (regionId == 0 ? "" : Request.Form["regiontype"] ?? "m");
        string itemType = Request.Form["itemtype"].ToString();
        string ztId = Request.Form["ztid"] ?? "0";
        string cardId = Request.Form["cardid"] ?? "0";

        ItemInfo item = bll.GetItemByItemAppId(userId, itemAppId);
        item.ItemType = itemType;
        item.ItemName = itemName;
        item.CategoryTypeID = catId;
        item.ItemPrice = itemPrice;
        item.ItemBuyDate = itemBuyDate;
        item.ItemAppID = itemAppId;
        item.Recommend = recommend;
        item.RegionID = regionId;
        item.RegionType = regionType;
        item.Synchronize = 0;
        item.UserID = userId;
        item.ZhuanTiID = Convert.ToInt32(ztId);
        item.CardID = Convert.ToInt32(cardId);
        item.ModifyDate = DateTime.Now;

        //写日志
        log.Info(string.Format(" ItemInfo -> {0}", item.ToString()));

        bool success = false;
        if (item.ItemID > 0)
        {
            success = bll.UpdateItemByItemAppId(item);
        }
        else if (itemId > 0)
        {
            item = bll.GetItemByItemId(itemId);
            item.ItemType = itemType;
            item.ItemName = itemName;
            item.CategoryTypeID = catId;
            item.ItemPrice = itemPrice;
            item.ItemBuyDate = itemBuyDate;
            item.ItemAppID = itemAppId;
            item.Recommend = recommend;
            item.RegionID = regionId;
            item.RegionType = regionType;
            item.Synchronize = 0;
            item.UserID = userId;
            item.ZhuanTiID = Convert.ToInt32(ztId);
            item.CardID = Convert.ToInt32(cardId);
            item.ModifyDate = DateTime.Now;

            if (item.ItemID > 0)
            {
                success = bll.UpdateItemWithSync(item);
            }
            else
            {
                success = bll.InsertItemWithSync(item);
            }
        }
        else
        {
            success = bll.InsertItemWithSync(item);
        }

        string result = "{";
        if (success)
        {
            result += "\"result\":\"" + item.ItemID + "\"";
        }
        else
        {
            result += "\"result\":\"0\"";
        } 
        result += "}";

        Response.Write(result);
        Response.End();
    }
}
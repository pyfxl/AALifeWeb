using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;
using AALife.BLL;
using AALife.Model;

public partial class Manage_BackupData : AdminPage
{
    private ItemTableBLL bll = new ItemTableBLL();
    private UserCategoryTableBLL cat_bll = new UserCategoryTableBLL();
    private CardTableBLL card_bll = new CardTableBLL();
    private UserTableBLL user_bll = new UserTableBLL();
    private OAuthTableBLL oauth_bll = new OAuthTableBLL();
    private ZhuanTiTableBLL zt_bll = new ZhuanTiTableBLL();
    private ZhuanZhangTableBLL zz_bll = new ZhuanZhangTableBLL();
    private DeleteTableBLL del_bll = new DeleteTableBLL();
        
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    
    //导出数据到App
    protected void Button1_Click(object sender, EventArgs e)
    {
        string strUserId = this.UserIDBox.Text.Trim();
        if (strUserId == "")
        {
            Utility.Alert(this, "用户ID不能为空！");
            return;
        }

        int userId = Convert.ToInt32(strUserId);
        UserInfo user = user_bll.GetUserByUserId(userId);
        string fileName = "aalife2(" + user.UserID + user.UserName + ").bak";

        StringBuilder sb = new StringBuilder();

        //备份用户表
        if (user.UserID > 0)
        {
            decimal userMoney = user.UserMoney;
            if (user.IsUpdate == 1)
            {
                userMoney = user.MoneyStart;
            }

            string result = "{";
            result += "\"userid\":\"" + user.UserID + "\",";
            result += "\"username\":\"" + user.UserName + "\",";
            result += "\"userpass\":\"" + user.UserPassword + "\",";
            result += "\"usernickname\":\"" + user.UserNickName + "\",";
            result += "\"createdate\":\"" + user.CreateDate.ToString("yyyy-MM-dd") + "\",";
            result += "\"useremail\":\"" + user.UserEmail + "\",";
            result += "\"userphone\":\"" + user.UserPhone + "\",";
            result += "\"userimage\":\"" + user.UserImage + "\",";
            result += "\"userworkday\":\"" + user.UserWorkDay + "\",";
            result += "\"usermoney\":\"" + userMoney + "\",";
            result += "\"categoryrate\":\"" + user.CategoryRate + "\",";
            result += "\"login\":\"true\",";

            OAuthInfo oauth = oauth_bll.GetOAuthByUserId(user.UserID);
            if (oauth.OAuthBound == 0)
            {
                result += "\"userbound\":\"0\"";
            }
            else
            {
                result += "\"userbound\":\"1\"";
            }

            result += "}";

            sb.AppendLine(result);
            sb.AppendLine("");
        }

        //备份消费表
        DataTable itemListTab = bll.GetItemListByUserId(userId);
        if (itemListTab.Rows.Count > 0)
        {
            sb.AppendLine("DELETE FROM ItemTable;");
            for (int i = itemListTab.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = itemListTab.Rows[i];
                sb.AppendLine("INSERT INTO ItemTable (ItemWebID, ItemName, ItemPrice, ItemBuyDate, CategoryID, Recommend, Synchronize, RegionID, RegionType, ItemType, ZhuanTiID, CardID) VALUES ('" +
                                dr["ItemID"].ToString() + "', '" +
                                Utility.ReplaceSql(dr["ItemName"].ToString()) + "', '" +
                                dr["ItemPrice"].ToString() + "', '" +
                                String.Format("{0:yyyy-MM-dd}", dr["ItemBuyDate"]) + "', '" +
                                dr["CategoryTypeID"].ToString() + "', '" +
                                dr["Recommend"].ToString() + "', '0', '" +
                                dr["RegionID"].ToString() + "', '" +
                                dr["RegionType"].ToString() + "', '" +
                                dr["ItemType"].ToString() + "', '" +
                                dr["ZhuanTiID"].ToString() + "', '" +
                                dr["CardID"].ToString() + "');");
            }
            sb.AppendLine("");
        }

        //备份类别表
        DataTable catListTab = cat_bll.GetUserCategoryList(userId);
        if (catListTab.Rows.Count > 0)
        {
            sb.AppendLine("DELETE FROM CategoryTable;");
            foreach (DataRow dr in catListTab.Rows)
            {
                sb.AppendLine("INSERT INTO CategoryTable (CategoryID, CategoryName, CategoryPrice, CategoryRank, CategoryDisplay, CategoryLive, Synchronize) VALUES ('" +
                                dr["CategoryTypeID"].ToString() + "', '" +
                                Utility.ReplaceSql(dr["CategoryTypeName"].ToString()) + "', '" +
                                dr["CategoryTypePrice"].ToString() + "', '" +
                                dr["CategoryTypeID"].ToString() + "', '" +
                                dr["CategoryTypeLive"].ToString() + "', '" +
                                dr["CategoryTypeLive"].ToString() + "', '0');");
            }
            sb.AppendLine("");
        }

        //备份专题表
        DataTable zhuanTiTab = zt_bll.GetZhuanTiList(userId);
        if (zhuanTiTab.Rows.Count > 0)
        {
            sb.AppendLine("DELETE FROM ZhuanTiTable;");
            foreach (DataRow dr in zhuanTiTab.Rows)
            {
                sb.AppendLine("INSERT INTO ZhuanTiTable (ZTID, ZhuanTiName, ZhuanTiImage, ZhuanTiLive, Synchronize) VALUES ('" +
                                dr["ZTID"].ToString() + "', '" +
                                Utility.ReplaceSql(dr["ZhuanTiName"].ToString()) + "', '" +
                                dr["ZhuanTiImage"].ToString() + "', '" +
                                dr["ZhuanTiLive"].ToString() + "', '0');");
            }
            sb.AppendLine("");
        }

        //备份转账表
        DataTable zhangTab = zz_bll.GetZhuanZhangList(userId);
        if (zhangTab.Rows.Count > 0)
        {
            sb.AppendLine("DELETE FROM ZhuanZhangTable;");
            foreach (DataRow dr in zhangTab.Rows)
            {
                sb.AppendLine("INSERT INTO ZhuanZhangTable (ZZID, ZhangFrom, ZhangTo, ZhangMoney, ZhangDate, ZhangNote, ZhangLive, Synchronize) VALUES ('" +
                                dr["ZZID"].ToString() + "', '" +
                                dr["ZhuanZhangFrom"].ToString() + "', '" +
                                dr["ZhuanZhangTo"].ToString() + "', '" +
                                dr["ZhuanZhangMoney"].ToString() + "', '" +
                                String.Format("{0:yyyy-MM-dd}", dr["ZhuanZhangDate"]) + "', '" +
                                Utility.ReplaceSql(dr["ZhuanZhangNote"].ToString()) + "', '" +
                                dr["ZhuanZhangLive"].ToString() + "', '0');");
            }
            sb.AppendLine("");
        }

        //备份钱包表
        DataTable cardTab = card_bll.GetCardList(userId);
        if (cardTab.Rows.Count > 0)
        {
            sb.AppendLine("DELETE FROM CardTable;");
            foreach (DataRow dr in cardTab.Rows)
            {
                if (dr["CardID"].ToString() == "0") 
                { 
                    continue; 
                }

                string cardMoney = dr["CardMoney"].ToString();
                if (user.IsUpdate == 1)
                {
                    cardMoney = dr["MoneyStart"].ToString();
                }

                sb.AppendLine("INSERT INTO CardTable (CDID, CardName, CardMoney, CardLive, Synchronize) VALUES ('" +
                                dr["CDID"].ToString() + "', '" +
                                Utility.ReplaceSql(dr["CardName"].ToString()) + "', '" +
                                cardMoney + "', '" +
                                dr["CardLive"].ToString() + "', '0');");
            }
        }

        string pathFileName = GetFilePathName(fileName);
        BackupHelper.WriteBackupFile(pathFileName, sb.ToString());

        bll.UpdateItemListWebBackByUserId(userId);

        cat_bll.UpdateCategoryListWebBack(userId);

        zt_bll.UpdateZhuanTiListWebBack(userId);

        card_bll.UpdateCardListWebBack(userId);

        DownBackFile(pathFileName, fileName);
    }

    //下载备份
    private void DownBackFile(string pathName, string fileName)
    {
        FileStream fileStream = new FileStream(pathName, FileMode.Open);
        long fileSize = fileStream.Length;
        Context.Response.ContentType = "application/octet-stream";
        //中文文件名需要UTF8编码
        Context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8) + "\"");
        Context.Response.AddHeader("Content-Length", fileSize.ToString());
        byte[] fileBuffer = new byte[fileSize];
        fileStream.Read(fileBuffer, 0, (int)fileSize);
        fileStream.Close();
        Context.Response.BinaryWrite(fileBuffer);
        Context.Response.End();
    }

    //取备份文件名
    private string GetFilePathName(string fileName)
    {
        string filePath = Server.MapPath("/Backup/Cloud") + System.IO.Path.DirectorySeparatorChar;
        return filePath + fileName;
    }

    //取备份文件名
    private string GetFileBakName(string fileName)
    {
        string filePath = Server.MapPath("/Backup/Bak") + System.IO.Path.DirectorySeparatorChar;
        return filePath + fileName;
    }

    //删除用户数据
    protected void Button3_Click(object sender, EventArgs e)
    {
        string strUserId = this.UserIDBox.Text.Trim();
        if (strUserId == "")
        {
            Utility.Alert(this, "用户ID不能为空！");
            return;
        }

        int userId = Convert.ToInt32(strUserId);
        bool success = user_bll.DeleteUserData(userId);
        if (success)
        {
            Utility.Alert(this, "删除成功。");
        }
        else
        {
            Utility.Alert(this, "删除失败！");
            return;
        }
    }

    //恢复APP数据
    protected void Button5_Click(object sender, EventArgs e)
    {
        string strUserId = this.UserIDBox.Text.Trim();
        if (strUserId == "")
        {
            Utility.Alert(this, "用户ID不能为空！");
            return;
        }

        int userId = Convert.ToInt32(strUserId);
        UserInfo user = user_bll.GetUserByUserId(userId);
        string fileName = "aalife2(" + user.UserID + user.UserName + ").bak";

        string filePathName = GetFilePathName(fileName);
        if (!File.Exists(filePathName))
        {
            Utility.Alert(this, "文件不存在！（" + fileName + "）");
            return;
        }

        try
        {
            BackupHelper.ReaderBackupFileFromApp(userId, filePathName);
            Utility.Alert(this, "恢复成功。");
        }
        catch(Exception ex)
        {
            Utility.Alert(this, "恢复失败！");
            return;
        }
    }

}
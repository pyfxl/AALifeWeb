using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;
using AALife.BLL;
using AALife.Model;

public partial class AdminBackupData : AdminPage
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
            this.BeginDateBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.EndDateBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    //备份数据
    protected void Button2_Command(object sender, CommandEventArgs e)
    {
        DateTime beginDate = Convert.ToDateTime(this.BeginDateBox.Text.Trim()).Date;
        DateTime endDate = Convert.ToDateTime(this.EndDateBox.Text.Trim()).AddDays(1).Date;

        string fileName = beginDate.ToString("yyyy-MM-dd") + "~" + endDate.AddDays(-1).ToString("yyyy-MM-dd") + "-bak.sql";
        string type = e.CommandArgument.ToString();
        if (type == "mysql")
        {
            fileName = "mysql-" + fileName;
        }

        StringBuilder sb = new StringBuilder();
        
        DataTable itemListTab = bll.GetItemListByDate(beginDate, endDate);
        if (itemListTab.Rows.Count > 0)
        {
            sb.AppendLine("--备份消费表");
            sb.Append("DELETE FROM ItemTable WHERE ItemID IN (");
            foreach (DataRow dr in itemListTab.Rows)
            {
                sb.Append(dr["ItemID"].ToString() + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.AppendLine(")");

            if (type == "mssql")
            {
                sb.AppendLine("SET IDENTITY_INSERT ItemTable ON");
            }
            foreach (DataRow dr in itemListTab.Rows)
            {
                sb.AppendLine("INSERT INTO ItemTable (ItemID, ItemType, ItemName, CategoryTypeID, ItemPrice, ItemBuyDate, UserID, ModifyDate, Recommend, Synchronize, RegionID, RegionType, ZhuanTiID, CardID) VALUES (" +
                                dr["ItemID"].ToString() + ", '" +
                                dr["ItemType"].ToString() + "', '" +
                                Utility.ReplaceSql(dr["ItemName"].ToString()) + "', '" +
                                dr["CategoryTypeID"].ToString() + "', '" +
                                dr["ItemPrice"].ToString() + "', '" +
                                String.Format("{0:yyyy-MM-dd HH:mm:ss}", dr["ItemBuyDate"].ToString()) + "', '" +
                                dr["UserID"].ToString() + "', '" +
                                String.Format("{0:yyyy-MM-dd HH:mm:ss}", dr["ModifyDate"].ToString()) + "', '" +
                                dr["Recommend"].ToString() + "', '" +
                                dr["Synchronize"].ToString() + "', '" +
                                dr["RegionID"].ToString() + "', '" +
                                dr["RegionType"].ToString() + "', '" +
                                dr["ZhuanTiID"].ToString() + "', '" +
                                dr["CardID"].ToString() + "')");
            }
            if (type == "mssql")
            {
                sb.AppendLine("SET IDENTITY_INSERT ItemTable OFF");
            }
            sb.AppendLine("");
        }

        DataTable userCatTypeListTab = cat_bll.GetUserCategoryListByDate(beginDate, endDate);
        if (userCatTypeListTab.Rows.Count > 0)
        {
            sb.AppendLine("--备份类别表");
            sb.Append("DELETE FROM UserCategoryTable WHERE UserCategoryID IN (");
            foreach (DataRow dr in userCatTypeListTab.Rows)
            {
                sb.Append(dr["UserCategoryID"].ToString() + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.AppendLine(")");

            if (type == "mssql")
            {
                sb.AppendLine("SET IDENTITY_INSERT UserCategoryTable ON");
            }
            foreach (DataRow dr in userCatTypeListTab.Rows)
            {
                sb.AppendLine("INSERT INTO UserCategoryTable (UserCategoryID, CategoryTypeName, CategoryTypePrice, UserID, CategoryTypeID, Synchronize, CategoryTypeLive, ModifyDate) VALUES (" +
                                dr["UserCategoryID"].ToString() + ", '" +
                                Utility.ReplaceSql(dr["CategoryTypeName"].ToString()) + "', '" +
                                dr["CategoryTypePrice"].ToString() + "', '" +
                                dr["UserID"].ToString() + "', '" +
                                dr["CategoryTypeID"].ToString() + "', '" +
                                dr["Synchronize"].ToString() + "', '" +
                                dr["CategoryTypeLive"].ToString() + "', '" +
                                String.Format("{0:yyyy-MM-dd HH:mm:ss}", dr["ModifyDate"].ToString()) + "')");
            }
            if (type == "mssql")
            {
                sb.AppendLine("SET IDENTITY_INSERT UserCategoryTable OFF");
            }
            sb.AppendLine("");
        }

        DataTable zhuanTiListTab = zt_bll.GetZhuanTiListByDate(beginDate, endDate);
        if (zhuanTiListTab.Rows.Count > 0)
        {
            sb.AppendLine("--备份专题表");
            sb.Append("DELETE FROM ZhuanTiTable WHERE ZhuanTiID IN (");
            foreach (DataRow dr in zhuanTiListTab.Rows)
            {
                sb.Append(dr["ZhuanTiID"].ToString() + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.AppendLine(")");

            if (type == "mssql")
            {
                sb.AppendLine("SET IDENTITY_INSERT ZhuanTiTable ON");
            }
            foreach (DataRow dr in zhuanTiListTab.Rows)
            {
                sb.AppendLine("INSERT INTO ZhuanTiTable (ZhuanTiID, ZhuanTiName, UserID, ZTID, ZhuanTiImage, Synchronize, ZhuanTiLive, ModifyDate) VALUES (" +
                                dr["ZhuanTiID"].ToString() + ", '" +
                                Utility.ReplaceSql(dr["ZhuanTiName"].ToString()) + "', '" +
                                dr["UserID"].ToString() + "', '" +
                                dr["ZTID"].ToString() + "', '" +
                                dr["ZhuanTiImage"].ToString() + "', '" +
                                dr["Synchronize"].ToString() + "', '" +
                                dr["ZhuanTiLive"].ToString() + "', '" +
                                String.Format("{0:yyyy-MM-dd HH:mm:ss}", dr["ModifyDate"].ToString()) + "')");
            }
            if (type == "mssql")
            {
                sb.AppendLine("SET IDENTITY_INSERT ZhuanTiTable OFF");
            }
            sb.AppendLine("");
        }

        DataTable zhangListTab = zz_bll.GetZhuanZhangListByDate(beginDate, endDate);
        if (zhangListTab.Rows.Count > 0)
        {
            sb.AppendLine("--备份转账表");
            sb.Append("DELETE FROM ZhuanZhangTable WHERE ZhuanZhangID IN (");
            foreach (DataRow dr in zhangListTab.Rows)
            {
                sb.Append(dr["ZhuanZhangID"].ToString() + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.AppendLine(")");

            if (type == "mssql")
            {
                sb.AppendLine("SET IDENTITY_INSERT ZhuanZhangTable ON");
            }
            foreach (DataRow dr in zhangListTab.Rows)
            {
                sb.AppendLine("INSERT INTO ZhuanZhangTable (ZhuanZhangID, ZhuanZhangFrom, ZhuanZhangTo, ZhuanZhangMoney, ZhuanZhangDate, ZhuanZhangNote, UserID, Synchronize, ZhuanZhangLive, ZZID, ModifyDate) VALUES (" +
                                dr["ZhuanZhangID"].ToString() + ", '" +
                                dr["ZhuanZhangFrom"].ToString() + "', '" +
                                dr["ZhuanZhangTo"].ToString() + "', '" +
                                dr["ZhuanZhangMoney"].ToString() + "', '" +
                                String.Format("{0:yyyy-MM-dd HH:mm:ss}", dr["ZhuanZhangDate"].ToString()) + "', '" +
                                Utility.ReplaceSql(dr["ZhuanZhangNote"].ToString()) + "', '" +
                                dr["UserID"].ToString() + "', '" +
                                dr["Synchronize"].ToString() + "', '" +
                                dr["ZhuanZhangLive"].ToString() + "', '" +
                                dr["ZZID"].ToString() + "', '" +
                                String.Format("{0:yyyy-MM-dd HH:mm:ss}", dr["ModifyDate"].ToString()) + "')");
            }
            if (type == "mssql")
            {
                sb.AppendLine("SET IDENTITY_INSERT ZhuanZhangTable OFF");
            }
            sb.AppendLine("");
        }

        DataTable cardListTab = card_bll.GetCardListByDate(beginDate, endDate);
        if (cardListTab.Rows.Count > 0)
        {
            sb.AppendLine("--备份钱包表");
            sb.Append("DELETE FROM CardTable WHERE CardID IN (");
            foreach (DataRow dr in cardListTab.Rows)
            {
                sb.Append(dr["CardID"].ToString() + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.AppendLine(")");

            if (type == "mssql")
            {
                sb.AppendLine("SET IDENTITY_INSERT CardTable ON");
            }
            foreach (DataRow dr in cardListTab.Rows)
            {
                sb.AppendLine("INSERT INTO CardTable (CardID, CardName, UserID, CDID, CardMoney, MoneyStart, CardNumber, CardImage, Synchronize, CardLive, ModifyDate) VALUES (" +
                                dr["CardID"].ToString() + ", '" +
                                Utility.ReplaceSql(dr["CardName"].ToString()) + "', '" +
                                dr["UserID"].ToString() + "', '" +
                                dr["CDID"].ToString() + "', '" +
                                dr["CardMoney"].ToString() + "', '" +
                                dr["MoneyStart"].ToString() + "', '" +
                                dr["CardNumber"].ToString() + "', '" +
                                dr["CardImage"].ToString() + "', '" +
                                dr["Synchronize"].ToString() + "', '" +
                                dr["CardLive"].ToString() + "', '" +
                                String.Format("{0:yyyy-MM-dd HH:mm:ss}", dr["ModifyDate"].ToString()) + "')");
            }
            if (type == "mssql")
            {
                sb.AppendLine("SET IDENTITY_INSERT CardTable OFF");
            }
            sb.AppendLine("");
        }

        DataTable oauthListTab = oauth_bll.GetOAuthListByDate(beginDate, endDate);
        if (oauthListTab.Rows.Count > 0)
        {
            sb.AppendLine("--备份第三方登录表");
            sb.Append("DELETE FROM OAuthTable WHERE OAuthID IN (");
            foreach (DataRow dr in oauthListTab.Rows)
            {
                sb.Append(dr["OAuthID"].ToString() + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.AppendLine(")");

            if (type == "mssql")
            {
                sb.AppendLine("SET IDENTITY_INSERT OAuthTable ON");
            }
            foreach (DataRow dr in oauthListTab.Rows)
            {
                sb.AppendLine("INSERT INTO OAuthTable (OAuthID, OpenID, AccessToken, UserID, OldUserID, OAuthBound, OAuthFrom, ModifyDate) VALUES (" +
                                dr["OAuthID"].ToString() + ", '" +
                                dr["OpenID"].ToString() + "', '" +
                                dr["AccessToken"].ToString() + "', '" +
                                dr["UserID"].ToString() + "', '" +
                                dr["OldUserID"].ToString() + "', '" +
                                dr["OAuthBound"].ToString() + "', '" +
                                dr["OAuthFrom"].ToString() + "', '" +
                                String.Format("{0:yyyy-MM-dd HH:mm:ss}", dr["ModifyDate"].ToString()) + "')");
            }
            if (type == "mssql")
            {
                sb.AppendLine("SET IDENTITY_INSERT OAuthTable OFF");
            }
            sb.AppendLine("");
        }

        DataTable userListTab = user_bll.GetUserListByDate(beginDate, endDate);
        if (userListTab.Rows.Count > 0)
        {
            sb.AppendLine("--备份用户表");
            sb.Append("DELETE FROM UserTable WHERE UserID IN (");
            foreach (DataRow dr in userListTab.Rows)
            {
                sb.Append(dr["UserID"].ToString() + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.AppendLine(")");

            if (type == "mssql")
            {
                sb.AppendLine("SET IDENTITY_INSERT UserTable ON");
            }
            foreach (DataRow dr in userListTab.Rows)
            {
                sb.AppendLine("INSERT INTO UserTable (UserID, UserName, UserPassword, UserNickName, UserImage, UserCity, UserMoney, MoneyStart, UserWorkDay, CategoryRate, UserFunction, CreateDate, ModifyDate, UserPhone, UserEmail, UserTheme, UserLevel, UserFrom) VALUES (" +
                                dr["UserID"].ToString() + ", '" +
                                Utility.ReplaceSql(dr["UserName"].ToString()) + "', '" +
                                Utility.ReplaceSql(dr["UserPassword"].ToString()) + "', '" +
                                Utility.ReplaceSql(dr["UserNickName"].ToString()) + "', '" +
                                dr["UserImage"].ToString() + "', '" +
                                dr["UserCity"].ToString() + "', '" +
                                dr["UserMoney"].ToString() + "', '" +
                                dr["MoneyStart"].ToString() + "', '" +
                                dr["UserWorkDay"].ToString() + "', '" +
                                dr["CategoryRate"].ToString() + "', '" +
                                dr["UserFunction"].ToString() + "', '" +
                                String.Format("{0:yyyy-MM-dd HH:mm:ss}", dr["CreateDate"]) + "', '" +
                                String.Format("{0:yyyy-MM-dd HH:mm:ss}", dr["ModifyDate"]) + "', '" +
                                dr["UserPhone"].ToString() + "', '" +
                                dr["UserEmail"].ToString() + "', '" +
                                dr["UserTheme"].ToString() + "', '" +
                                dr["UserLevel"].ToString() + "', '" +
                                dr["UserFrom"].ToString() + "')");
            }
            if (type == "mssql")
            {
                sb.AppendLine("SET IDENTITY_INSERT UserTable OFF");
            }
            sb.AppendLine("");
        }

        DataTable delListTab = del_bll.GetDeleteList();
        if (delListTab.Rows.Count > 0)
        {
            sb.AppendLine("--清除删除表");
            int index = 0;
            sb.Append("DELETE FROM ItemTable WHERE ItemID IN (");
            foreach (DataRow dr in delListTab.Rows)
            {
                index++;
                sb.Append(dr["ItemID"].ToString());
                if (index != delListTab.Rows.Count)
                {
                    sb.Append(", ");
                }
            }
            sb.Append(")");
        }

        string pathFileName = GetFileBakName(fileName);
        BackupHelper.WriteBackupFile(pathFileName, sb.ToString());

        DownBackFile(pathFileName, fileName);
    }

    //恢复数据
    protected void Button4_Click(object sender, EventArgs e)
    {
        string beginDate = this.BeginDateBox.Text.Trim();
        string endDate = this.EndDateBox.Text.Trim();

        string fileName = beginDate + "~" + endDate + "-bak.sql";
        string filePathName = GetFileBakName(fileName);
        if (!File.Exists(filePathName))
        {
            Utility.Alert(this, "文件不存在！（" + fileName + "）");
            return;
        }

        try 
        {
            BackupHelper.ReaderBackupFile(filePathName);
            Utility.Alert(this, "恢复成功。");
        }
        catch
        {
            Utility.Alert(this, "恢复失败！");
            return;
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
        catch
        {
            Utility.Alert(this, "恢复失败！");
            return;
        }
    }

}
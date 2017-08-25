using System;
using System.Data;
using System.Web;
using System.IO;
using Aspose.Cells;
using System.Data.Common;
using System.Text;
using AALife.BLL;
using AALife.Model;

public partial class UserDataAdmin : WebPage
{
    private ItemTableBLL bll = new ItemTableBLL();
    private UserCategoryTableBLL cat_bll = new UserCategoryTableBLL();
    private ZhuanTiTableBLL zt_bll = new ZhuanTiTableBLL();
    private CardTableBLL card_bll = new CardTableBLL();
    private int userId = 0;
    private string userName = "";
    private DataTable all = new DataTable();
    private DataTable itemTypeList = new DataTable();
    private DataTable catTypeList = new DataTable();
    private DataTable zhuanTiList = new DataTable();
    private DataTable cardList = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        userName = Session["UserName"].ToString();
        itemTypeList = bll.GetItemTypeList();
        catTypeList = cat_bll.GetUserCategoryList(userId);
        zhuanTiList = zt_bll.GetZhuanTiList(userId);
        cardList = card_bll.GetCardList(userId);

        if (!IsPostBack)
        {
            this.ItemBuyDate1.Attributes.Add("readonly", "readonly");
            this.ItemBuyDate2.Attributes.Add("readonly", "readonly");
            
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string first = DateHelper.GetMonthFirst(DateTime.Now).ToString("yyyy-MM-dd");

            this.ItemBuyDate1.Text = first;
            this.ItemBuyDate2.Text = today;            
        }
    }
    
    //导出数据
    protected void Button2_Click(object sender, EventArgs e)
    {
        DateTime itemBuyDate1 = Convert.ToDateTime(this.ItemBuyDate1.Text);
        DateTime itemBuyDate2 = Convert.ToDateTime(this.ItemBuyDate2.Text);

        DataTable dt = bll.GetItemExportList(userId, itemBuyDate1, itemBuyDate2);
        string fileName = "AA生活记账数据导出(" + userId + userName + "_" + this.ItemBuyDate1.Text + "_" + this.ItemBuyDate2.Text + ").xlsx";
        string savePath = Server.MapPath("/Backup/Export/") + fileName;

        try
        {
            AsposeExport(dt, savePath);
            DownExcel(savePath, fileName);
        }
        catch 
        {
            Utility.Alert(this, "数据导出错误！");
        }
    }
    
    //导出下载
    private void DownExcel(string filePath, string fileName)
    {
        FileStream fileStream = new FileStream(filePath, FileMode.Open);
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

    //Aspose导出
    private void AsposeExport(DataTable dt, string savePath)
    {
        Workbook workbook = new Workbook();
        Worksheet sheet = workbook.Worksheets[0];
        Cells cells = sheet.Cells;

        Aspose.Cells.Style styleTitle = workbook.Styles[workbook.Styles.Add()];
        styleTitle.Font.IsBold = true;
        
        int colnums = dt.Columns.Count;
        int rownums = dt.Rows.Count;

        for (int i = 0; i < colnums; i++)
        {
            cells[0, i].PutValue(dt.Columns[i].ColumnName);
            cells[0, i].SetStyle(styleTitle);
            cells.SetColumnWidth(i, 18);
        }
                
        for (int i = 0; i < rownums; i++)
        {
            for (int j = 0; j < colnums; j++)
            {
                cells[1 + i, j].PutValue(dt.Rows[i][j].ToString());
            }
        }

        //Aspose.Cells.Style style = cells["D2"].GetStyle();
        //style.Number = 3;
        //cells["D2"].SetStyle(style);

        workbook.Save(savePath);
    }

    //模板下载
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("/Backup/") + "AA生活记账导入模板.xlsx";
        Workbook workbook = new Workbook(filePath);

        Worksheet sheet = workbook.Worksheets[1];
        for (int i = 0; i < catTypeList.Rows.Count; i++)
        {
            sheet.Cells[i, 1].PutValue(catTypeList.Rows[i]["CategoryTypeName"].ToString());
        }

        sheet = workbook.Worksheets[2];
        for (int i = 0; i < zhuanTiList.Rows.Count; i++)
        {
            sheet.Cells[i, 0].PutValue(zhuanTiList.Rows[i]["ZhuanTiName"].ToString());
        }

        sheet = workbook.Worksheets[3];
        for (int i = 0; i < cardList.Rows.Count; i++)
        {
            sheet.Cells[i, 0].PutValue(cardList.Rows[i]["CardName"].ToString());
        }

        string fileName = "AA生活记账导入模板(" + userId + userName + ").xlsx";
        string savePath = Server.MapPath("/Backup/Template/") + fileName;

        try
        {
            workbook.Save(savePath);
            DownExcel(savePath, fileName);
        }
        catch
        {
            Utility.Alert(this, "模板下载错误！");
        }
    }

    //导入数据
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.FileUpload1.HasFile == false)
        {
            Utility.Alert(this, "请选择导入的Excel文件！");
            return;
        }
        string fileName = FileUpload1.FileName;
        string fileExt = System.IO.Path.GetExtension(fileName).ToString().ToLower();
        if (fileExt != ".xls" && fileExt != ".xlsx")
        {
            Utility.Alert(this, "只可以导入Excel文件！");
            return;
        }

        fileName = "(" + userId + userName + ")" + fileName;
        string savePath = Server.MapPath("/Backup/Import/") + fileName;
        FileUpload1.SaveAs(savePath);
        DataTable dt = AsposeImport(savePath);

        if (dt.Rows.Count > 0)
        {
            try
            {
                if (!CheckExcel(dt))
                {
                    Utility.Alert(this, "导入的模板文件错误！");
                    return;
                }

                RemoveColumnsFlag(dt);

                int[] arr = new int[2];
                arr = SaveExcel(dt);
                Utility.Alert(this, string.Format("成功导入 {0} 条，重复的 {1} 条。", arr[0], arr[1]));
            }
            catch
            {
                Utility.Alert(this, "导入失败！");
                return;
            }
        }
        else
        {
            Utility.Alert(this, "没有要导入的数据！");
        }
    }

    //检查Excel文件
    private bool CheckExcel(DataTable dt)
    {
        return dt.Columns.Contains("分类 *") && dt.Columns.Contains("商品类别 *") && dt.Columns.Contains("商品名称 *") && dt.Columns.Contains("商品价格 *") && dt.Columns.Contains("购买日期 *") &&
               dt.Columns.Contains("推荐否") && dt.Columns.Contains("专题") && dt.Columns.Contains("钱包");
    }

    //Aspose导入
    private DataTable AsposeImport(string filePath)
    {
        Workbook workbook = new Workbook(filePath);
        Worksheet sheet = workbook.Worksheets[0];
        Cells cells = sheet.Cells;

        return cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, 8, true);
    }

    //导入保存
    private int[] SaveExcel(DataTable dt)
    {
        int[] arr = new int[2];
        all = bll.GetItemExportList(userId);

        string dbProviderName = WebConfiguration.DbProviderName;
        string dbConnectionString = WebConfiguration.DbConnectionString;
        DbProviderFactory factory = DbProviderFactories.GetFactory(dbProviderName);
        using (DbConnection conn = factory.CreateConnection())
        {
            conn.ConnectionString = dbConnectionString;
            conn.Open();
            DbCommand comm = conn.CreateCommand();
            comm.Connection = conn;

            try
            {
                int n1 = 0;
                int n2 = 0;
                string sql = "";
                foreach (DataRow dr in dt.Rows)
                {
                    string _itemType = GetItemTypeValue(dr["分类"].ToString());
                    string _itemName = dr["商品名称"].ToString();
                    int _catTypeId = GetCategoryTypeId(dr["商品类别"].ToString());
                    string _itemPrice = dr["商品价格"].ToString();
                    DateTime _itemBuyDate = Convert.ToDateTime(dr["购买日期"]);
                    int _recommend = (dr["推荐否"].ToString() == "是" ? 1 : 0);
                    int? _zhuanTiId = GetZhuanTiId(dr["专题"].ToString());
                    int? _cardId = GetCardId(dr["钱包"].ToString());

                    ItemInfo item = new ItemInfo();
                    item.ItemType = _itemType;
                    item.ItemName = _itemName;
                    item.CategoryTypeID = _catTypeId;
                    item.ItemPrice = (_itemPrice == "" ? 0 : Convert.ToDecimal(_itemPrice));
                    item.ItemBuyDate = _itemBuyDate;
                    item.Recommend = Convert.ToByte(_recommend);
                    item.ZhuanTiID = _zhuanTiId;
                    item.CardID = _cardId;
                    item.Synchronize = 1;
                    item.ModifyDate = DateTime.Now;

                    if (CheckRepeat(item))
                    {
                        n2++;
                        continue;
                    }
                    sql = "INSERT INTO ItemTable(ItemType, ItemName, CategoryTypeID, ItemPrice, ItemBuyDate, UserID, Recommend, ZhuanTiID, CardID, Synchronize, ModifyDate) VALUES('" +
                                item.ItemType + "','" + item.ItemName + "','" + item.CategoryTypeID + "','" + item.ItemPrice + "','" +
                                item.ItemBuyDate.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss") + "','" + userId + "','" + item.Recommend + "','" +
                                item.ZhuanTiID + "','" + item.CardID + "','" + item.Synchronize + "','" + item.ModifyDate + "');";
                    
                    comm.CommandText = sql;
                    comm.ExecuteNonQuery();
                    
                    n1++;
                }
                
                arr[0] = n1;
                arr[1] = n2;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        return arr;
    }

    //检查数据重复
    private bool CheckRepeatOld(ItemInfo _item)
    {
        foreach (DataRow dr in all.Rows)
        {
            string _itemName = dr["商品名称"].ToString();
            DateTime _itemBuyDate = Convert.ToDateTime(dr["购买日期"]);
            int _recommend = (dr["推荐否"].ToString() == "是" ? 1 : 0);
            int? _zhuanTiId = GetZhuanTiId(dr["专题"].ToString());
            int? _cardId = GetCardId(dr["钱包"].ToString());
            int _catTypeId = GetCategoryTypeId(dr["商品类别"].ToString());
            Decimal _itemPrice = Convert.ToDecimal(dr["商品价格"]);
            string _itemType = GetItemTypeValue(dr["分类"].ToString());

            ItemInfo item = new ItemInfo();
            item.ItemName = _itemName;
            item.ItemBuyDate = _itemBuyDate;
            item.Recommend = Convert.ToByte(_recommend);
            item.ZhuanTiID = _zhuanTiId;
            item.CardID = _cardId;
            item.CategoryTypeID = _catTypeId;
            item.ItemPrice = _itemPrice;
            item.ItemType = _itemType;

            if (item.Equals(_item))
            {
                return true;
            } 
        }

        return false;
    }

    //检查数据重复
    private bool CheckRepeat(ItemInfo _item)
    {
        return bll.CheckItemExists(_item);
    }

    //取类别ID
    private int GetCategoryTypeId(string catName)
    {
        catTypeList = cat_bll.GetUserCategoryList(userId);
        foreach (DataRow dr in catTypeList.Rows)
        {
            if (catName == dr["CategoryTypeName"].ToString())
            {
                return Convert.ToInt32(dr["CategoryTypeID"]);
            }
        }

        UserCategoryInfo category = new UserCategoryInfo();
        category.CategoryTypeID = cat_bll.GetMaxCategoryTypeId(userId);
        category.CategoryTypeName = catName;
        category.CategoryTypePrice = 0m;
        category.UserID = userId;
        category.CategoryTypeLive = 1;
        category.Synchronize = 1;
        category.ModifyDate = DateTime.Now;

        bool success = cat_bll.InsertUserCategory(category);
        if (success)
        {
            return category.CategoryTypeID;
        }
        else
        {
            throw new Exception();
        }
    }

    //取专题ID
    private int? GetZhuanTiId(string zhuanTiName)
    {
        if (zhuanTiName == "")
        {
            return 0;
        }
        
        zhuanTiList = zt_bll.GetZhuanTiList(userId);
        foreach (DataRow dr in zhuanTiList.Rows)
        {
            if (zhuanTiName == dr["ZhuanTiName"].ToString())
            {
                return Convert.ToInt32(dr["ZTID"]);
            }
        }

        ZhuanTiInfo zhuanTi = new ZhuanTiInfo();
        zhuanTi.ZhuanTiName = zhuanTiName;
        zhuanTi.ZhuanTiLive = 1;
        zhuanTi.Synchronize = 1;
        zhuanTi.UserID = userId;
        zhuanTi.ModifyDate = DateTime.Now;
        zhuanTi.ZTID = zt_bll.GetMaxZhuanTiId(userId);

        bool success = zt_bll.InsertZhuanTi(zhuanTi);
        if (success)
        {
            return zhuanTi.ZTID;
        }
        else
        {
            throw new Exception();
        }
    }

    //取钱包ID
    private int? GetCardId(string cardName)
    {
        if (cardName == "" || cardName == "我的钱包")
        {
            return 0;
        }

        cardList = card_bll.GetCardList(userId);
        foreach (DataRow dr in cardList.Rows)
        {
            if (cardName == dr["CardName"].ToString())
            {
                return Convert.ToInt32(dr["CDID"]);
            }
        }

        CardInfo card = new CardInfo();
        card.CardName = cardName;
        card.CardMoney = 0m;
        card.UserID = userId;
        card.CardLive = 1;
        card.Synchronize = 1;
        card.CDID = card_bll.GetMaxCardId(userId);
        card.ModifyDate = DateTime.Now;

        bool success = card_bll.InsertCard(card);
        if (success)
        {
            return card.CDID;
        }
        else
        {
            throw new Exception();
        }
    }
    
    //取支出收入分类
    private string GetItemTypeValue(string name)
    {
        foreach (DataRow dr in itemTypeList.Rows)
        {
            string _name = dr["ItemTypeName"].ToString();
            if (_name == name)
            {
                return dr["ItemType"].ToString();
            }
        }

        return "zc";
    }

    //去掉列的*号
    private void RemoveColumnsFlag(DataTable dt)
    {
        foreach (DataColumn dc in dt.Columns)
        {
            dc.ColumnName = dc.ColumnName.Replace("*", "").Trim();
        }
    }

}

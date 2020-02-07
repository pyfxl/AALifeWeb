using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

/// <summary>
/// MenuHelper 的摘要说明
/// </summary>
public class MenuHelper
{
    private List<MyCheckBox> lists = new List<MyCheckBox>();
    private DataTable query = new DataTable();

	public MenuHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    //初始默认值
    public void PopulateControls(string menu)
    {
        string[] arr = menu.Split(',');
        for (int i = 0; i < arr.Length; i++)
        {
            foreach (MyCheckBox cb in lists)
            {
                CheckBox checkbox = (CheckBox)cb.CheckBox;
                string value = cb.Ref;
                if (arr[i] == value) checkbox.Checked = true;
            }
        }
    }

    //取用户菜单
    public DataTable GetUserFunction(string menu)
    {
        DataTable menus = GetMenuData();
        DataTable dt = menus.Clone();

        string[] arr = menu.Split(',');
        for (int i = 0; i < arr.Length; i++)
        {
            foreach (DataRow dr in menus.Rows)
            {
                string value = dr["MenuID"].ToString();
                if (arr[i] == value) dt.Rows.Add(dr.ItemArray);
            }
        }

        return dt;
    }

    //取菜单datatable
    public DataTable GetMenuData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("MenuURL", typeof(string));
        dt.Columns.Add("MenuName", typeof(string));
        dt.Columns.Add("MenuID", typeof(string));
        dt.Columns.Add("MenuType", typeof(string));
        dt.Columns.Add("MenuLive", typeof(bool));
        dt.Columns.Add("MenuImage", typeof(string));

        DataRow dr = dt.NewRow();
        dr[0] = "Default.aspx";
        dr[1] = "首页";
        dr[2] = "23";
        dr[3] = "system";
        dr[4] = false;
        dr[5] = "home_icon.gif";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "ItemAddSmart.aspx";
        dr[1] = "添加消费";
        dr[2] = "24";
        dr[3] = "system";
        dr[4] = false;
        dr[5] = "ico_meet.gif";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "ItemQuery.aspx";
        dr[1] = "消费明细";
        dr[2] = "25";
        dr[3] = "system";
        dr[4] = false;
        dr[5] = "";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "ItemGroup.aspx";
        dr[1] = "消费统计";
        dr[2] = "26";
        dr[3] = "system";
        dr[4] = true;
        dr[5] = "";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "ItemCompare.aspx";
        dr[1] = "消费比较";
        dr[2] = "27";
        dr[3] = "system";
        dr[4] = true;
        dr[5] = "";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "UserAdmin.aspx";
        dr[1] = "用户资料";
        dr[2] = "13";
        dr[3] = "user";
        dr[4] = true;
        dr[5] = "48_icon08.gif";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "UserBoundAdmin.aspx";
        dr[1] = "绑定管理";
        dr[2] = "14";
        dr[3] = "user";
        dr[4] = true;
        dr[5] = "COMMUNITY_LABEL.gif";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "UserDataAdmin.aspx";
        dr[1] = "数据管理";
        dr[2] = "16";
        dr[3] = "user";
        dr[4] = true;
        dr[5] = "ico_software.gif";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "UserFunctionSetting.aspx";
        dr[1] = "菜单管理";
        dr[2] = "17";
        dr[3] = "user";
        dr[4] = true;
        dr[5] = "icon19.gif";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "UserCategoryAdmin.aspx";
        dr[1] = "类别管理";
        dr[2] = "15";
        dr[3] = "user";
        dr[4] = true;
        dr[5] = "dot_12.gif";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "UserZhuanTi.aspx";
        dr[1] = "用户专题";
        dr[2] = "20";
        dr[3] = "user";
        dr[4] = true;
        dr[5] = "i_hexa.gif";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "UserCardAdmin.aspx";
        dr[1] = "钱包管理";
        dr[2] = "22";
        dr[3] = "user";
        dr[4] = true;
        dr[5] = "icon_card2.gif";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "Helper.aspx";
        dr[1] = "用户声明";
        dr[2] = "21";
        dr[3] = "user";
        dr[4] = true;
        dr[5] = "ku1.gif";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "UserLogout.aspx";
        dr[1] = "用户退出";
        dr[2] = "19";
        dr[3] = "user";
        dr[4] = true;
        dr[5] = "f04.gif";
        dt.Rows.Add(dr);

        dt.Merge(query);

        return dt;
    }

    //取查询菜单
    public void SetQueryData(DataTable list)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("MenuURL", typeof(string));
        dt.Columns.Add("MenuName", typeof(string));
        dt.Columns.Add("MenuID", typeof(string));
        dt.Columns.Add("MenuType", typeof(string));
        dt.Columns.Add("MenuLive", typeof(bool));
        dt.Columns.Add("MenuImage", typeof(string));
        
        DataRow dr = dt.NewRow();
        foreach (DataRow row in list.Rows)
        {
            dr = dt.NewRow();
            dr[0] = row["UserQueryURL"].ToString();
            dr[1] = row["UserQueryName"].ToString();
            dr[2] = "q" + row["UserQueryID"].ToString();
            dr[3] = "query";
            dr[4] = true;
            dr[5] = "";
            dt.Rows.Add(dr);
        }

        query = dt;
    }

    //添加菜单checkbox到list
    public void AddCheckBox(Repeater list)
    {
        foreach (Control c in list.Controls)
        {
            CheckBox checkbox = (CheckBox)c.FindControl("MenuBox");
            string strId = ((HiddenField)c.FindControl("MenuIDHid")).Value;
            MyCheckBox cb = new MyCheckBox(checkbox, strId);
            lists.Add(cb);
        }
    }
    
    //保存
    public string GetSaveMenu()
    {
        string value = "";

        foreach (MyCheckBox cb in lists)
        {
            CheckBox checkbox = (CheckBox)cb.CheckBox;
            if (checkbox.Checked) value += (cb.Ref + ",");
        }

        if (value != "")
        {
            value = value.Remove(value.Length - 1);

            string[] arr = value.Split(',');
            if (arr.Length > 5)
            {
                throw new Exception("选择数量不能大于5个。");
            }
        }

        return value;
    }

    //checkbox自定义类
    class MyCheckBox
    {
        public CheckBox CheckBox { get; set; }
        public string Ref { get; set; }

        public MyCheckBox() { }

        public MyCheckBox(CheckBox cb, string str)
        {
            this.CheckBox = cb;
            this.Ref = str;
        }
    }

}
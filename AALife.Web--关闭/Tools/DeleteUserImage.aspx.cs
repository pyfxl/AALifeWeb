using AALife.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_DeleteUserImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserTableBLL bll = new UserTableBLL();

        if (!Page.IsPostBack)
        {
            string dirPath = Server.MapPath("~/Images/Users/");
            DataTable name_dt = FileHelper.GetFileName(dirPath);

            DataTable user_dt = bll.GetUserList();

            foreach (DataRow name_dr in name_dt.Rows)
            {
                bool hasFile = false;
                string fileName = name_dr["FileName"].ToString();
                
                foreach (DataRow user_dr in user_dt.Rows)
                {
                    string userImage = user_dr["UserImage"].ToString();
                    if (fileName == userImage)
                    {
                        hasFile = true;
                    }
                }

                if (!hasFile)
                {
                    string path = dirPath + fileName;
                    FileHelper.DeleteFile(path);
                }
            }
        }
    }
}
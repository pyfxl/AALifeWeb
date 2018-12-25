<%@ WebHandler Language="C#" Class="SyncUserImage" %>

using System;
using System.Web;
using AALife.BLL;
using AALife.Model;

public class SyncUserImage : IHttpHandler 
{
    
    public void ProcessRequest (HttpContext context) 
    {

        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";

        int count = context.Request.Files.Count;

        for (int i = 0; i < count; i++) 
        {
            HttpPostedFile aFile = context.Request.Files[i];

            if (aFile.ContentLength == 0 || String.IsNullOrEmpty(aFile.FileName))
            {
                continue;
            }
            
            string displayFileName = System.IO.Path.GetFileName(aFile.FileName);
            string realFileName = System.Web.HttpContext.Current.Server.MapPath("/Images/Users/") + displayFileName;
            string imageFileName = displayFileName;

            try
            {
                aFile.SaveAs(realFileName);
            }
            catch
            {
                imageFileName = "user.gif";
            }

            int userId = getUserId(displayFileName);
            
            UserTableBLL bll = new UserTableBLL();
            UserInfo user = bll.GetUserByUserId(userId);
            user.UserImage = imageFileName;
            user.ModifyDate = DateTime.Now;
            
            bool success = bll.UpdateUser(user);
            if (success)
            {
                context.Response.Write(1);
            }
            else
            {
                context.Response.Write(0);
            }
        }
        
    }
 
    public bool IsReusable 
    {
        get 
        {
            return false;
        }
    }

    private int getUserId(string fileName)
    {
        int start = fileName.LastIndexOf(".");
        string name = fileName.Substring(0, start);
        name = name.Replace("tu_", "");

        return Convert.ToInt32(name);
    }

}
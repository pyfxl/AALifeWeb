<%@ WebHandler Language="C#" Class="FileUpload" %>

using System;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using AALife.EF.BLL;
using System.Drawing;

public class FileUpload : IHttpHandler
{
    public void ProcessRequest (HttpContext context)
    {
        try
        {
            HttpPostedFile postedFile = context.Request.Files["UserImage"];
            int userId = Convert.ToInt32(context.Request.QueryString["userId"]);
            string filePath = context.Server.MapPath("~/Images/Users/");
            string fileName = GetFileName(postedFile.FileName, userId);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            System.Drawing.Image bm = System.Drawing.Image.FromStream(postedFile.InputStream);
            bm = ResizeBitmap((Bitmap) bm, 100, 100); /// new width, height
            bm.Save(Path.Combine(filePath, fileName));
            //postedFile.SaveAs(Path.Combine(filePath, fileName));

            UpdateUserImage(userId, fileName);

            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    //取头像名
    private string GetFileName(string fileName, int userId)
    {
        if (fileName == "") return "user.gif";
        string extName = fileName.Substring(fileName.IndexOf('.'));
        return "tu_" + userId + extName;
    }

    //更新头像
    private void UpdateUserImage(int userId, string fileName)
    {
        UserTableBLL bll = new UserTableBLL();
        bll.UpdateUserImage(userId, fileName);
    }
    
    //修改头像大小
    private Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
    {
        Bitmap result = new Bitmap(nWidth, nHeight);
        using (Graphics g = Graphics.FromImage((System.Drawing.Image)result))
        {
            g.DrawImage(b, 0, 0, nWidth, nHeight);
        }
        return result;
    }

}
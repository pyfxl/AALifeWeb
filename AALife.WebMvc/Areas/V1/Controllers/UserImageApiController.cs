using AALife.Service.EF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class UserImageApiController : BaseApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IHttpActionResult Post(int id)
        {
            try
            {
                var context = HttpContext.Current;
                HttpFileCollection postedFile = context.Request.Files;
                if (postedFile.Count > 0)
                {
                    string filePath = context.Server.MapPath("~/Images/Users/");
                    string fileName = GetFileName(postedFile[0].FileName, id);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    System.Drawing.Image bm = System.Drawing.Image.FromStream(postedFile[0].InputStream);
                    bm = ResizeBitmap((Bitmap)bm, 100, 100); /// new width, height
                    bm.Save(Path.Combine(filePath, fileName));

                    UpdateUserImage(id, fileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
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
}
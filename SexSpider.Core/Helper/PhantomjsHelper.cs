using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SexSpider.Core.Helper
{
    public class PhantomjsHelper
    {
        /// <summary>
        /// 利用phantomjs 爬取AJAX加载完成之后的页面
        /// JS脚本刷新时间间隔为1秒，防止页面AJAX请求时间过长导致数据无法获取
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetAjaxHtml(string url, HttpConfig config, int interval = 1000)
        {
            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                ProcessStartInfo start = new ProcessStartInfo(path + @"phantomjs\phantomjs.exe");//设置运行的命令行文件问ping.exe文件，这个文件系统会自己找到 
                start.WorkingDirectory = path + @"phantomjs\";
 
                ////设置命令参数
                string commond = string.Format("{0} {1} {2} {3} {4} {5}", path + @"phantomjs\codes.js", url, interval, config.UserAgent, config.Accept, config.Referer);
                start.Arguments = commond;
                StringBuilder sb = new StringBuilder();
                start.CreateNoWindow = true;//不显示dos命令行窗口 
                start.RedirectStandardOutput = true;// 
                start.RedirectStandardInput = true;// 
                start.UseShellExecute = false;//是否指定操作系统外壳进程启动程序 
                Process p = Process.Start(start);
 
                StreamReader reader = p.StandardOutput;//截取输出流                
                string line = reader.ReadToEnd();//每次读取一行 
                string strRet = line;// sb.ToString();
                p.WaitForExit();//等待程序执行完退出进程 
                p.Close();//关闭进程  
                reader.Close();//关闭流 
                return strRet;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        
        public class HttpConfig
        {
            /// <summary>
            /// 网站cookie信息
            /// </summary>
            public string Cookie { get; set; }
 
            /// <summary>
            /// 页面Referer信息
            /// </summary>
            public string Referer { get; set; }
 
            /// <summary>
            /// 默认(text/html)
            /// </summary>
            public string ContentType { get; set; }
 
            public string Accept { get; set; }
 
            public string AcceptEncoding { get; set; }
 
            /// <summary>
            /// 超时时间(毫秒)默认100000
            /// </summary>
            public int Timeout { get; set; }
 
            public string UserAgent { get; set; }
 
            /// <summary>
            /// POST请求时，数据是否进行gzip压缩
            /// </summary>
            public bool GZipCompress { get; set; }
 
            public bool KeepAlive { get; set; }
 
            public string CharacterSet { get; set; }
 
            public HttpConfig()
            {
                this.Timeout = 100000;
                this.ContentType = "text/html; charset=" + Encoding.UTF8.WebName;
 
                this.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36";
                this.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                this.AcceptEncoding = "gzip,deflate";
                this.GZipCompress = false;
                this.KeepAlive = true;
                this.CharacterSet = "UTF-8";
            }
        }
    }
}
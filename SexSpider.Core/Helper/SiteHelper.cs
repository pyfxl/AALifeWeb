using AngleSharp.Parser.Html;
using SexSpider.Core.Filter;
using SexSpider.Core.Models;
using Newtonsoft.Json.Linq;
using NLog;
using NReco.PhantomJS;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SexSpider.Core.Helper
{
    public class SiteHelper
    {
        public static Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 取站点列表
        /// </summary>
        public static IEnumerable<ListModel> GetSiteList(SexSpiders sex)
        {
            string html = sex.DocType != null && sex.DocType.Contains("ajax") ? GetJSContent(sex.SiteLink, sex.PageEncode) : GetHtmlContent(sex.SiteLink, sex.PageEncode, sex.Domain);

            //过滤站点
            html = FilterHtml(html, sex.SiteFilter);
            html = ReplaceHtml(html, sex.SiteReplace);

            FilterChain chain = LoadFilter(sex.ListFilter);

            if (sex.DocType != null && sex.DocType.Contains("json"))
            {
                string[] root = Regex.Split(sex.ListDiv, "\\|\\|");
                var jObject = Newtonsoft.Json.Linq.JObject.Parse(html);
                
                var jToken = jObject[root[0]];

                string[] m = root[0].Split('&');
                switch (m.Length)
                {
                    case 2:
                        jToken = jObject[m[0]][m[1]];
                        break;
                    case 3:
                        jToken = jObject[m[0]][m[1]][m[2]];
                        break;
                }

                foreach (var item in jToken)
                {
                    string[] child = root[1].Split('&');

                    yield return new ListModel
                    {
                        Thumb = item.Value<string>(child[2]),
                        Title = System.Net.WebUtility.HtmlDecode(item.Value<string>(child[0])),
                        Link = GetLink(item.Value<string>(child[1]), sex.Domain),
                        Domain = sex.Domain,
                        LastStart = item.Value<string>(child[3])
                    };
                }
            }
            else
            {
                var parser = new HtmlParser();
                var _document = parser.Parse(html);

                if (!string.IsNullOrWhiteSpace(sex.MainDiv))
                {
                    var main = _document.QuerySelectorAll(sex.MainDiv);
                    foreach (var m in main)
                    {
                        var ctx = parser.Parse(m.InnerHtml);
                        var item = ctx.QuerySelector(sex.ListDiv);

                        if (item == null) continue;

                        string _title = chain.DoFilter(item.InnerHtml);
                        string _link = GetLink(item.GetAttribute("href"), sex.Domain);

                        if (String.IsNullOrEmpty(_title)) continue;

                        var thumb = ctx.QuerySelector(sex.ThumbDiv);
                        var imgtext = thumb == null ? "" : thumb.OuterHtml;

                        yield return new ListModel
                        {
                            Thumb = GetThumb(imgtext, sex.Domain),
                            Title = System.Net.WebUtility.HtmlDecode(_title),
                            Link = _link,
                            Domain = sex.Domain
                        };
                    }
                }
                else
                {
                    var content = _document.QuerySelectorAll(sex.ListDiv);
                    foreach (var item in content)
                    {
                        string _title = chain.DoFilter(item.InnerHtml);
                        string _link = GetLink(item.GetAttribute("href"), sex.Domain);

                        if (String.IsNullOrEmpty(_title)) continue;

                        yield return new ListModel
                        {
                            Thumb = GetThumb(item.InnerHtml, sex.Domain),
                            Title = System.Net.WebUtility.HtmlDecode(_title),
                            Link = _link,
                            Domain = sex.Domain
                        };
                    }
                }
            }
        }

        /// <summary>
        /// 取图片页面
        /// </summary>
        public static IEnumerable<ImageModel> GetListImage(SexSpiders sex, string url)
        {
            string html = sex.ImgType != null && sex.ImgType.Contains("ajax") ? GetJSContent(url, sex.PageEncode) : GetHtmlContent(url, sex.PageEncode, sex.Domain);

            //过滤站点
            html = FilterHtml(html, sex.SiteFilter);
            html = ReplaceHtml(html, sex.SiteReplace);

            FilterChain chain = LoadFilter(sex.ImageFilter);
            
            var parser = new HtmlParser();
            var _document = parser.Parse(html);
            var content = _document.QuerySelectorAll(sex.ImageDiv);
            foreach (var item in content)
            {
                string link = "";
                if (chain.Count() > 0)
                {
                    link = chain.DoFilter(item.OuterHtml);
                }
                else
                {
                    link = item.GetAttribute("src") ?? item.GetAttribute("href");
                }

                if (String.IsNullOrEmpty(link)) continue;

                string _image = GetLink(link, sex.Domain);

                yield return new ImageModel
                {
                    ImageUrl = _image,
                    ImageDomain = sex.Domain
                };
            }
        }

        /// <summary>
        /// 取有分页的图片
        /// </summary>
        public static IEnumerable<ImageModel> GetListImagePage(SexSpiders sex, string url)
        {
            var images = new List<ImageModel>();
            var pages = new List<PageModel>();

            var newPages = SiteHelper.GetImagePage(sex, url).ToList();

            //1：默认 2：总页数[1][2]..[10] 3：先通过filter取总页数 4:ajax #page
            if (sex.PageLevel == 4)
            {
                string total = GetPageTotal(sex, url);
                pages = GetPageAjax(url, total);
            }
            else if (sex.PageLevel == 3)
            {
                string total = GetPageTotal(sex, url);
                pages = GetPageMany(url, newPages, total);
            }
            else if (sex.PageLevel == 2)
            {
                pages = GetPageMany(url, newPages, "");
            }
            else
            {
                pages = newPages;
            }
                        
            //添加原始页面
            pages.Insert(0, new PageModel { PageUrl = url });
            
            foreach (var p in pages)
            {
                var image = SiteHelper.GetListImage(sex, p.PageUrl).ToList();
                images.AddRange(image);
            }

            return images;
        }

        /// <summary>
        /// 取图片总页数
        /// </summary>
        public static string GetPageTotal(SexSpiders sex, string url)
        {
            string total = "";
            string html = sex.ImgType != null && sex.ImgType.Contains("ajax") ? GetJSContent(url, sex.PageEncode) : GetHtmlContent(url, sex.PageEncode, sex.Domain);

            //过滤站点
            html = FilterHtml(html, sex.SiteFilter);
            html = ReplaceHtml(html, sex.SiteReplace);
            
            var parser = new HtmlParser();
            var _document = parser.Parse(html);
            var content = _document.QuerySelectorAll(sex.PageFilter);//取总页数
            foreach (var item in content)
            {
                string str = System.Net.WebUtility.HtmlDecode(item.InnerHtml);
                total = Regex.Replace(str, "[^\\d]", "");
            }

            return total;
        }
        
        /// <summary>
        /// 取图片分页
        /// </summary>
        public static IEnumerable<PageModel> GetImagePage(SexSpiders sex, string url)
        {
            string html = sex.ImgType != null && sex.ImgType.Contains("ajax") ? GetJSContent(url, sex.PageEncode) : GetHtmlContent(url, sex.PageEncode, sex.Domain);

            //过滤站点
            html = FilterHtml(html, sex.SiteFilter);
            html = ReplaceHtml(html, sex.SiteReplace);

            FilterChain chain = LoadFilter(sex.PageFilter);
            html = chain.DoFilter(html);

            //分页的时候取当前页
            string _domain = url.Substring(0, url.LastIndexOf('/') + 1);

            var parser = new HtmlParser();
            var _document = parser.Parse(html);
            var content = _document.QuerySelectorAll(sex.PageDiv);
            foreach (var item in content)
            {
                //分页时点击类型没有内容要注释，此处与获取多分页时冲突
                if (!Regex.IsMatch(item.InnerHtml, @"^\d*$")) continue;

                string _link = item.GetAttribute("href");

                if (_link == null || _link == "#" || _link.Contains("javascript")) continue;

                _link = GetLink(_link, _domain, sex.Domain);
                
                yield return new PageModel
                {
                    PageUrl = _link
                };
            }
        }

        /// <summary>
        /// 取html内容2
        /// </summary>
        public static string GetHtmlContent2(string url, string encoding, string domain)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            //client.Proxy = new WebProxy("127.0.0.1", 32438);
            client.Encoding = Encoding.GetEncoding(encoding);
            client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.13 Safari/537.36");
            client.Headers.Add("referer", domain);
            
            return client.DownloadString(url);
        }

        /// <summary>
        /// 取html内容
        /// </summary>
        public static string GetHtmlContent(string url, string encode, string domain)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)System.Net.WebRequest.Create(url);
                //req.Proxy = new WebProxy("127.0.0.1", 32897); //lantern
                req.AutomaticDecompression = DecompressionMethods.GZip;
                req.Referer = domain;
                req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.13 Safari/537.36";
                req.Timeout = 60 * 1000;

                using (var rep = (HttpWebResponse)req.GetResponse())
                using (var stream = rep.GetResponseStream())
                using (var sr = new StreamReader(stream, Encoding.GetEncoding(encode)))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                log.Error("访问网络错误", url, ex.Message);
            }

            return "";
        }

        /// <summary>
        /// 异步获取html方法
        /// </summary>
        public static async Task<string> GetHtmlContent3(string url, string encode, string domain)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var contentType = response.Content.Headers.ContentType;
            if (string.IsNullOrEmpty(contentType.CharSet))
            {
                contentType.CharSet = encode;
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        /// <summary>
        /// 加载过滤器
        /// </summary>
        private static FilterChain LoadFilter(string str)
        {
            FilterChain chain = new FilterChain();

            if (str == null || str == "") return chain;

            string[] cls = str.Split(',');

            foreach(var c in cls)
            {
                string className = string.Format("SexSpider.Core.Filter.{0}", c);
                var t = Type.GetType(className);
                if (t != null)
                {
                    var filter = Activator.CreateInstance(t) as IFilter;
                    chain.AddFilter(filter);
                }
            }

            return chain;
        }
        
        /// <summary>e
        /// 取url地址
        /// </summary>
        private static string GetLink(string url, string domain)
        {
            if (url == null || url == "") return "";

            if(url.StartsWith("http://") || url.StartsWith("https://") || url.StartsWith("//"))
            {
                return url;
            }
            
            url = url.Replace("./", "").Replace("../", "");
            
            return domain + url;
        }

        /// <summary>
        /// 取url地址
        /// </summary>
        private static string GetLink(string url, string _domain, string domain)
        {
            if (url == null || url == "") return "";

            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                return url;
            }
            else if (url.StartsWith("/"))
            {
                return domain + url;
            }

            url = url.Replace("./", "").Replace("../", "");

            return _domain + url;
        }

        /// <summary>
        /// 取缩略图
        /// </summary>
        private static string GetThumb(string str, string domain)
        {
            if (str == null || str == "") return "";
            var img = "";

            var parser = new HtmlParser();
            var _document = parser.Parse(str);
            var item = _document.QuerySelector("img");

            if (item == null)
            {
                if (str.IndexOf("background") != -1)
                {
                    Regex reg = new Regex(@"url\(([^)]*)\)");
                    Match m = reg.Match(str);
                    if (m.Success)
                        img = m.Result("$1");
                }
            }
            else
            {
                var attrs = new List<string> { "file", "data-original", "data-src", "zoomfile", "src" };

                foreach (var s in attrs)
                {
                    if (item.HasAttribute(s))
                    {
                        img = item.GetAttribute(s);
                        break;
                    }                    
                }
            }

            if (img == "") return "";

            if (img.StartsWith("http://") || img.StartsWith("https://"))
            {
                return img;
            }

            return domain + img;
        }

        /// <summary>
        /// 取多个页面，ajax页面 #p=8
        /// </summary>
        private static List<PageModel> GetPageAjax(string url, string total)
        {
            var pages = new List<PageModel>();

            int pageNum = 0;

            if (total != "")
            {
                pageNum = Convert.ToInt32(total);
            }

            for (int i = 1; i <= pageNum; i++)
            {
                string newUrl = string.Format("{0}#p={1}", url, i);

                pages.Add(new PageModel { PageUrl = newUrl });
            }

            return pages;
        }
        
        /// <summary>
        /// 取多个页面，用来取只显示部分页面站点，比如妹子图[1][2][3]...[70]
        /// </summary>
        private static List<PageModel> GetPageMany(string url, List<PageModel> newPages, string total)
        {
            var pages = new List<PageModel>();

            int lastPos = url.LastIndexOf('/');
            string urlFirst = url.Substring(0, lastPos);
            string urlEnd = url.Substring(lastPos);

            int dianPos = urlEnd.LastIndexOf('.');
            string dianFirst = "";
            if (dianPos != -1)
            {
                dianFirst = urlEnd.Substring(0, dianPos);
            }
            else
            {
                dianFirst = urlEnd;
            }

            string newUrl = urlFirst + dianFirst;

            string lastPage = newPages.FindLast(a => a.PageUrl != "").PageUrl;

            string pageStr = lastPage.Replace(newUrl, "");

            int pageNum = 0;

            if (total != "")
            {
                pageNum = Convert.ToInt32(total);
            }
            else
            {
                Int32.TryParse(Regex.Replace(pageStr, @"[^\d]", ""), out pageNum);
            }

            for (int i = 2; i <= pageNum; i++)
            {
                Regex r = new Regex(@"\d+");

                string newString = r.Replace(pageStr, i.ToString());

                pages.Add(new PageModel { PageUrl = newUrl + newString });
            }

            return pages;
        }
        
        /// <summary>
        /// 替换站点html
        /// </summary>
        /// <param name="html"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static string ReplaceHtml(string html, string filter)
        {
            if (filter == null || filter == "") return html;

            var jArray = Newtonsoft.Json.Linq.JArray.Parse(filter);
            foreach(var arr in jArray)
            {
                string oldStr = HttpUtility.UrlDecode(arr["old"].ToString());
                string newStr = HttpUtility.UrlDecode(arr["new"].ToString());
                html = html.Replace(oldStr, newStr);
            }

            return html;
        }

        /// <summary>
        /// 过滤站点html
        /// </summary>
        /// <param name="html"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static string FilterHtml(string html, string filter)
        {
            if (filter == null || filter == "") return html;

            FilterChain chain = LoadFilter(filter);
            if (chain.Count() > 0)
            {
                html = chain.DoFilter(html);
            }

            return html;
        }

        /// <summary>
        /// 加载js动态内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetJSContent(string url, string encode)
        {
            string text = "";

            var phantomJS = new PhantomJS();

            // write result to stdout
            //Console.WriteLine("Getting content from baidu.com directly to C# code...");
            //var outFileHtml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "baidu.html");
            //if (File.Exists(outFileHtml))
            //    File.Delete(outFileHtml);
            using (var outFs = new MemoryStream())
            {
                try
                {
                    phantomJS.RunScript(string.Format(@"
                        var system = require('system');
                        var page = require('webpage').create();
                        page.open('{0}', function() {{
                            system.stdout.writeLine(page.content);
                            phantom.exit();
                        }});
                    ", url), null, null, outFs);

                    byte[] b = outFs.ToArray();
                    text = System.Text.Encoding.GetEncoding(encode).GetString(b, 0, b.Length);
                }
                finally
                {
                    phantomJS.Abort(); // ensure that phantomjs.exe is stopped
                }
            }
            //Console.WriteLine("Result is saved into " + outFileHtml);

            return text;
        }

        /// <summary>
        /// webdriver取页面
        /// </summary>
        private static string GetJSContent0(string url, string encode)
        {
            string _assemblyPath = Assembly.GetExecutingAssembly().Location;
            string _rootPath = Path.GetDirectoryName(_assemblyPath);
            ChromeOptions op = new ChromeOptions();
            op.AddArguments("--headless");//开启无gui模式
            op.AddArguments("--no-sandbox");//停用沙箱以在Linux中正常运行
            ChromeDriver cd = new ChromeDriver(_rootPath, op, TimeSpan.FromSeconds(180));
            cd.Navigate().GoToUrl(url);
            Thread.Sleep(5000);
            //cd.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            string text = cd.PageSource;
            cd.Quit();
            
            return text;
        }
    }
}
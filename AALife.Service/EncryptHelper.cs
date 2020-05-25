using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AALife.Service
{
    public static class EncryHelper
    {

        public static string SHA1(string str)
        {
            var buffer = Encoding.UTF8.GetBytes(str);
            var data = System.Security.Cryptography.SHA1.Create().ComputeHash(buffer);

            var sb = new StringBuilder();
            foreach (var t in data)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }

        public static string Md5(this string str)
        {
            //将输入字符串转换成字节数组
            var buffer = Encoding.Default.GetBytes(str);

            //接着，创建Md5对象进行散列计算
            var data = System.Security.Cryptography.MD5.Create().ComputeHash(buffer);

            //创建一个新的Stringbuilder收集字节
            var sb = new StringBuilder();

            //遍历每个字节的散列数据 
            foreach (var t in data)
            {
                //格式每一个十六进制字符串
                sb.Append(t.ToString("X2"));
            }

            //返回十六进制字符串
            return sb.ToString();
        }

    }
}
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Net.Mail;
using System.Net;

public class Message
{
    static Message()
    {
    }

    public static void SendMessage(string name, string from, string img)
    {
        string tFrom = "67936108@qq.com";
        string tTo = "67936108@qzone.qq.com";
        string tSubject = "AA生活记账 - " + "欢迎" + name;
        string dEncoding = "utf-8";
        string tBody = GetBody(img);
        string tSmtpServer = "smtp.qq.com";
        string tPort = "25";
        string tLogin = "67936108";
        string tPassword = "feng,7459235";

        Dimac.JMail.Message message = new Dimac.JMail.Message();
        message.From = tFrom;
        message.To.Add(tTo);
        message.Subject = tSubject;
        message.Charset = System.Text.Encoding.GetEncoding(dEncoding);
        message.BodyHtml = tBody;

        try
        {
            Dimac.JMail.Smtp.Send(message, tSmtpServer, short.Parse(tPort), GetDomain(message.From.Email), Dimac.JMail.SmtpAuthentication.Login, tLogin, tPassword);
        }
        catch
        {
        }	
    }

    public static void SendMessageOld(string name, string from, string img)
    {
        try
        {
            SmtpClient smtp = new SmtpClient();
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = false;

            smtp.Host = "smtp.qq.com";
            smtp.Port = 25;

            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential("67936108@qq.com", "feng,7459235");

            MailMessage mm = new MailMessage();
            mm.Priority = MailPriority.Normal;

            mm.From = new MailAddress("67936108@qq.com", "冯湘灵", Encoding.GetEncoding(65001));
            //mm.ReplyTo = new MailAddress("67936108@qq.com", "我的接收邮箱", Encoding.GetEncoding(65001));
            mm.To.Add(new MailAddress("67936108@qzone.qq.com", "QQ空间", Encoding.GetEncoding(65001)));

            mm.Subject = "AA生活记账 - " + "欢迎" + name;
            mm.SubjectEncoding = Encoding.GetEncoding(65001);
            mm.IsBodyHtml = true;
            mm.BodyEncoding = Encoding.GetEncoding(65001);
            string msg = "";
            msg += "<p><img src='" + (Regex.IsMatch(img, "^http") ? img : "http://www.fxlweb.com/Images/Users/" + img) + "' title='查看详情' style='width:50px;height:50px;' /></p>";
            msg += "<p>" + WebConfiguration.SiteDescription + "</p>";
            mm.Body = msg;

            smtp.Send(mm);
        }
        catch
        {
        }
    }
    
    // Get the domain part of an email address.
    private static string GetDomain(string email)
    {
        int index = email.IndexOf('@');
        return email.Substring(index + 1);
    }

    public static string GetBody(string img)
    {
        string msg = "";
        msg += "<p>" + WebConfiguration.SiteDescription + "</p>"; 
        msg += "<p><img src='" + (Regex.IsMatch(img, "^http") ? img : "http://www.fxlweb.com/Images/Users/" + img) + "' title='查看详情' style='width:50px;height:50px;' /></p>";
        return msg;
    }

    public static string GetBody(string name, string img, string content, string email)
    {
        string str = content.Equals("") ? WebConfiguration.SiteDescription : content;
        string msg = "";
        msg += "<p>" + name + " 说：" + str + "</p>";
        msg += "<p>（邮箱：" + email + "）</p>"; 
        msg += "<p><img src='" + (Regex.IsMatch(img, "^http") ? img : "http://www.fxlweb.com/Images/Users/" + img) + "' title='查看详情' style='width:50px;height:50px;' /></p>";
        return msg;
    }

    public static string GetSubject(string name, string from)
    {
        string str = "AA生活记账 - " + "欢迎" + name;
        return str;
    }
}
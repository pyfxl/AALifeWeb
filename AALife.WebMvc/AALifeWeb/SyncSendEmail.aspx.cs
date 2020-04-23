using System;
using System.Net.Mail;
using System.Text;
using System.Web;

public partial class AALifeWeb_SyncSendEmail : SyncBase
{
    public static string EmailPass = System.Configuration.ConfigurationManager.AppSettings["emailpass"];

    protected void Page_Load(object sender, EventArgs e)
    {
        string userName = Request.Form["username"].ToString();
        userName = (userName == "" ? "匿名" : userName);
        string userImage = Request.Form["userimage"].ToString();
        userImage = (userImage == "" ? "user.gif" : userImage);
        string content = Request.Form["content"].ToString();
        string userEmail = Request.Form["useremail"].ToString();

        //Response.Write("{ \"result\":\"ok\" }");
        //Response.End();

        //userName = HttpUtility.UrlEncode(userName);
        //content = HttpUtility.UrlEncode(content);
        //userEmail = HttpUtility.UrlEncode(userEmail);

        string mSubject = "来自用户 - " + userName;
        string mBody = Message.GetBody(userName, userImage, content, userEmail);
        //string sUrl = "SyncSendEmail.asp?subject=" + HttpUtility.UrlEncode(mSubject) + "&body=" + HttpUtility.UrlEncode(mBody) + "&email=" + HttpUtility.UrlEncode(userEmail);
        //Response.Redirect(sUrl);

        //ding
        AALife.WebMvc.MsgHelper.DingMessage(string.Format("用户建议和反馈\n\n来自：{0}\n\n内容：{1}", userName, content));

        bool flag = SendEmail(userEmail, mSubject, mBody);
        if (flag)
        {
            Response.Write("{ \"result\":\"ok\" }");
            Response.End();
        }
        else
        {
            Response.Write("{ \"result\":\"error\" }");
            Response.End();
        }
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="mailTo">要发送的邮箱</param>
    /// <param name="mailSubject">邮箱主题</param>
    /// <param name="mailContent">邮箱内容</param>
    /// <returns>返回发送邮箱的结果</returns>
    public static bool SendEmail0(string mailTo, string mailSubject, string mailContent)
    {
        // 设置发送方的邮件信息,例如使用网易的smtp
        string smtpServer = "smtp.aliyun.com"; //SMTP服务器
        string mailFrom = "pyfxl83@aliyun.com"; //登陆用户名
        string userPassword = "7459235sss";//登陆密码

        // 邮件服务设置
        SmtpClient smtpClient = new SmtpClient();
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
        smtpClient.Host = smtpServer; //指定SMTP服务器
        smtpClient.Port = 465;
        smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//用户名和密码

        // 发送邮件设置       
        MailMessage mailMessage = new MailMessage(mailFrom, mailTo); // 发送人和收件人
        mailMessage.Subject = mailSubject;//主题
        mailMessage.Body = mailContent;//内容
        mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
        mailMessage.IsBodyHtml = true;//设置为HTML格式
        mailMessage.Priority = MailPriority.Low;//优先级

        try
        {
            smtpClient.Send(mailMessage); // 发送邮件
            return true;
        }
        catch (SmtpException ex)
        {
            log.Info(mailTo + " | "+ mailContent);
            log.Info(ex);
            return false;
        }
    }

    /// <summary>
    /// 阿里云要用这种方式，要不然发不了，System.Net.Mail不支持Ssl
    /// </summary>
    /// <param name="mailTo"></param>
    /// <param name="mailSubject"></param>
    /// <param name="mailContent"></param>
    /// <returns></returns>
    public static bool SendEmail(string mailTo, string mailSubject, string mailContent)
    {
        log.Info(mailTo + " | " + mailContent);

        // 设置发送方的邮件信息,例如使用网易的smtp
        string smtpServer = "smtp.qq.com"; //SMTP服务器
        string mailFrom = "67936108@qq.com"; //登陆用户名
        string userPassword = EmailPass;//登陆密码，需要发送短信“配置邮件客户端”到1069070069开通，有可能会自动关闭，又要重新开通

        System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
        try
        {
            mail.To = mailTo;
            mail.From = mailFrom;
            mail.Subject = mailSubject;
            mail.BodyFormat = System.Web.Mail.MailFormat.Html;
            mail.Body = mailContent;

            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", mailFrom); //set your username here
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", userPassword); //set your password here
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 465);//set port
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");//set is ssl
            System.Web.Mail.SmtpMail.SmtpServer = smtpServer;
            System.Web.Mail.SmtpMail.Send(mail);
            return true;
        }
        catch (Exception ex)
        {

            log.Info(ex);
            return false;
        }
    }
}
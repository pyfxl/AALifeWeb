using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc
{
    public static class DingHelper
    {
        public static Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken(string appKey, string appSecret)
        {
            try
            {
                var client = new DefaultDingTalkClient("https://oapi.dingtalk.com/gettoken");

                OapiGettokenRequest tokenRequest = new OapiGettokenRequest();
                tokenRequest.Corpid = appKey;
                tokenRequest.Corpsecret = appSecret;
                tokenRequest.SetHttpMethod("GET");
                OapiGettokenResponse retString = client.Execute(tokenRequest);
                log.Info(retString);
                return retString.AccessToken;
            }
            catch (Exception ex)
            {
                log.Info(ex);
                return "";
            }
        }

        /// <summary>
        /// 获取免登录ticket
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static string GetJSTicket(string accessToken)
        {
            try
            { 
                var client = new DefaultDingTalkClient("https://oapi.dingtalk.com/get_jsapi_ticket");

                OapiGetJsapiTicketRequest jsapiTicket = new OapiGetJsapiTicketRequest();
                jsapiTicket.AddOtherParameter("access_token", accessToken);
                jsapiTicket.SetHttpMethod("GET");
                OapiGetJsapiTicketResponse retString = client.Execute(jsapiTicket);
                log.Info(retString);
                return retString.Ticket;
            }
            catch (Exception ex)
            {
                log.Info(ex);
                return "";
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static string SendMessage(string agentId, string accessToken, string title, string body, string user)
        {
            try
            {
                IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2");

                OapiMessageCorpconversationAsyncsendV2Request request = new OapiMessageCorpconversationAsyncsendV2Request();
                request.UseridList = user;// GetUserIdByPhone("15918514353", accessToken);
                request.AgentId = long.Parse(agentId);
                request.ToAllUser = false;

                OapiMessageCorpconversationAsyncsendV2Request.MsgDomain msg = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain();
                //msg.Msgtype = "text";
                //msg.Text = new OapiMessageCorpconversationAsyncsendV2Request.TextDomain();
                //msg.Text.Content = "test123";
                //request.Msg_ = msg;

                //msg.Msgtype = "image";
                //msg.Image = (new OapiMessageCorpconversationAsyncsendV2Request.ImageDomain());
                //msg.Image.MediaId = "@lADOdvRYes0CbM0CbA";
                //request.Msg_ = msg;

                //msg.Msgtype = "file";
                //msg.File = (new OapiMessageCorpconversationAsyncsendV2Request.FileDomain());
                //msg.File.MediaId = "@lADOdvRYes0CbM0CbA";
                //request.Msg_ = msg;

                //msg.Msgtype = "link";
                //msg.Link = (new OapiMessageCorpconversationAsyncsendV2Request.LinkDomain());
                //msg.Link.Title = "test";
                //msg.Link.Text = "test";
                //msg.Link.MessageUrl = "https://www.baidu.com";
                //msg.Link.PicUrl = "https://www.baidu.com";
                //request.Msg_ = msg;

                //msg.Msgtype = "markdown";
                //msg.Markdown = (new OapiMessageCorpconversationAsyncsendV2Request.MarkdownDomain());
                //msg.Markdown.Text = "##### text";
                //msg.Markdown.Title = "### Title";
                //request.Msg_ = msg;

                //msg.Oa = (new OapiMessageCorpconversationAsyncsendV2Request.OADomain());
                //msg.Oa.Head = (new OapiMessageCorpconversationAsyncsendV2Request.HeadDomain());
                //msg.Oa.Head.Text = title;
                //msg.Oa.Body = (new OapiMessageCorpconversationAsyncsendV2Request.BodyDomain());
                //msg.Oa.Body.Content = body;
                //msg.Oa.MessageUrl = "http://www.fxlweb.com/Web2018/DingLogin.aspx";
                //msg.Oa.PcMessageUrl = "http://www.fxlweb.com/Web2018/DingLogin.aspx";
                //msg.Msgtype = "oa";
                //request.Msg_ = msg;

                msg.ActionCard = (new OapiMessageCorpconversationAsyncsendV2Request.ActionCardDomain());
                msg.ActionCard.Title = title;
                msg.ActionCard.Markdown = body;
                msg.ActionCard.SingleTitle = "访问网站";
                msg.ActionCard.SingleUrl = "http://www.fxlweb.com/Web2018/DingLogin.aspx";
                msg.Msgtype = "action_card";
                request.Msg_ = msg;

                OapiMessageCorpconversationAsyncsendV2Response response = client.Execute(request, accessToken);
                log.Info(response);
                return response.Body;
            }
            catch (Exception ex)
            {
                log.Info(ex);
                return "";
            }
        }

        /// <summary>
        /// 通过code获取用户
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetUserByCode(string accessToken, string code)
        {
            try
            {
                IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/user/getuserinfo");
                OapiUserGetuserinfoRequest request = new OapiUserGetuserinfoRequest();
                request.Code = code;
                request.SetHttpMethod("GET");
                OapiUserGetuserinfoResponse response = client.Execute(request, accessToken);
                log.Info(response);
                return response.Userid;
            }
            catch (Exception ex)
            {
                log.Info(ex);
                return "";
            }
        }
        
        /// <summary>
        /// 通过手机号码找用户id
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static string GetUserIdByPhone(string phoneNum, string accessToken)
        {
            try
            {
                IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/user/get_by_mobile");
                OapiUserGetByMobileRequest request = new OapiUserGetByMobileRequest();
                request.Mobile = phoneNum;
                request.SetHttpMethod("GET");
                OapiUserGetByMobileResponse response = client.Execute(request, accessToken);
                log.Info(response);
                return response.Userid;
            }
            catch (Exception ex)
            {
                log.Info(ex);
                return "";
            }
        }

        /// <summary>
        /// 发送普通消息--弃用
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="sender">消息发送者 userId</param>
        /// <param name="cId">群会话或者个人会话的id，通过JSAPI的dd.chooseChatForNormalMsg接口唤起联系人界面选择之后即可拿到会话cid</param>
        /// <param name="msg">消息内容，消息类型和样例可参考“消息类型与数据格式”文档。最长不超过2048个字节</param>
        /// <returns></returns>
        public static OapiMessageSendToConversationResponse SendToConversation(string accessToken, string sender, string cId, OapiMessageSendToConversationRequest.MsgDomain msg)
        {
            try
            {
                IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/message/send_to_conversation");

                OapiMessageSendToConversationRequest req = new OapiMessageSendToConversationRequest();
                req.Sender = sender;
                req.Cid = cId;
                req.Msg_ = msg;

                OapiMessageSendToConversationResponse response = client.Execute(req, accessToken);
                log.Info(response);
                return response;
            }
            catch (Exception ex)
            {
                log.Info(ex);
                return null;
            }
        }


    }
}
<%@ Page Title="网上记账" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" Inherits="AALife.WebMvc.Web2018.DingLogin" Codebehind="DingLogin.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="js" Runat="Server">
<script src="https://g.alicdn.com/dingding/dingtalk-jsapi/2.10.3/dingtalk.open.js"></script>
<script src="http://g.alicdn.com/dingding/dingtalk-pc-api/2.7.0/index.js"></script>
<script type="text/javascript">
    $(function () {
        
        var udd = DingTalkPC.ua.isDesktop && DingTalkPC.ua.isInDingTalk ? DingTalkPC : dd;
        
        //config配置
        udd.config({
            agentId: '<%=dic["agentId"]%>', // 必填，微应用ID
            corpId: '<%=dic["corpId"]%>',//必填，企业ID
            timeStamp: '<%=dic["timeStamp"]%>', // 必填，生成签名的时间戳
            nonceStr: '<%=dic["nonceStr"]%>', // 必填，生成签名的随机串
            signature: '<%=dic["signature"]%>', // 必填，签名
            jsApiList: ['runtime.info',
                'biz.contact.choose',
                'device.notification.confirm',
                'device.notification.alert',
                'device.notification.prompt',
                'biz.ding.post',
                'runtime.permission.requestAuthCode',
                'device.geolocation.get',
                'biz.ding.post',
                'biz.contact.complexChoose']
        });

        //ready成功
        udd.ready(function (res) {
            //获取免登授权码 -- 注销获取免登服务，可以测试jsapi的一些方法
            udd.runtime.permission.requestAuthCode({
                corpId: '<%=dic["corpId"]%>',
                onSuccess: function (result) {
                    console.log(result);
                    //后台用code获取钉钉user，判断是否已关联
                    $.getJSON("DingUser.aspx?accessToken=<%=dic["accessToken"]%>&code=" + result["code"], function (data) {
                        console.log(data);  
                        $("#<%=HiddenCode.ClientID %>").val(data["user"]);
                        if (data["exists"] == "1") {                            
                            $("#dtloging").text("钉钉登录成功。");
                            window.location.href = "Default.aspx";
                        }
                        $("#dtloging").delay(500).hide(0);
                    });
                },
                onFail: function (err) {
                    console.log(err);
                }
            });
        });

        //error错误
        udd.error(function (error) {
            console.log(error);
        });

        var username = $("#<%=UserName.ClientID %>");
        var password = $("#<%=UserPassword.ClientID %>");
        username.val() == "" ? username.focus() : password.focus();

    });
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" Runat="Server">
<div id="datechoose"></div>
<div id="dtloging" style="width: 200px; height: 40px; position: absolute; left: calc(50% - 100px); top: 100px; background: lightblue; opacity: 0.7; z-index: 99; text-align: center;">正在检查钉钉登录信息。。。</div>
<div class="r_title">
    <h1>钉钉登录</h1>
</div>
<div id="r_content">
    <!--内容开始-->
    <div class="r_login">
        <table border="0" style="width:100%;" class="tableform">
            <tr>
                <th></th>
                <td></td>
                <td class="mbhide"></td>
            </tr>
            <tr>
                <th style="width:24%;">用户名　</th>
                <td><asp:TextBox ID="UserName" runat="server" MaxLength="12" TabIndex="1"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**&nbsp;&nbsp;&nbsp;<em>( 3-10字符 )</em></td>
                <td class="mbhide" style="width:24%;"></td>
            </tr>
            <tr>
                <th>密　码　</th>
                <td><asp:TextBox ID="UserPassword" runat="server" TextMode="Password" MaxLength="12" TabIndex="2"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**&nbsp;&nbsp;&nbsp;<em>( 3-10字符 )</em></td>
                <td class="mbhide"></td>
            </tr>
            <tr>
                <th></th>
                <td></td>
                <td class="mbhide"></td>
            </tr>
            <tr>
                <th></th>
                <td><asp:Button ID="SubmitButtom" runat="server" Text="登录" OnClick="SubmitButtom_Click" CssClass="btninput" TabIndex="3" /></td>
                <td class="mbhide">
                    <asp:HiddenField ID="HiddenCode" runat="server" />
                </td>
            </tr>
            <tr>
                <th></th>
                <td></td>
                <td class="mbhide"></td>
            </tr>
            <tr>
                <th></th>
                <td></td>
                <td class="mbhide"></td>
            </tr>
            <tr>
                <th></th>
                <td></td>
                <td class="mbhide"></td>
            </tr>
        </table>
        <div class="clear"></div>
    </div>
    <!--内容结束-->
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="end" Runat="Server">
</asp:Content>
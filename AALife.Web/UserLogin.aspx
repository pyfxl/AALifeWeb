<%@ Page Title="网上记账" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserLogin.aspx.cs" Inherits="Web2016_UserLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="js" Runat="Server">
<script type="text/javascript">
    $(function () {

        var username = $("#<%=UserName.ClientID %>");
        var password = $("#<%=UserPassword.ClientID %>");
        username.val() == "" ? username.focus() : password.focus();

    });
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" Runat="Server">
<div id="datechoose"></div>
<div class="r_title">
    <h1>用户登录</h1>
</div>
<div id="r_content">
    <!--内容开始-->
    <div class="r_login">
        <table border="0" style="width:100%;" class="tableform">
            <tr>
                <th></th>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <th style="width:24%;">用户名　</th>
                <td><asp:TextBox ID="UserName" runat="server" MaxLength="12" TabIndex="1"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**&nbsp;&nbsp;&nbsp;<em>( 3-10字符 )</em></td>
                <td style="width:24%;"><a target="_blank" href="http://shang.qq.com/wpa/qunwpa?idkey=944c168235281af2bf07b97f297950d1502ac20a861f261fb0573c78c96abc9a"><img border="0" src="http://pub.idqqimg.com/wpa/images/group.png" alt="加入AA生活记账QQ群" title="加入AA生活记账QQ群"/></a></td>
            </tr>
            <tr>
                <th>密　码　</th>
                <td><asp:TextBox ID="UserPassword" runat="server" TextMode="Password" MaxLength="12" TabIndex="2"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**&nbsp;&nbsp;&nbsp;<em>( 3-10字符 )</em></td>
                <td><a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=67936108&site=qq&menu=yes"><img border="0" src="http://wpa.qq.com/pa?p=1:67936108:7" alt="点击这里给我发消息" title="点击这里给我发消息"/></a></td>
            </tr>
            <tr>
                <th></th>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <th></th>
                <td><asp:Button ID="SubmitButtom" runat="server" Text="登录" OnClick="SubmitButtom_Click" CssClass="btninput" TabIndex="3" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="https://graph.qq.com/oauth2.0/authorize?client_id=100294524&response_type=token&scope=get_user_info&redirect_uri=http%3A%2F%2Fwww.fxlweb.com%2FAuthorLogin%2Fqq_login_ok.html"><img src="theme/images/Connect_logo_7.png" alt="" title="" /></a></td>
                <td></td>
            </tr>
            <tr>
                <th></th>
                <td><a href="UserRegister.aspx" style="font-weight:bold;">注册新用户 &gt;&gt;</a></td>
                <td></td>
            </tr>
            <tr>
                <th></th>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <th></th>
                <td></td>
                <td></td>
            </tr>
        </table>
        <div class="clear"></div>
    </div>
    <!--内容结束-->
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="end" Runat="Server">
</asp:Content>
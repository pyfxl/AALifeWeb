<%@ Page Title="网上记账" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserLogin.aspx.cs" Inherits="UserLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    $(function () {
        var username = $("#<%=UserName.ClientID %>");
        var password = $("#<%=UserPassword.ClientID %>");
        username.val() == "" ? username.focus() : password.focus();
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose"></div>
<div id="content">
    <!--内容开始-->  
    <div class="maindiv">
        <div class="title"><img src="/Images/Others/group_key.png" alt="" title="" />&nbsp;&nbsp;用户登录</div>
        <table cellspacing="0" border="0" style="width:100%;" class="tableuser tablelogin">
            <tr>
                <th></th>
                <td></td>
            </tr>
            <tr>
                <th style="width:24%;">用户名</th>
                <td><asp:TextBox ID="UserName" runat="server" MaxLength="12" TabIndex="1"></asp:TextBox></td>
            </tr>
            <tr>
                <th>密　码</th>
                <td><asp:TextBox ID="UserPassword" runat="server" TextMode="Password" MaxLength="12" TabIndex="2"></asp:TextBox></td>
            </tr>
            <tr>
                <th></th>
                <td></td>
            </tr>
            <tr>
                <th></th>
                <td><asp:Button ID="SubmitButtom" runat="server" Text="登录" OnClick="SubmitButtom_Click" CssClass="btninput" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="https://graph.qq.com/oauth2.0/authorize?client_id=100294524&response_type=token&scope=get_user_info&redirect_uri=http%3A%2F%2Fwww.fxlweb.com%2FAuthorLogin%2Fqq_login_ok.html"><img src="/Images/Others/Connect_logo_7.png" alt="" title="" /></a></td>
            </tr>
            <tr>
                <th></th>
                <td><a href="UserRegister.aspx">注册新用户 &gt;&gt;</a></td>
            </tr>
            <tr>
                <th></th>
                <td></td>
            </tr>
        </table>
    </div>
    <div class="h10"></div>
    <!--内容结束-->
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js" Runat="Server">
<script type="text/javascript">    
</script>
</asp:Content>
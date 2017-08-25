<%@ Page Title="用户注册" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserRegister.aspx.cs" Inherits="Web2016_UserRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="js" Runat="Server">
<script type="text/javascript">
    $(function () {
        $("#<%=UserName.ClientID %>").focus();
    });
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" Runat="Server">
<div id="datechoose"></div>
<div class="r_title">
    <h1>用户注册</h1>
</div>
<div id="r_content">
    <!--内容开始-->  
    <div class="r_login">
        <table border="0" style="width:100%;" class="tableform">
            <tr>
                <th></th>
                <td></td>
            </tr>
            <tr>
                <th style="width:24%;">用户名　</th>
                <td><asp:TextBox ID="UserName" runat="server" MaxLength="12" autocomplete="off"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**&nbsp;&nbsp;&nbsp;<em>( 3-10字符 )</em></td>
            </tr>
            <tr>
                <th>密　码　</th>
                <td><asp:TextBox ID="UserPassword" runat="server" TextMode="Password" MaxLength="12" autocomplete="off"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**&nbsp;&nbsp;&nbsp;<em>( 3-10字符 )</em></td>
            </tr>
            <tr>
                <th>重复密码</th>
                <td><asp:TextBox ID="UserPassword2" runat="server" TextMode="Password" MaxLength="12" autocomplete="off"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**&nbsp;&nbsp;&nbsp;<em>( 两次密码须一致 )</em></td>
            </tr>
            <tr>
                <th>昵　称　</th>
                <td><asp:TextBox ID="UserNickName" runat="server" MaxLength="20"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<em>( 可用中文 )</em></td>
            </tr>
            <tr>
                <th>邮　箱　</th>
                <td><asp:TextBox ID="UserEmail" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<em>( 用于找回密码 )</em></td>                        
            </tr>
            <tr>
                <th>工作日　</th>
                <td><asp:DropDownList ID="UserWorkDay" runat="server" MaxLength="20"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<em>( 用于固定消费 )</em></td>                        
            </tr>
            <tr>
                <th></th>
                <td><asp:Label ID="Label1" runat="server" CssClass="error"></asp:Label></td>
            </tr>
            <tr>
                <th></th>
                <td><asp:Button ID="SubmitButtom" runat="server" Text="提交注册" onclick="SubmitButtom_Click" CssClass="btninput" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="https://graph.qq.com/oauth2.0/authorize?client_id=100294524&response_type=token&scope=get_user_info&redirect_uri=http%3A%2F%2Fwww.fxlweb.com%2FAuthorLogin%2Fqq_login_ok.html"><img src="theme/images/Connect_logo_7.png" alt="" title="" /></a></td>
            </tr>
            <tr>
                <th></th>
                <td><a href="UserLogin.aspx">&lt;&lt; 返回登录</a></td>
            </tr>
            <tr>
                <th></th>
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
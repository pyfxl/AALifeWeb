<%@ Page Title="用户资料" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserAdmin.aspx.cs" Inherits="UserAdmin" %>

<%@ Register Src="UserControl/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    $(function () {
        if (window.location.href.lastIndexOf("#info") > 0) {
            $(".maindiv").scrollTop($(".maindiv").height());
        }
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose"></div>
<div id="content">
    <!--内容开始-->
    <uc6:UserMenu ID="UserMenu1" runat="server" />
    <div class="maindiv userdiv">
        <table cellspacing="0" border="0" style="width:100%;" class="tableuser tablelogin">
            <tr>
                <th style="width:24%;"></th>
                <td></td>
            </tr>
            <tr>
                <th>荣　耀　</th>
                <td>您已记账 <asp:Label ID="JoinDay" runat="server"></asp:Label> 天，共添加消费 <asp:Label ID="ItemCount" runat="server"></asp:Label> 笔。</td>
            </tr>
            <tr class="userline">
                <th></th>
                <td></td>
            </tr>
            <tr>
                <th></th>
                <td></td>
            </tr>
            <tr>
                <th>头　像　</th>
                <td><asp:Image ID="UserImage" runat="server" Width="50px" Height="50px" BorderWidth="1" BorderColor="LightGray" /></td>
            </tr>
            <tr>
                <th></th>    
                <td><asp:FileUpload ID="UserImageUpload" runat="server" CssClass="fileinput" /><em><asp:Label ID="Label5" runat="server" CssClass="error"></asp:Label></em></td>
            </tr>
            <tr>
                <th></th>
                <td><asp:Button ID="Button1" runat="server" Text="修改头像" OnClick="Button1_Click" CssClass="btninput" UseSubmitBehavior="false" /></td>
            </tr>
            <tr class="userline">
                <th></th>
                <td></td>
            </tr>
            <tr>
                <th></th>
                <td></td>
            </tr>
            <tr>
                <th>旧密码　</th>
                <td><asp:TextBox ID="UserPassword" runat="server" TextMode="Password" MaxLength="12" autocomplete="off"></asp:TextBox><em><asp:Label ID="Label2" runat="server" CssClass="error"></asp:Label></em></td>
            </tr>
            <tr>
                <th>新密码　</th>
                <td><asp:TextBox ID="NewPassword" runat="server" TextMode="Password" MaxLength="12" autocomplete="off"></asp:TextBox><em><asp:Label ID="Label1" runat="server" CssClass="error"></asp:Label></em></td>
            </tr>
            <tr>
                <th>重复密码</th>
                <td><asp:TextBox ID="NewPassword2" runat="server" TextMode="Password" MaxLength="12" autocomplete="off"></asp:TextBox><em><asp:Label ID="Label4" runat="server" CssClass="error"></asp:Label></em></td>
            </tr>
            <tr>
                <th></th>
                <td><asp:Button ID="Button2" runat="server" Text="修改密码" OnClick="Button2_Click" CssClass="btninput" UseSubmitBehavior="false" /></td>
            </tr>
            <tr class="userline">
                <th></th>
                <td></td>
            </tr>
            <tr>
                <th></th>
                <td></td>
            </tr>
            <tr>
                <th>昵　称　</th>
                <td><asp:TextBox ID="UserNickName" runat="server" MaxLength="20"></asp:TextBox></td>
            </tr>
            <tr>
                <th>邮　箱　</th>
                <td><asp:TextBox ID="UserEmail" runat="server"></asp:TextBox><em><asp:Label ID="Label3" runat="server" CssClass="error"></asp:Label></em></td>
            </tr>
            <tr>
                <th>手　机　</th>
                <td><asp:TextBox ID="UserPhone" runat="server" MaxLength="20"></asp:TextBox><em><asp:Label ID="Label6" runat="server" CssClass="error"></asp:Label></em></td>
            </tr>
            <tr>
                <th>工作日　</th>
                <td><asp:DropDownList ID="UserWorkDay" runat="server" MaxLength="20"></asp:DropDownList></td>
            </tr>
            <tr>
                <th>预算率％</th>
                <td><asp:TextBox ID="CategoryRate" runat="server" onkeyup="getpriceint(this);" MaxLength="10"></asp:TextBox><em><asp:Label ID="Label7" runat="server" CssClass="error"></asp:Label></em></td>
            </tr>
            <tr>
                <th><a name="#info"></a></th>
                <td><asp:Button ID="SubmitButton" runat="server" Text="修改资料" OnClick="SubmitButton_Click" CssClass="btninput" /></td>
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
    $(function () {
        $("#content .tabletitle .u1").addClass("on");
    });
</script>
</asp:Content>
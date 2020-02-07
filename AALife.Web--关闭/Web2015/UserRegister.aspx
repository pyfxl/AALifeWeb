<%@ Page Title="用户注册" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserRegister.aspx.cs" Inherits="UserRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    $(function () {
        $("#<%=UserName.ClientID %>").focus();
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose"></div>
<div id="content">
    <!--内容开始-->  
    <div class="maindiv userdiv">
        <div class="title"><img src="/Images/Others/group_add.png" alt="" title="" />&nbsp;&nbsp;用户注册</div>
        <table cellspacing="0" border="0" style="width:100%;" class="tableuser tablelogin">
            <tr>
                <th></th>
                <td></td>
            </tr>
            <tr>
                <th style="width:24%;">用户名　</th>
                <td><asp:TextBox ID="UserName" runat="server" MaxLength="12" autocomplete="off"></asp:TextBox><em>( 必填，3-10字符 )</em></td>
            </tr>
            <tr>
                <th>密　码　</th>
                <td><asp:TextBox ID="UserPassword" runat="server" TextMode="Password" MaxLength="12" autocomplete="off"></asp:TextBox><em>( 必填，3-10字符 )</em></td>
            </tr>
            <tr>    
                <th>重复密码</th>
                <td><asp:TextBox ID="UserPassword2" runat="server" TextMode="Password" MaxLength="12" autocomplete="off"></asp:TextBox><em>( 必填，两次密码须一致 )</em></td>
            </tr>
            <tr>    
                <th>昵　称　</th>
                <td><asp:TextBox ID="UserNickName" runat="server" MaxLength="20"></asp:TextBox><em>( 选填，可用中文 )</em></td>
            </tr>
            <tr>    
                <th>邮　箱　</th>
                <td><asp:TextBox ID="UserEmail" runat="server"></asp:TextBox><em>( 选填 )</em></td>                        
            </tr>
            <tr>    
                <th>工作日　</th>
                <td><asp:DropDownList ID="UserWorkDay" runat="server" MaxLength="20"></asp:DropDownList></td>                        
            </tr>
            <tr>
                <th></th>
                <td><asp:Label ID="Label1" runat="server" CssClass="error"></asp:Label></td>
            </tr>
            <tr>
                <th></th>
                <td><asp:Button ID="SubmitButtom" runat="server" Text="提交注册" onclick="SubmitButtom_Click" CssClass="btninput" /></td>
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
    </div>
    <div class="h10"></div>
    <!--内容结束-->
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js" Runat="Server">
<script type="text/javascript">
    
</script>
</asp:Content>
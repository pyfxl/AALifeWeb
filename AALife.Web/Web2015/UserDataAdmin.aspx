<%@ Page Title="数据管理" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserDataAdmin.aspx.cs" Inherits="UserDataAdmin_2015" %>

<%@ Register Src="UserControl/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose"></div>
<div id="content">
    <!--内容开始-->    
    <uc6:UserMenu ID="UserMenu1" runat="server" />
    <div class="maindiv userdiv">
        <table cellspacing="0" border="0" style="width:90%;" class="tableuser tablelogin">
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>1. 将外部Excel记账文件导入到AA生活记账。<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"><img src="/Images/Others/page_excel.png" alt="" title="" /> 模板下载</asp:LinkButton></td>
            </tr>
            <tr>
                <td><asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileinput" />&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="导入" CssClass="btninput" Width="60" onclick="Button1_Click" /></td>
            </tr>
            <tr>
                <td></td>
            </tr> 
            <tr>
                <td>2. 把AA生活记账的数据导出到Excel：</td>
            </tr>
            <tr>
                <td><asp:Button ID="Button2" runat="server" Text="导出数据" CssClass="btninput" onclick="Button2_Click" /></td>
            </tr>
            <tr>
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
        $("#content .tabletitle .u5").addClass("on");
    });
</script>
</asp:Content>
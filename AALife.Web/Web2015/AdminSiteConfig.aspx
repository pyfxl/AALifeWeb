<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="AdminSiteConfig.aspx.cs" Inherits="AdminSiteConfig" %>

<%@ Register Src="UserControl/AdminMenu.ascx" TagName="AdminMenu" TagPrefix="uc8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>网站管理|后台管理</title>
<link href="common/style.css" type="text/css" rel="stylesheet" />
<link rel="Shortcut Icon" href="favicon.ico" />
<script type="text/javascript" src="common/jquery.min.js"></script>
</head>
<body>
<form id="form1" runat="server">
<div id="Box">
    <uc8:AdminMenu ID="AdminMenu1" runat="server" />
    <script type="text/javascript">
        $("#Box #TopMenu .m5").addClass("cur");
    </script>
    <div id="Main">
        <div id="Left" style="width:100%;">
            <div id="ItemList">  
                <table cellspacing="0" border="1" style="width:100%;border-collapse:collapse;" class="detailtable">
                    <tr>
                        <td>网站名称</td>
                        <td><asp:TextBox ID="SiteNameBox" runat="server" Width="500"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>网站作者</td>
                        <td><asp:TextBox ID="SiteAuthorBox" runat="server" Width="500"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>关键字</td>
                        <td><asp:TextBox ID="SiteKeywordsBox" runat="server" TextMode="MultiLine" Width="500" Height="35"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>网站描述</td>
                        <td><asp:TextBox ID="SiteDescriptionBox" runat="server" TextMode="MultiLine" Width="500" Height="70"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>页显示数</td>
                        <td><asp:TextBox ID="PagePerNumberBox" runat="server" Width="500"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>工作日</td>
                        <td><asp:TextBox ID="UserWorkDayBox" runat="server" Width="500"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>预算率%</td>
                        <td><asp:TextBox ID="CategoryRateBox" runat="server" Width="500"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>网站贴士</td>
                        <td><asp:TextBox ID="SiteTipsBox" runat="server" TextMode="MultiLine" Width="500" Height="60"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>公告版本</td>
                        <td><asp:TextBox ID="MessageCodeBox" runat="server" Width="500"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>网站公告</td>
                        <td><asp:TextBox ID="SiteMessageBox" runat="server" TextMode="MultiLine" Width="500" Height="60"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>手机公告</td>
                        <td><asp:TextBox ID="PhoneMessageBox" runat="server" TextMode="MultiLine" Width="500" Height="60"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td><asp:Button ID="Button1" runat="server" Text="确认无误，修改表单" CssClass="btninput" onclick="Button1_Click" /></td>
                    </tr>
                </table>        
            </div>
        <div class="clear"></div>
    </div>
</div>
</form>
</body>
</html>
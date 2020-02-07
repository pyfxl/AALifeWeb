<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminItemList.aspx.cs" Inherits="AdminItemList" %>

<%@ Register Src="UserControl/AdminMenu.ascx" TagName="AdminMenu" TagPrefix="uc8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>消费列表|后台管理</title>
<link href="common/style.css" type="text/css" rel="stylesheet" />
<link rel="Shortcut Icon" href="favicon.ico" />
<script type="text/javascript" src="common/jquery.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("#KeyBox").focus();
    });
</script>
</head>
<body>
<form id="form1" runat="server">
<div id="Box">
    <uc8:AdminMenu ID="AdminMenu1" runat="server" />
    <script type="text/javascript">
        $("#Box #TopMenu .m3").addClass("cur");
    </script>
    <div id="Main">
        <div id="Left" style="width:100%;">
            <div id="ItemList">
                <div>
                    <asp:Label ID="Label1" runat="server" />&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="查看所有消费列表" CssClass="btninput" OnClick="Button1_Click" UseSubmitBehavior="false" />&nbsp;&nbsp;
                    关键字：<asp:TextBox ID="KeyBox" runat="server" Width="120px"></asp:TextBox>
                    <asp:Button ID="Button4" runat="server" Text="搜索" CssClass="btninput" OnClick="Button4_Click" />
                </div>
                <div class="h10"></div>
                <asp:GridView ID="List" runat="server" Width="100%" BorderWidth="0" OnPageIndexChanging="List_PageIndexChanging">
                    <PagerStyle CssClass="bottompager" />
                    <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" PageButtonCount="20" />
                </asp:GridView>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</div>
</form>
</body>
</html>

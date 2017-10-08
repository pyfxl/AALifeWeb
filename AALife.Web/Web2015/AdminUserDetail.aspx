<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminUserDetail.aspx.cs" Inherits="AdminUserDetail" %>

<%@ Register Src="UserControl/AdminMenu.ascx" TagName="AdminMenu" TagPrefix="uc8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>用户数据|后台管理</title>
<link href="common/style.css" type="text/css" rel="stylesheet" />
<link rel="Shortcut Icon" href="favicon.ico" />
<script type="text/javascript" src="common/jquery.min.js"></script>
<script type="text/javascript">
    $(function () {
        var img_td = $("#UserList tr").eq(1).find("td").eq(4);
        var img_name = img_td.html();
        var img_url = img_name.length > 20 ? img_name : "/Images/Users/" + img_name;
        img_td.html("<img src='" + img_url + "' width='50' height='50' />");
    });
</script>
</head>
<body>
<form id="form1" runat="server">
<div id="Box">
    <uc8:AdminMenu ID="AdminMenu1" runat="server" />
    <script type="text/javascript">
        $("#Box #TopMenu .m2").addClass("cur");
    </script>
    <div id="Main">
        <div id="Left" style="width:100%;">
            <div id="ItemList">
                <asp:GridView ID="UserList" runat="server" Width="100%" BorderWidth="0"></asp:GridView>
                <div class="h10"></div>
                <asp:GridView ID="OAuthList" runat="server" Width="100%" BorderWidth="0"></asp:GridView>
                <div class="h10"></div>
                <asp:GridView ID="UserCategoryList" runat="server" Width="100%" BorderWidth="0"></asp:GridView>
                <div class="h10"></div>
                <asp:GridView ID="ZhuanTiList" runat="server" Width="100%" BorderWidth="0"></asp:GridView>
                <div class="h10"></div>
                <asp:GridView ID="CardList" runat="server" Width="100%" BorderWidth="0"></asp:GridView>
                <div class="h10"></div>
                <asp:GridView ID="List" runat="server" Width="100%" BorderWidth="0" AllowPaging="true" PageSize="36" OnPageIndexChanging="List_PageIndexChanging">
                    <PagerStyle CssClass="bottompager" />
                    <PagerSettings Position="Bottom" Mode="NumericFirstLast" PageButtonCount="20" />
                </asp:GridView>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</div>
</form>
</body>
</html>

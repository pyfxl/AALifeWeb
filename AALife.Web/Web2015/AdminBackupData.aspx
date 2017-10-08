<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminBackupData.aspx.cs" Inherits="AdminBackupData" %>

<%@ Register Src="UserControl/AdminMenu.ascx" TagName="AdminMenu" TagPrefix="uc8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>备份数据|后台管理</title>
<script type="text/javascript" src="common/jquery.min.js"></script>
<script type="text/javascript" src="common/development-bundle/ui/minified/jquery.ui.core.min.js"></script>
<script type="text/javascript" src="common/development-bundle/ui/minified/jquery.ui.widget.min.js"></script>
<script type="text/javascript" src="common/development-bundle/ui/minified/jquery.ui.position.min.js"></script>
<script type="text/javascript" src="common/development-bundle/ui/minified/jquery.ui.autocomplete.min.js"></script>
<script type="text/javascript" src="common/development-bundle/ui/minified/jquery.ui.datepicker.min.js"></script>
<script type="text/javascript" src="common/development-bundle/ui/i18n/jquery.ui.datepicker-zh-CN.js"></script>
<link rel="stylesheet" href="common/development-bundle/themes/base/jquery.ui.all.css"/>
<link href="common/style.css" type="text/css" rel="stylesheet" />
<link rel="Shortcut Icon" href="favicon.ico" />
<script type="text/javascript">
    $(function () {
        $("#BeginDateBox").datepicker({
            showOtherMonths: true,
            selectOtherMonths: true
        }); 
        $("#EndDateBox").datepicker({
            showOtherMonths: true,
            selectOtherMonths: true
        });
    });
</script>
</head>
<body>
<form id="form1" runat="server">
<div id="Box">
    <uc8:AdminMenu ID="AdminMenu1" runat="server" />
    <script type="text/javascript">
        $("#Box #TopMenu .m4").addClass("cur");
    </script>
    <div id="Main">
        <div id="Left" style="width:100%;">
            <div id="ItemList">
                <div>
                    <asp:TextBox ID="BeginDateBox" runat="server" Width="80px"></asp:TextBox>
                    <asp:TextBox ID="EndDateBox" runat="server" Width="80px"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" Text="备份" OnCommand="Button2_Command" CommandName="mssql" CommandArgument="mssql" CssClass="btninput" />
                    <asp:Button ID="Button4" runat="server" Text="恢复" OnCommand="Button4_Click" CssClass="btninput" OnClientClick="return confirm('确定要恢复吗？');" />&nbsp;&nbsp;
                    <asp:TextBox ID="UserIDBox" runat="server" Width="60px"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="导出到APP数据" CssClass="btninput" OnClick="Button1_Click" UseSubmitBehavior="false" />
                    <asp:Button ID="Button3" runat="server" Text="删除用户数据" CssClass="btninput" OnClick="Button3_Click" UseSubmitBehavior="false" />
                    <asp:Button ID="Button5" runat="server" Text="恢复APP数据" CssClass="btninput" OnClick="Button5_Click" UseSubmitBehavior="false" />
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</div>
</form>
</body>
</html>

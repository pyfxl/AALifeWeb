<%@ Page Title="用户转账明细" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserZhuanZhangDetail.aspx.cs" Inherits="UserZhuanZhangDetail" %>

<%@ Register Src="UserControl/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    $(function () {
        fixhead();
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="content">
    <!--内容开始-->
    <uc6:UserMenu ID="UserMenu1" runat="server" />
    <table cellspacing="0" border="0" style="width:100%;" class="tabledate">
        <tr>
            <td style="width:33%;"></td>
            <td style="width:34%;"><strong>用户转账明细</strong></td>
            <td style="width:33%;"><a href="UserCardAdmin.aspx">&lt;&lt; 返回列表</a></td>
        </tr>
    </table>
    <div class="maindiv">
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
            <tr>
                <th style="width:20%;">编号</th>
                <th style="width:20%;">转账来自</th>
                <th style="width:20%;">转账给</th>
                <th style="width:20%;" class="cellprice">转账金额</th>
                <th style="width:20%;">转账日期</th>
            </tr>
            <asp:Repeater ID="ItemList" runat="server">
            <ItemTemplate>
            <tr>
                <td><%# ItemList.Items.Count + 1 %></td>
                <td><%# Eval("ZhuanZhangFromName") %></td>
                <td><%# Eval("ZhuanZhangToName") %></td>
                <td class="cellprice">￥<%# Eval("ZhuanZhangMoney", "{0:0.0##}") %></td>
                <td><%# Eval("ZhuanZhangDate", "{0:yyyy-MM-dd}") %></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr>
                <td colspan="5" class="noitemcell"><asp:Label ID="Label1" runat="server" Text="没有转账记录。" Visible="<%# ItemList.Items.Count == 0 %>"></asp:Label></td>
            </tr>
            </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div class="h10"></div>
    <!--内容结束-->
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js" Runat="Server">
<script type="text/javascript">
    $(function () {
        $("#content .tabletitle .u7").addClass("on");

        $(".tableadd tr td").eq(0).html(Number($(".tablelist").eq(0).find("tr").eq(-2).find("td").eq(0).html()) + 1);
    });
</script>
</asp:Content>
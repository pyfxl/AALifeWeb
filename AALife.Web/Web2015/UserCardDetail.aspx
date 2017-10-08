<%@ Page Title="钱包消费明细" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserCardDetail.aspx.cs" Inherits="UserCardDetail" %>

<%@ Register Src="UserControl/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    $(function () {
        fixhead();
    });
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose" class="hasDatepicker"></div>
<div id="content">
    <!--内容开始-->
    <uc6:UserMenu ID="UserMenu1" runat="server" />
    <table cellspacing="0" border="0" style="width:100%;" class="tabledate">
        <tr>
            <td style="width:33%;"></td>
            <td style="width:34%;"><strong>钱包消费明细</strong></td>
            <td style="width:33%;"><a href="UserCardAdmin.aspx">&lt;&lt; 返回列表</a></td>
        </tr>
    </table> 
    <div class="maindiv">
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
            <tr>
                <th style="width:20%;">分类</th>
                <th style="width:20%;">商品名称</th>
                <th style="width:20%;">日期</th>
                <th style="width:20%;" class="cellprice">价格</th>
                <th style="width:20%;">操作</th>
            </tr>
            <asp:Repeater ID="PriceTop" runat="server">
            <ItemTemplate>
            <tr>
                <td><%# Eval("ItemTypeName") %></td>
                <td><%# Eval("ItemName") %></td>
                <td><%# Eval("ItemBuyDate", "{0:yyyy-MM-dd}") %></td>
                <td class="cellprice">￥<%# Eval("ItemPrice", "{0:0.0##}") %></td>
                <td><a href="ItemList.aspx?date=<%# Eval("ItemBuyDate", "{0:yyyy-MM-dd}") %>">查看详细</a></td>                    
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr>
                <td colspan="5" class="noitemcell"><asp:Label ID="Label1" runat="server" Text="没有消费记录。" Visible='<%# PriceTop.Items.Count == 0 %>'></asp:Label></td>
            </tr>
            </FooterTemplate>
            </asp:Repeater>            
            <tr class="totalcell">
                <td>总计</td>
                <td colspan="2" class="celldate">收入 ￥<asp:Label ID="Label2" runat="server"></asp:Label></td>
                <td colspan="2" class="celldate">支出 ￥<asp:Label ID="Label3" runat="server"></asp:Label></td>
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
        $("#content .tabletitle .u7").addClass("on");

        $(".tableadd tr td").eq(0).html(Number($(".tablelist").eq(0).find("tr").eq(-2).find("td").eq(0).html()) + 1);
    });
</script>
</asp:Content>
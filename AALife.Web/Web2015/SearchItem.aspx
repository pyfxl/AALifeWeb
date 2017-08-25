<%@ Page Title="搜索消费" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="SearchItem.aspx.cs" Inherits="SearchItem_2015" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    $(function () {	
        fixhead();
        fixheadtop();
    });
</script>
<style type="text/css">
table.tableuser {
    background: #FFF;
}
table.tableuser td, table.tableuser th {
    padding-bottom: 0px;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="content">
    <!--内容开始-->  
    <div class="title"><img src="/Images/Others/magnifier.png" alt="" title="" />&nbsp;&nbsp;搜索消费</div>
    <table cellspacing="0" border="0" style="width:100%;" class="tableuser">
        <tr>
            <td style="width:20%;"></td>
            <td style="width:10%;" align="center"><strong>关键字</strong></td>
            <td style="width:40%;"><asp:TextBox ID="Keywords" runat="server" MaxLength="20" Width="278"></asp:TextBox></td>
            <td style="width:20%;"><asp:Button ID="Button2" runat="server" Text="搜索" OnClick="SubmitButtom_Click" CssClass="btninput" /></td>
            <td style="width:10%;"></td>
        </tr>
    </table>
    <div class="maindiv">
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
            <tr>
                <th style="width:8%;">选择</th>
                <th style="width:8%;">分类</th>
                <th style="width:17%;">商品名称</th>
                <th style="width:17%;">商品类别</th>
                <th style="width:17%;" class="cellprice">价格</th>
                <th style="width:17%;">日期</th>
                <th style="width:16%;">操作</th>
            </tr>
            <asp:Repeater ID="SearchItemList" runat="server">
            <ItemTemplate>
            <tr>
                <td><asp:CheckBox ID="ItemCheckBox" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="ItemCheckBox_CheckedChanged" CssClass="radioinput"></asp:CheckBox></td>
                <td><asp:Label ID="ItemTypeLab" runat="server" Text='<%# Eval("ItemTypeName")%>' /></td>
                <td><%# Eval("ItemName")%></td>
                <td><%# Eval("CategoryTypeName")%></td>
                <td class="cellprice">￥<asp:Label ID="ItemPriceLab" runat="server" Text='<%# Eval("ItemPrice", "{0:N2}")%>' /></td>
                <td><%# Eval("ItemBuyDate", "{0:yyyy-MM-dd}")%></td>
                <td><a href="ItemList.aspx?date=<%# Eval("ItemBuyDate", "{0:yyyy-MM-dd}")%>">查看详细</a></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr>
                <td colspan="7" class="noitemcell"><asp:Label ID="Label1" runat="server" Text="没有消费记录。" Visible='<%# SearchItemList.Items.Count == 0 %>'></asp:Label></td>
            </tr>
            </FooterTemplate>
            </asp:Repeater>
            <tr class="totalcell">
                <td colspan="2">总计</td>
                <td colspan="2" class="celldate">收入 ￥<asp:Label ID="Label2" runat="server"></asp:Label></td>
                <td colspan="2" class="celldate">支出 ￥<asp:Label ID="Label3" runat="server"></asp:Label></td>
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
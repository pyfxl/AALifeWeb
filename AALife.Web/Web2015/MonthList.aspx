<%@ Page Title="每月消费" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="MonthList.aspx.cs" Inherits="MonthList" %>

<%@ Register Src="UserControl/MonthMenu.ascx" TagName="MonthMenu" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" src="common/development-bundle/ui/monthpicker.js"></script>
<script type="text/javascript">
    $(function () {
        $("#datechoose").datepicker();
        $("#datepicker").click(function () { datechoose($(this)); });
        $(".ui-state-default").click(function () { location.href = "MonthList.aspx?date=" + ($(".ui-datepicker-year").html() + "-" + this.getAttribute("data-month") + "-01"); });
        
        $(".expanddown").click(function () {
            var tr = $(this).parent().parent();
            //alert(tr.next().find("table").html());
            if (tr.next().find("table").length > 0) {
                $(this).html() == "展开" ? $(this).html("收缩") : $(this).html("展开");
                tr.toggleClass("expandtr");
                tr.next().toggle();
            } else {
                $(this).html("收缩");
                tr.addClass("expandtr");

                $.ajax({
                    type: "post",
                    url: "/AutoItemListJson.aspx?term=" + $(this).attr("ref"),
                    dataType: "html",
                    cache: false,
                    beforeSend: function () {
                        
                    }, success: function (response) {
                        //alert(response);
                        tr.after("<tr class=\"expanddiv\"><td colspan=\"6\">" + response + "</td></tr>");
                    }, complete: function () {

                    }
                });
            }
        });

        fixhead();
        fixheadtop();
    });
</script>
<style type="text/css">
.ui-datepicker td span, .ui-datepicker td a { height: 18px; line-height: 18px; }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose" class="hasDatepicker"></div>
<div id="content">
    <!--内容开始-->
    <uc2:MonthMenu ID="MonthMenu1" runat="server" />
    <div class="maindiv userdiv">
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
            <tr>
                <th style="width:16%;">日期 <img src="/Images/Others/zim.gif" border="0" alt="此列可点击" title="此列可点击" /></th>
                <th style="width:17%;" class="cellprice">收入</th>
                <th style="width:17%;" class="cellprice">支出</th>
                <th style="width:17%;" class="cellprice">借出还入</th>
                <th style="width:17%;" class="cellprice">借入还出</th>
                <th style="width:16%;">操作</th>
            </tr>
            <asp:Repeater ID="List" runat="server">
            <ItemTemplate>
            <tr>
                <td><a href="ItemList.aspx?date=<%# Eval("ItemBuyDate", "{0:yyyy-MM-dd}") %>"><%# Convert.ToDateTime(Eval("ItemBuyDate")).ToString("MM-dd") + "&nbsp;&nbsp;&nbsp;" + Utility.GetWeekStrShort(Convert.ToInt32(Convert.ToDateTime(Eval("ItemBuyDate")).DayOfWeek))%></a></td>
                <td class="cellprice">￥<%# Eval("ShouRuPrice", "{0:0.0##}") %></td>
                <td class="cellprice">￥<%# Eval("ZhiChuPrice", "{0:0.0##}") %></td>
                <td class="cellprice <%# ItemHelper.JieHuanColor(Eval("JiePrice").ToString(), 1) %>">￥<%# Eval("JiePrice", "{0:0.##}") %></td>
                <td class="cellprice <%# ItemHelper.JieHuanColor(Eval("HuanPrice").ToString(), 0) %>">￥<%# Eval("HuanPrice", "{0:0.##}") %></td>
                <td><a href="javascript:void(0);" class="expanddown" ref="<%# Eval("ItemBuyDate", "{0:yyyy-MM-dd}") %>">展开</a></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr>
                <td colspan="6" class="noitemcell"><asp:Label ID="Label1" runat="server" Text="没有消费记录。" Visible='<%# List.Items.Count == 0 %>'></asp:Label></td>
            </tr>
            </FooterTemplate>
            </asp:Repeater>
            <tr>
                <th>总计</th>
                <th class="cellprice">￥<asp:Label ID="Label3" runat="server"></asp:Label></th>
                <th class="cellprice">￥<asp:Label ID="Label2" runat="server"></asp:Label></th>
                <th class="cellprice <%= ItemHelper.JieHuanColor(this.Label4.Text, 1) %>">未还&nbsp;&nbsp;&nbsp;&nbsp;￥<asp:Label ID="Label4" runat="server"></asp:Label></th>
                <th class="cellprice <%= ItemHelper.JieHuanColor(this.Label5.Text, 1) %>">欠还&nbsp;&nbsp;&nbsp;&nbsp;￥<asp:Label ID="Label5" runat="server"></asp:Label></th>
                <th></th>
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
        $("#menu table tr td").eq(1).addClass("on");
    });
</script>
</asp:Content>
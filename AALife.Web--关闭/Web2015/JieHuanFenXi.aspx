<%@ Page Title="收支借还分析" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="JieHuanFenXi.aspx.cs" Inherits="JieHuanFenXi" %>

<%@ Register Src="UserControl/FenXiMenu.ascx" TagName="FenXiMenu" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    $(function () {
        fixhead();

        $(".dateprev").click(function () {
            location.href = "JieHuanFenXi.aspx?date=<%=today.AddYears(-1).ToString("yyyy") %>-<%=today.ToString("MM-dd") %>&type=month";
        });
        $(".datenext").click(function () {
            location.href = "JieHuanFenXi.aspx?date=<%=today.AddYears(1).ToString("yyyy") %>-<%=today.ToString("MM-dd") %>&type=month";
        });

        $(".expanddown").click(function () {
            var tr = $(this).parent().parent();
            if (tr.next().find("table").length > 0) {
                $(this).html() == "展开" ? $(this).html("收缩") : $(this).html("展开");
                tr.toggleClass("expandtr");
                tr.next().toggle();
            } else {
                $(this).html("收缩");
                tr.addClass("expandtr");

                $.ajax({
                    type: "post",
                    url: "/AutoJieHuanListJson.aspx?term=" + $(this).attr("ref"),
                    dataType: "html",
                    cache: false,
                    beforeSend: function () {

                    }, success: function (response) {
                        tr.after("<tr class=\"expanddiv\"><td colspan=\"8\">" + response + "</td></tr>");
                    }, complete: function () {

                    }
                });
            }
        });
    });
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose"></div>
<div id="content">
    <!--内容开始-->
    <uc4:FenXiMenu ID="FenXiMenu1" runat="server" />   
    <table cellspacing="0" border="0" style="width:100%;" class="tabledate">
        <tr>
            <td style="width:10%;">&nbsp;</td>
            <td style="width:27%;"><a href="JieHuanFenXi.aspx">全部</a></td>
            <td style="width:26%;"><a href="javascript:void(0)" class="dateprev">&lt;上年</a>&nbsp;&nbsp;&nbsp;<strong>日期：</strong><%=Convert.ToDateTime(Session["TodayDate"]).ToString("yyyy年") %>&nbsp;&nbsp;<img src="/Images/Others/calendar.png" alt="" title="" />&nbsp;&nbsp;&nbsp;<a href="javascript:void(0)" class="datenext">下年&gt;</a></td>
            <td style="width:27%;"><a href="javascript:history.go(-1);">&lt;&lt;返回</a></td>
            <td style="width:10%;">&nbsp;</td>
        </tr>
    </table>
    <div class="maindiv">
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
            <tr>
                <th style="width:14%;">日期 <img src="/Images/Others/zim.gif" border="0" alt="此列可点击" title="此列可点击" /></th>
                <th style="width:15%;" class="cellprice">总收入</th>
                <th style="width:15%;" class="cellprice">总支出</th>
                <th style="width:14%;" class="cellprice">总借出</th>
                <th style="width:14%;" class="cellprice">总还入</th>
                <th style="width:14%;" class="cellprice">总借入</th>
                <th style="width:14%;" class="cellprice">总还出</th>
            </tr>
            <asp:Repeater ID="List" runat="server">
            <ItemTemplate>
            <tr>
                <td><a href="<%# GetURL(Eval("ItemBuyDate").ToString()) %>"><%# Eval("ItemBuyDate") %></a></td>
                <td class="cellprice">￥<%# Eval("ShouRuPrice", "{0:0.0##}") %></td>   
                <td class="cellprice">￥<%# Eval("ZhiChuPrice", "{0:0.0##}") %></td>   
                <td class="cellprice <%# ItemHelper.JieHuanColor(Eval("JieChuPrice").ToString(), 1) %>">￥<%# Eval("JieChuPrice", "{0:0.##}") %></td>   
                <td class="cellprice <%# ItemHelper.JieHuanColor(Eval("HuanRuPrice").ToString(), 0) %>">￥<%# Eval("HuanRuPrice", "{0:0.##}") %></td>   
                <td class="cellprice <%# ItemHelper.JieHuanColor(Eval("JieRuPrice").ToString(), 1) %>">￥<%# Eval("JieRuPrice", "{0:0.##}") %></td>   
                <td class="cellprice <%# ItemHelper.JieHuanColor(Eval("HuanChuPrice").ToString(), 0) %>">￥<%# Eval("HuanChuPrice", "{0:0.##}") %></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr>
                <td colspan="7" class="noitemcell"><asp:Label ID="Label1" runat="server" Text="没有消费记录。" Visible='<%# List.Items.Count == 0 %>'></asp:Label></td>
            </tr>
            </FooterTemplate>
            </asp:Repeater>
            <tr>
                <th>总计</th>
                <th class="cellprice">￥<asp:Label ID="Label3" runat="server"></asp:Label></th>
                <th class="cellprice">￥<asp:Label ID="Label2" runat="server"></asp:Label></th>
                <th class="cellprice <%= ItemHelper.JieHuanColor(this.Label4.Text, 1) %>">￥<asp:Label ID="Label4" runat="server"></asp:Label></th>
                <th class="cellprice <%= ItemHelper.JieHuanColor(this.Label5.Text, 0) %>">￥<asp:Label ID="Label5" runat="server"></asp:Label></th>
                <th class="cellprice <%= ItemHelper.JieHuanColor(this.Label6.Text, 1) %>">￥<asp:Label ID="Label6" runat="server"></asp:Label></th>
                <th class="cellprice <%= ItemHelper.JieHuanColor(this.Label7.Text, 0) %>">￥<asp:Label ID="Label7" runat="server"></asp:Label></th>
            </tr>
            <tr>
                <th class="<%= ItemHelper.JieHuanColor(this.Label8.Text, 0) %>">差异</th>
                <th colspan="2" class="<%= ItemHelper.JieHuanColor(this.Label8.Text, 0) %>">结存&nbsp;&nbsp;&nbsp;&nbsp;￥<asp:Label ID="Label8" runat="server"></asp:Label></th>
                <th colspan="2" class="<%= ItemHelper.JieHuanColor(this.Label9.Text, 1) %>">未还&nbsp;&nbsp;&nbsp;&nbsp;￥<asp:Label ID="Label9" runat="server"></asp:Label></th>
                <th colspan="2" class="<%= ItemHelper.JieHuanColor(this.Label10.Text, 1) %>">欠还&nbsp;&nbsp;&nbsp;&nbsp;￥<asp:Label ID="Label10" runat="server"></asp:Label></th>
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
        $("#menu table tr td").eq(3).addClass("on");

        $("#content .tabletitle .f6").addClass("on");
    });
</script>
</asp:Content>
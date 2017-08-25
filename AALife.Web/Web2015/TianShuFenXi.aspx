<%@ Page Title="消费天数分析" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="TianShuFenXi.aspx.cs" Inherits="TianShuFenXi_2015" %>

<%@ Register Src="UserControl/FenXiMenu.ascx" TagName="FenXiMenu" TagPrefix="uc4" %>
<%@ Register Src="UserControl/ViewTitle.ascx" TagName="ViewTitle" TagPrefix="uc8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    var arrChart = null;

    $(function () {
        $("#datechoose").datepicker({
            showOtherMonths: true,
            selectOtherMonths: true,
            defaultDate: "<%=Session["TodayDate"].ToString() %>",
            onSelect: function (date, format) {
                location.href = "TianShuFenXi.aspx?date=" + date;
            },
            onChangeMonthYear: function(year, month, format) {
                location.href = "TianShuFenXi.aspx?date=" + year + "-" + fullmonth(month) + "-" + "<%=Convert.ToDateTime(Session["TodayDate"]).ToString("dd") %>";
            }
        });
        
        $("#datepicker").live("click", function () { datechoose($(this)); });

        $(".tabledate input[type=radio]").click(function(){
            if($(this).val() == "图表") {
                $.cookie("view", "1");
                getview();
            } else {
                $.cookie("view", "0");
                getview();
            }
        });

        fixhead();

        getview();

        arrChart = $("#<%=hidChartData.ClientID %>").val().split(",");

        bindscroll();
        var load = false;
        var page = 1;
        function longdata(){
            if(($(this).scrollTop() + $(this).height()) == $(this).get(0).scrollHeight) {
                $(".maindiv").unbind("scroll");
                page++;
                
                $.ajax({
                    type: "post",
                    url: "AutoTSFXJson.aspx?page=" + page,
                    dataType: "json",
                    cache: false,
                    beforeSend: function () {
                        if(!load) loading();
                    }, success: function (response) {
                        log(response);
                    }, complete: function () {
                        bindscroll();
                    }
                });
            }
        }        
        function log(data) {
            load = false;
            $(".maindiv table").eq(0).find("tr:last").remove();
            for (var i = 0; i < data.length; i++) {
                $(".maindiv table").eq(0).append("<tr><td>" + data[i].ItemTypeName + "</td><td>" + data[i].ItemName + "</td><td>" + data[i].ItemBuyDate + "</td><td>" + data[i].NotBuy + " 天前购买</td><td><a href='ItemList.aspx?date=" + data[i].ItemBuyDate + "'>查看详情</a></td></tr>");
            }
        }        
        function loading() {
            load = true;
            $(".maindiv table").eq(0).append("<tr><td colspan='5'><img src='/Images/Others/ui-anim_basic_16x16.gif' alt='' title='' /> 努力加载中...</td></tr>");
        }
        function bindscroll() {
            if(($(".maindiv table").eq(0).find("tr").length - 2) != <%=howManyItems %>) {
                $(".maindiv").bind("scroll", longdata);
            }
        }
    });

    function chart_click(index) {
        location.href = "ItemList.aspx?date=" + arrChart[index];
    }
</script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose"></div>
<div id="content">
    <!--内容开始-->
    <uc4:FenXiMenu ID="FenXiMenu1" runat="server" />
    <uc8:ViewTitle ID="ViewTitle1" runat="server" />
    <div class="maindiv">
        <div class="chartdiv">
            <script type="text/javascript" src="/ofcgwt/swfobject.js"></script>
            <script type="text/javascript">
                swfobject.embedSWF(
                "/ofcgwt/open-flash-chart-SimplifiedChinese.swf", "my_chart", "100%", "431",
                "9.0.0", "/ofcgwt/expressInstall.swf",
                { "data-file": "TSFXChartJson.aspx" }, { "wmode": "transparent" }
                );
            </script>
            <div id="my_chart"></div>
            <input type="hidden" id="hidChartData" runat="server" />
        </div>
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
            <tr>
                <th style="width:20%;">分类</th>
                <th style="width:20%;">商品名称</th>
                <th style="width:20%;">最后消费</th>
                <th style="width:20%;">天数</th>
                <th style="width:20%;">操作</th>
            </tr>
            <asp:Repeater ID="List" runat="server">
            <ItemTemplate>
            <tr>
                <td><%# Eval("ItemTypeName") %></td>
                <td><%# Eval("ItemName") %></td>
                <td><%# Eval("ItemBuyDate", "{0:yyyy-MM-dd}") %></td>
                <td><%# Eval("NotBuy") %> 天前购买</td>
                <td><a href="ItemList.aspx?date=<%# Eval("ItemBuyDate", "{0:yyyy-MM-dd}") %>">查看详细</a></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr>
                <td colspan="5" class="noitemcell"><asp:Label ID="Label1" runat="server" Text="没有消费记录。" Visible="<%# List.Items.Count == 0 %>"></asp:Label></td>
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
        $("#menu table tr td").eq(3).addClass("on");

        $("#content .tabletitle .f3").addClass("on");
    });
</script>
</asp:Content>
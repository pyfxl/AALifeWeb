<%@ Page Title="网上记账" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" Inherits="AALife.WebMvc.Web2018._Default" Codebehind="Default.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="js" Runat="Server">
<script type="text/javascript">
    var chart_arr = "<%=chartDate %>".split(",");

    function chart_click(index) {
        //alert(chart_arr[index]);
        location.href = "ItemQuery.aspx?date=" + chart_arr[index] + "&showType=d";
    }

    function chart_open(index) {
        //alert(chart_arr[index]);
        location.href = "ItemGroup.aspx?date=<%=curDate%>&" + chart_arr[index];
    }

    $(function () {

        //图片选择日期
        $("#datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            showOn: "button",
            buttonImage: "/theme/images/dot_10.gif",
            buttonImageOnly: true,
            buttonText: "选择日期",
            yearRange: "-10:+10",
            dateFormat: "yy-mm-dd",
            defaultDate : "<%=curDate %>",
            onSelect: function (date, inst) {
                $(this).val("<%=QueryHelper.GetSpinDateVal(curDate, 0, "d") %>");
                location.href = "Default.aspx?date=" + date + "&chartType=<%=chartType %>";
            },
            beforeShow: function (input, inst) {
                $.datepicker._pos = $.datepicker._findPos(input);
                $.datepicker._pos[0] = $("#m_right .ui-spinner").offset().left;
                $.datepicker._pos[1] = $("#m_right .ui-spinner").offset().top + 22;
            }
        });

        //选择日期
        $("#datepicker").spinner({
            spin: function (event, ui) {
                var ref = $(this).attr("ref-spin");
                var date = $(this).attr("ref-date");
                //if (date == "") return false;
                if (ui.value > ref) {
                    $(this).attr("ref-spin", ui.value);
                    $(this).spinner("value", "<%=QueryHelper.GetSpinDate(curDate, 1, "d") %>");
                    location.href = "Default.aspx?date=<%=QueryHelper.GetSpinDateVal(curDate, 1, "d") %>&chartType=<%=chartType %>";
                    return false;
                } else {
                    $(this).attr("ref-spin", ui.value);
                    $(this).spinner("value", "<%=QueryHelper.GetSpinDate(curDate, -1, "d") %>");
                    location.href = "Default.aspx?date=<%=QueryHelper.GetSpinDateVal(curDate, -1, "d") %>&chartType=<%=chartType %>";
                    return false;
                }
            }
        });

        //显示下拉
        $("#<%=ChartTypeDropDown.ClientID %>").multiselect({
            multiple: false,
            header: false,
            selectedList: 1,
            minWidth: 130,
            height: 96,
            click: function (event, ui) {
                setTimeout(charttypeclick, 0);
            }
        });

        function charttypeclick() {
            location.href = "Default.aspx?date=<%=curDate %>&chartType=" + $("#<%=ChartTypeDropDown.ClientID %>").val();
        }

    });

</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" Runat="Server">
    <div class="r_title">
        <div class="r_title_home">
            <input type="text" id="datepicker" style="width:100px;" ref-spin="0" ref-date="<%=QueryHelper.GetSpinDateVal(curDate, 0, "d") %>" value="<%=QueryHelper.GetSpinDate(curDate, 0, "d") %>" readonly="true" />
        </div>
    </div>
    <div id="r_home">
        <div class="r_home_top">
            <table border="1" class="tablehome" id="shouzhi">
                <tr>
                    <th style="width:115px;" class="firsttitle">&nbsp;</th>
                    <th style="width:115px;" class="shoutitle">收入</th>
                    <th style="width:115px;" class="zhititle">支出</th>
                    <th style="width:115px;" class="jietitle">结存</th>
                </tr>
                <tr>
                    <td class="homelink"><a href="ItemQuery.aspx?date=<%=curDate %>&showType=d">今日 &gt;</a></td>
                    <td><asp:HyperLink ID="ShouRuLab" runat="server" CssClass="shoucolor hometotal" NavigateUrl='<%# "ItemQuery.aspx?date=" + curDate + "&showType=d&itemType=sr" %>' /></td>
                    <td><asp:HyperLink ID="ZhiChuLab" runat="server" CssClass="zhicolor hometotal" NavigateUrl='<%# "ItemQuery.aspx?date=" + curDate + "&showType=d&itemType=zc" %>' /></td>
                    <td><asp:HyperLink ID="JieCunLab" runat="server" CssClass="jiecolor hometotal" NavigateUrl='<%# "ItemQuery.aspx?date=" + curDate + "&showType=d&itemType=sr,zc" %>' /></td>
                </tr>
                <tr>
                    <td class="coltitle homelink"><a href="ItemQuery.aspx?date=<%=curDate %>&showType=w">本周 &gt;</a></td>
                    <td class="coltitle"><asp:HyperLink ID="ShouRuWeekLab" runat="server" CssClass="shoucolor hometotal" NavigateUrl='<%# "ItemQuery.aspx?date=" + curDate + "&showType=w&itemType=sr" %>' /></td>
                    <td class="coltitle"><asp:HyperLink ID="ZhiChuWeekLab" runat="server" CssClass="zhicolor hometotal" NavigateUrl='<%# "ItemQuery.aspx?date=" + curDate + "&showType=w&itemType=zc" %>' /></td>
                    <td class="coltitle"><asp:HyperLink ID="JieCunWeekLab" runat="server" CssClass="jiecolor hometotal" NavigateUrl='<%# "ItemQuery.aspx?date=" + curDate + "&showType=w&itemType=sr,zc" %>' /></td>
                </tr>
                <tr>
                    <td class="homelink"><a href="ItemQuery.aspx?date=<%=curDate %>&showType=m">本月 &gt;</a></td>
                    <td><asp:HyperLink ID="ShouRuMonthLab" runat="server" CssClass="shoucolor hometotal" NavigateUrl='<%# "ItemQuery.aspx?date=" + curDate + "&showType=m&itemType=sr" %>' /></td>
                    <td><asp:HyperLink ID="ZhiChuMonthLab" runat="server" CssClass="zhicolor hometotal" NavigateUrl='<%# "ItemQuery.aspx?date=" + curDate + "&showType=m&itemType=zc" %>' /></td>
                    <td><asp:HyperLink ID="JieCunMonthLab" runat="server" CssClass="jiecolor hometotal" NavigateUrl='<%# "ItemQuery.aspx?date=" + curDate + "&showType=m&itemType=sr,zc" %>' /></td>
                </tr>
                <tr>
                    <td class="coltitle homelink"><a href="ItemQuery.aspx?date=<%=curDate %>&showType=y">今年 &gt;</a></td>
                    <td class="coltitle"><asp:HyperLink ID="ShouRuYearLab" runat="server" CssClass="shoucolor hometotal" NavigateUrl='<%# "ItemQuery.aspx?date=" + curDate + "&showType=y&itemType=sr" %>' /></td>
                    <td class="coltitle"><asp:HyperLink ID="ZhiChuYearLab" runat="server" CssClass="zhicolor hometotal" NavigateUrl='<%# "ItemQuery.aspx?date=" + curDate + "&showType=y&itemType=zc" %>' /></td>
                    <td class="coltitle"><asp:HyperLink ID="JieCunYearLab" runat="server" CssClass="jiecolor hometotal" NavigateUrl='<%# "ItemQuery.aspx?date=" + curDate + "&showType=y&itemType=sr,zc" %>' /></td>
                </tr>
                <tr>
                    <td class="homelink"><a href="ItemQuery.aspx?date=<%=curDate %>&showType=a">全部 &gt;</a></td>
                    <td><asp:HyperLink ID="ShouRuAllLab" runat="server" CssClass="shoucolor hometotal" NavigateUrl='<%# "ItemQuery.aspx?date=" + curDate + "&showType=a&itemType=sr" %>' /></td>
                    <td><asp:HyperLink ID="ZhiChuAllLab" runat="server" CssClass="zhicolor hometotal" NavigateUrl='<%# "ItemQuery.aspx?date=" + curDate + "&showType=a&itemType=zc" %>' /></td>
                    <td><asp:HyperLink ID="JieCunAllLab" runat="server" CssClass="jiecolor hometotal" NavigateUrl='<%# "ItemQuery.aspx?date=" + curDate + "&showType=a&itemType=sr,zc" %>' /></td>
                </tr>
            </table> 
            <table border="1" class="tablehome" id="jiehuan">
                <tr>
                    <th style="width:115px;" class="shoutitle">借入</th>
                    <th style="width:115px;" class="zhititle">借出</th>
                </tr>
                <tr>
                    <td><asp:HyperLink ID="JieRuAllLab" runat="server" CssClass="shoucolor totalprice" NavigateUrl="ItemQuery.aspx?date=&showType=a&itemType=jr" /></td>
                    <td><asp:HyperLink ID="JieChuAllLab" runat="server" CssClass="zhicolor totalprice" NavigateUrl="ItemQuery.aspx?date=&showType=a&itemType=jc" /></td>
                </tr>
                <tr>
                    <th class="zhititle">还出</th>
                    <th class="shoutitle">还入</th>
                </tr>
                <tr>
                    <td><asp:HyperLink ID="HuanChuAllLab" runat="server" CssClass="zhicolor totalprice" NavigateUrl="ItemQuery.aspx?date=&showType=a&itemType=hc" /></td>
                    <td><asp:HyperLink ID="HuanRuAllLab" runat="server" CssClass="shoucolor totalprice" NavigateUrl="ItemQuery.aspx?date=&showType=a&itemType=hr" /></td>
                </tr>
                <tr>
                    <th class="jietitle">未还</th>
                    <th class="jietitle">欠还</th>
                </tr>
                <tr>
                    <td><asp:HyperLink ID="WeiHuanAllLab" runat="server" CssClass="jiecolor totalprice" NavigateUrl="ItemQuery.aspx?date=&showType=a&itemType=jr,hc" /></td>
                    <td><asp:HyperLink ID="QianHuanAllLab" runat="server" CssClass="jiecolor totalprice" NavigateUrl="ItemQuery.aspx?date=&showType=a&itemType=jc,hr" /></td>
                </tr>
                <tr>
                    <td colspan="2" class="homelink"><a href="ItemGroup.aspx?date=&showType=a&groupType=ItemTypeName&subGroup=ItemName">借还明细&gt;&gt;</a></td>
                </tr>
            </table>
            <table border="1" class="tablehome" id="qianbao" style="margin-right:0;">
                <asp:Repeater ID="CardList" runat="server">
                    <ItemTemplate>
                    <tr>
                        <th style="width:210px;" class="qiantitle"><%# Eval("CardName")%></th>
                    </tr>
                    <tr>
                        <td class="qiancolor qianprice"><a href='ItemQuery.aspx?date=&showType=a&cardId=<%# Eval("CDID") %>'><%# Eval("CardBalance", "{0:0.0##}") %></a></td>
                    </tr>
                    </ItemTemplate>
                </asp:Repeater>            
                <tr>
                    <td class="homelink"><a href="UserCardAdmin.aspx">钱包管理&gt;&gt;</a></td>
                </tr>
            </table>
            <div class="clear"></div>
        </div>
        <div class="r_home_chart clear mbhide">
            <asp:DropDownList ID="ChartTypeDropDown" runat="server"></asp:DropDownList>
            <script type="text/javascript" src="/ofcgwt/swfobject.js"></script>
            <script type="text/javascript">
                swfobject.embedSWF(
                "/ofcgwt/open-flash-chart-SimplifiedChinese.swf", "my_chart", "100%", "241",
                "9.0.0", "/ofcgwt/expressInstall.swf",
                { "data-file": "<%=chartUrl %>" }, { "wmode": "transparent" }
                );
            </script>
            <div id="my_chart"></div>
            <asp:HiddenField ID="hidChartData" runat="server" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="end" Runat="Server">
<script type="text/javascript">
    $(function () {
        //设置当前菜单
        $(".menu_nav li").eq(0).addClass("cur");
        $("#menu_div .system_ul li").eq(0).addClass("cur");
    });
</script>
</asp:Content>


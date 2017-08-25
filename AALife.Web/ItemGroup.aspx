<%@ Page Title="消费统计" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="ItemGroup.aspx.cs" Inherits="ItemGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="js" Runat="Server">
<script type="text/javascript">
    $(function () {

        //图片选择日期
        $("#datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            showOn: "button",
            buttonImage: "theme/images/dot_10.gif",
            buttonImageOnly: true,
            buttonText: "选择日期",
            yearRange: "-10:+10",
            dateFormat: "yy-mm-dd",
            defaultDate: "<%=curDate %>",
            onSelect: function (date, inst) {
                $(this).val("<%=QueryHelper.GetSpinDateVal(curDate, 0, showType) %>");
                location.href = "ItemGroup.aspx?date=" + date + "&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=<%=sort %>&by=<%=by %>";
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
                    $(this).spinner("value", "<%=QueryHelper.GetSpinDate(curDate, 1, showType) %>");
                    location.href = "ItemGroup.aspx?date=<%=QueryHelper.GetSpinDateVal(curDate, 1, showType) %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=<%=sort %>&by=<%=by %>";
                    return false;
                } else {
                    $(this).attr("ref-spin", ui.value);
                    $(this).spinner("value", "<%=QueryHelper.GetSpinDate(curDate, -1, showType) %>");
                    location.href = "ItemGroup.aspx?date=<%=QueryHelper.GetSpinDateVal(curDate, -1, showType) %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=<%=sort %>&by=<%=by %>";
                    return false;
                }
            }
        });

        //显示下拉
        $("#<%=ShowTypeDropDown.ClientID %>").multiselect({
            multiple: false,
            header: false,
            selectedList: 1,
            minWidth: 40,
            height: 146,
            click: function (event, ui) {
                setTimeout(showtypeclick, 0);
            }
        });

        function showtypeclick() {
            location.href = "ItemGroup.aspx?showType=" + $("#<%=ShowTypeDropDown.ClientID %>").val() + "&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=<%=sort %>&by=<%=by %>";
        }

        //分组类别下拉
        $("#<%=GroupTypeDropDown.ClientID %>").multiselect({
            multiple: false,
            header: false,
            selectedList: 1,
            minWidth: 130,
            click: function (event, ui) {
                setTimeout(categorytypeclick, 0);
            },
            checkAll: function () {
                setTimeout(categorytypeclick, 0);
            },
            uncheckAll: function () {
                setTimeout(categorytypeclick, 0);
            }
        });

        function categorytypeclick() {
            location.href = "ItemGroup.aspx?date=<%=curDate %>&showType=<%=showType %>&groupType=" + $("#<%=GroupTypeDropDown.ClientID %>").val() + "&subGroup=<%=subGroup %>&sort=<%=sort %>&by=<%=by %>";
        }

        //子分组类别下拉
        $("#<%=SubGroupDropDown.ClientID %>").multiselect({
            multiple: false,
            header: false,
            selectedList: 1,
            minWidth: 130,
            click: function (event, ui) {
                setTimeout(subgroupclick, 0);
            },
            checkAll: function () {
                setTimeout(subgroupclick, 0);
            },
            uncheckAll: function () {
                setTimeout(subgroupclick, 0);
            }
        });

        function subgroupclick() {
            location.href = "ItemGroup.aspx?date=<%=curDate %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=" + $("#<%=SubGroupDropDown.ClientID %>").val() + "&keywords=<%=keywords %>&sort=<%=sort %>&by=<%=by %>";
        }

        //展开
        $(".expanddown").click(function () {
            if ($("#<%=SubGroupDropDown.ClientID %>").val() == "") {
                alert("请选择子分组。");
                return;
            }

            var query1 = "<%=groupType %>='" + $(this).attr("ref") + "'";
            //alert(query1);

            expandgroup(this, query1, "<%=subGroup %>", "expandtr1", "expandtr2");
        });

        //明细
        $("body").on("click", ".detaildown", function () {
            var query2 = $(this).attr("query") + " and <%=subGroup %>='" + $(this).attr("ref") + "'";
            //alert(query2);

            expandgroup(this, query2, "ItemName", "expandtr2", "expandtr3");
        });

        //消费
        $("body").on("click", ".itemdown", function () {
            var query3 = $(this).attr("query") + " and ItemName='" + $(this).attr("ref") + "'";
            //alert(query3);

            expandgroup(this, query3, "ItemBuyDate", "expandtr3", "expandtr4");
        });

        function expandgroup(obj, query, sub, style1, style2) {
            var tr = $(obj).parent().parent();
            //使用行的class
            style2 = tr.attr("class");
            if (tr.next().find("table").length > 0) {
                $(obj).html() == "展开" ? $(obj).html("收缩") : $(obj).html("展开");
                tr.toggleClass("expandtr");
                //tr.toggleClass(style1);
                tr.next().toggle();
            } else {
                $(obj).html("收缩");
                tr.addClass("expandtr");
                //tr.addClass(style1);

                $.ajax({
                    type: "post",
                    url: "AutoItemGroupJson.aspx?begin=<%=beginDate.ToString("yyyy-MM-dd") %>&end=<%=endDate.ToString("yyyy-MM-dd") %>&query=" + encodeURIComponent(query) + "&sub=" + sub + "&sort=<%=sort %>&by=<%=by %>",
                    dataType: "html",
                    cache: false,
                    beforeSend: function () {

                    }, success: function (response) {
                        tr.after("<tr class=\"" + style2 + "\"><td colspan=\"9\">" + response + "</td></tr>");
                    }, complete: function () {

                    }
                });
            }
        }

    });

    //查找关键字
    function keywordsclick() {
        var keywords = $("#keywords").val();
        if (keywords.trim() == "") {
            alert("关键字不能为空！");
            return;
        }

        location.href = "ItemGroup.aspx?date=<%=curDate %>&showType=a&groupType=ItemName&subGroup=<%=subGroup %>&keywords=" + keywords + "&sort=<%=sort %>&by=<%=by %>";
    }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" Runat="Server">
    <div class="r_title">
        <h1><asp:Label ID="QueryTitle" runat="server" Text="查询标题"></asp:Label><asp:TextBox ID="QueryTitleBox" runat="server" Visible="false" MaxLength="8"></asp:TextBox></h1>
        <ul>
            <li>
                <asp:LinkButton ID="ButtonRename" runat="server" OnClick="ButtonRename_Click"><img src="theme/images/ga.gif" title="重命名" /> 重命名</asp:LinkButton>
                <asp:LinkButton ID="ButtonBack" runat="server" OnClick="ButtonBack_Click" Visible="false"><img src="theme/images/dot_19.gif" title="返回" /> 返回</asp:LinkButton>
            </li>
            <li><asp:LinkButton ID="ButtonSave" runat="server" OnClick="ButtonSave_Click"><img src="theme/images/zip1.gif" title="保存" /> 保存</asp:LinkButton></li>                        
            <li><asp:LinkButton ID="ButtonDelete" runat="server" OnClick="ButtonDelete_Click" OnClientClick="return confirm('确定要删除此查询吗？');"><img src="theme/images/ic_dead.gif" title="删除" /> 删除</asp:LinkButton></li>
        </ul>
    </div>
    <div class="r_query">
        <ul>
            <li><asp:DropDownList ID="ShowTypeDropDown" runat="server"></asp:DropDownList></li>
            <li><input type="text" id="datepicker" style="width:100px;" ref-spin="0" ref-date="<%=QueryHelper.GetSpinDateVal(curDate, 0, showType) %>" value="<%=QueryHelper.GetSpinDate(curDate, 0, showType) %>" readonly="true" /></li>
            <li><asp:DropDownList ID="GroupTypeDropDown" runat="server"></asp:DropDownList></li>
            <li><asp:DropDownList ID="SubGroupDropDown" runat="server"></asp:DropDownList></li>
            <li style="margin-right:0;" class="queryinput"><input type="text" id="keywords" style="width:110px;" value="<%=keywords %>" /> <input type="button" value="" onclick="keywordsclick();" title="查找" /></li>
        </ul>
    </div>
    <div id="r_gridview">
        <table border="0" style="width:100%;" class="tablelist">
            <tr>
                <th style="width:140px;">分组名称</th>
                <th style="width:100px;"><a href="ItemGroup.aspx?date=<%=curDate %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=CountNum&by=<%=this.sortHid.Value %>">数量 <%=QueryHelper.GetSortArrow("CountNum", sort, this.sortHid.Value) %></a></th>
                <th style="width:100px;"><a href="ItemGroup.aspx?date=<%=curDate %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=ShouRuPrice&by=<%=this.sortHid.Value %>">收入 <%=QueryHelper.GetSortArrow("ShouRuPrice", sort, this.sortHid.Value) %></a></th>
                <th style="width:100px;"><a href="ItemGroup.aspx?date=<%=curDate %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=ZhiChuPrice&by=<%=this.sortHid.Value %>">支出 <%=QueryHelper.GetSortArrow("ZhiChuPrice", sort, this.sortHid.Value) %></a></th>
                <th style="width:100px;"><a href="ItemGroup.aspx?date=<%=curDate %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=JieRuPrice&by=<%=this.sortHid.Value %>">借入 <%=QueryHelper.GetSortArrow("JieRuPrice", sort, this.sortHid.Value) %></a></th>
                <th style="width:100px;"><a href="ItemGroup.aspx?date=<%=curDate %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=HuanChuPrice&by=<%=this.sortHid.Value %>">还出 <%=QueryHelper.GetSortArrow("HuanChuPrice", sort, this.sortHid.Value) %></a></th>
                <th style="width:100px;"><a href="ItemGroup.aspx?date=<%=curDate %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=HuanRuPrice&by=<%=this.sortHid.Value %>">还入 <%=QueryHelper.GetSortArrow("HuanRuPrice", sort, this.sortHid.Value) %></a></th>
                <th style="width:100px;"><a href="ItemGroup.aspx?date=<%=curDate %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=JieChuPrice&by=<%=this.sortHid.Value %>">借出 <%=QueryHelper.GetSortArrow("JieChuPrice", sort, this.sortHid.Value) %></a></th>
                <th class="cellleft">操作</th>
            </tr>
        </table>
        <asp:GridView ID="GroupList" runat="server" AutoGenerateColumns="False" CssClass="tablelist"
            BorderWidth="0" Width="100%" BorderStyle="None" AllowSorting="true" ShowHeader="False" OnRowDataBound="GroupList_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="分组名称">
                    <ItemTemplate>
                        <asp:Label ID="MainGroupLab" runat="server" Text='<%# Eval("MyLabel") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="140px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="数量" SortExpression="CountNum">
                    <ItemTemplate>
                        <asp:Label ID="CountNumLab" runat="server" Text='<%# Eval("CountNum") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="countcolor" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="收入" SortExpression="ShouRuPrice">
                    <ItemTemplate>
                        <asp:Label ID="ShouRuLab" runat="server" Text='<%# QueryHelper.GetPriceDot(Eval("ShouRuPrice"), "sr") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="shoucolor" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="支出" SortExpression="ZhiChuPrice">
                    <ItemTemplate>
                        <asp:Label ID="ZhiChuLab" runat="server" Text='<%# QueryHelper.GetPriceDot(Eval("ZhiChuPrice"), "zc") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="zhicolor" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="借入" SortExpression="JieRuPrice">
                    <ItemTemplate>
                        <asp:Label ID="JieRuLab" runat="server" Text='<%# QueryHelper.GetPriceDot(Eval("JieRuPrice"), "jr") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="shoucolor" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="还出" SortExpression="HuanChuPrice">
                    <ItemTemplate>
                        <asp:Label ID="HuanChuLab" runat="server" Text='<%# QueryHelper.GetPriceDot(Eval("HuanChuPrice"), "hc") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="zhicolor" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="还入" SortExpression="HuanRuPrice">
                    <ItemTemplate>
                        <asp:Label ID="HuanRuLab" runat="server" Text='<%# QueryHelper.GetPriceDot(Eval("HuanRuPrice"), "hr") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="shoucolor" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="借出" SortExpression="JieChuPrice">
                    <ItemTemplate>
                        <asp:Label ID="JieChuLab" runat="server" Text='<%# QueryHelper.GetPriceDot(Eval("JieChuPrice"), "jc") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="zhicolor" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate><a href="javascript:void(0);" class="expanddown baselink" ref="<%# Eval("MyLabel") %>">展开</a></ItemTemplate>
                    <ItemStyle CssClass="cellleft" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                没有消费记录。
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:HiddenField ID="sortHid" runat="server" Value="" />
        <asp:HiddenField ID="urlHid" runat="server" Value="" />
        <asp:HiddenField ID="titleHid" runat="server" Value="" />
    </div>
    <div class="r_total clear">
        <table border="0" style="width:100%;" class="tabletotal">
            <tr>
                <th style="width:140px;">合计</th>
                <th style="width:100px;"><asp:Label ID="CountNumTotalLab" runat="server" Text="0" CssClass="countcolor totalprice"></asp:Label></th>
                <th style="width:100px;"><asp:Label ID="ShouTotalLab" runat="server" Text="0" CssClass="shoucolor totalprice"></asp:Label></th>
                <th style="width:100px;"><asp:Label ID="ZhiTotalLab" runat="server" Text="0" CssClass="zhicolor totalprice"></asp:Label></th>
                <th style="width:100px;"><asp:Label ID="JieRuTotalLab" runat="server" Text="0" CssClass="shoucolor totalprice"></asp:Label></th>
                <th style="width:100px;"><asp:Label ID="HuanChuTotalLab" runat="server" Text="0" CssClass="zhicolor totalprice"></asp:Label></th>
                <th style="width:100px;"><asp:Label ID="HuanRuTotalLab" runat="server" Text="0" CssClass="shoucolor totalprice"></asp:Label></th>
                <th style="width:100px;"><asp:Label ID="JieChuTotalLab" runat="server" Text="0" CssClass="zhicolor totalprice"></asp:Label></th>
                <th class="cellleft"></th>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td colspan="2">结存&nbsp;&nbsp;<asp:Label ID="JieCunTotalLab" runat="server" Text="0" CssClass="jiecolor totalprice"></asp:Label></td>
                <td colspan="2">未还&nbsp;&nbsp;<asp:Label ID="WeiHuanTotalLab" runat="server" Text="0" CssClass="jiecolor totalprice"></asp:Label></td>
                <td colspan="2">欠还&nbsp;&nbsp;<asp:Label ID="QianHuanTotalLab" runat="server" Text="0" CssClass="jiecolor totalprice"></asp:Label></td>
                <td></td>
            </tr>
        </table>
        <div class="clear"></div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="end" Runat="Server">
<script type="text/javascript">
    $(function () {
        //设置当前菜单
        $("#menu_div .system_ul li").eq(3).addClass("cur");

        //顶部菜单
        var thisname = "<%=this.titleHid.Value %>";
        cur_menu(thisname, ".query_ul");
    });
</script>
</asp:Content>
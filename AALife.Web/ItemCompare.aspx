<%@ Page Title="消费比较" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="ItemCompare.aspx.cs" Inherits="ItemCompare" %>

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
                location.href = "ItemCompare.aspx?date=" + date + "&date2=<%=curDate2 %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=<%=sort %>&by=<%=by %>";
            },
            beforeShow: function (input, inst) {
                $.datepicker._pos = $.datepicker._findPos(input);
                $.datepicker._pos[0] = $("#m_right .date").offset().left;
                $.datepicker._pos[1] = $("#m_right .date").offset().top + 22;
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
                    location.href = "ItemCompare.aspx?date=<%=QueryHelper.GetSpinDateVal(curDate, 1, showType) %>&date2=<%=curDate2 %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=<%=sort %>&by=<%=by %>";
                    return false;
                } else {
                    $(this).attr("ref-spin", ui.value);
                    $(this).spinner("value", "<%=QueryHelper.GetSpinDate(curDate, -1, showType) %>");
                    location.href = "ItemCompare.aspx?date=<%=QueryHelper.GetSpinDateVal(curDate, -1, showType) %>&date2=<%=curDate2 %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=<%=sort %>&by=<%=by %>";
                    return false;
                }
            }
        });

        //图片选择日期2
        $("#datepicker2").datepicker({
            changeMonth: true,
            changeYear: true,
            showOn: "button",
            buttonImage: "theme/images/dot_10.gif",
            buttonImageOnly: true,
            buttonText: "选择日期",
            yearRange: "-10:+10",
            dateFormat: "yy-mm-dd",
            defaultDate: "<%=curDate2 %>",
            onSelect: function (date, inst) {
                $(this).val("<%=QueryHelper.GetSpinDateVal(curDate2, 0, showType) %>");
                location.href = "ItemCompare.aspx?date=<%=curDate %>&date2=" + date + "&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=<%=sort %>&by=<%=by %>";
            },
            beforeShow: function (input, inst) {
                $.datepicker._pos = $.datepicker._findPos(input);
                $.datepicker._pos[0] = $("#m_right .date2").offset().left;
                $.datepicker._pos[1] = $("#m_right .date2").offset().top + 22;
            }
        });

        //选择日期2
        $("#datepicker2").spinner({
            spin: function (event, ui) {
                var ref = $(this).attr("ref-spin");
                var date = $(this).attr("ref-date");
                //if (date == "") return false;
                if (ui.value > ref) {
                    $(this).attr("ref-spin", ui.value);
                    $(this).spinner("value", "<%=QueryHelper.GetSpinDate(curDate2, 1, showType) %>");
                    location.href = "ItemCompare.aspx?date=<%=curDate %>&date2=<%=QueryHelper.GetSpinDateVal(curDate2, 1, showType) %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=<%=sort %>&by=<%=by %>";
                    return false;
                } else {
                    $(this).attr("ref-spin", ui.value);
                    $(this).spinner("value", "<%=QueryHelper.GetSpinDate(curDate2, -1, showType) %>");
                    location.href = "ItemCompare.aspx?date=<%=curDate %>&date2=<%=QueryHelper.GetSpinDateVal(curDate2, -1, showType) %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=<%=sort %>&by=<%=by %>";
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
            location.href = "ItemCompare.aspx?showType=" + $("#<%=ShowTypeDropDown.ClientID %>").val() + "&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=<%=sort %>&by=<%=by %>";
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
            location.href = "ItemCompare.aspx?date=<%=curDate %>&date2=<%=curDate2 %>&showType=<%=showType %>&groupType=" + $("#<%=GroupTypeDropDown.ClientID %>").val() + "&subGroup=<%=subGroup %>&sort=<%=sort %>&by=<%=by %>";
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
            location.href = "ItemCompare.aspx?date=<%=curDate %>&date2=<%=curDate2 %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=" + $("#<%=SubGroupDropDown.ClientID %>").val() + "&keywords=<%=keywords %>&sort=<%=sort %>&by=<%=by %>";
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
                    url: "AutoItemCompareJson.aspx?begin=<%=beginDate.ToString("yyyy-MM-dd") %>&end=<%=endDate.ToString("yyyy-MM-dd") %>&begin2=<%=beginDate2.ToString("yyyy-MM-dd") %>&end2=<%=endDate2.ToString("yyyy-MM-dd") %>&query=" + encodeURIComponent(query) + "&sub=" + sub + "&sort=<%=sort %>&by=<%=by %>",
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

        location.href = "ItemCompare.aspx?date=<%=curDate %>&date2=<%=curDate2 %>&showType=a&groupType=ItemName&subGroup=<%=subGroup %>&keywords=" + keywords + "&sort=<%=sort %>&by=<%=by %>";
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
            <li class="date2"><input type="text" id="datepicker2" style="width:100px;" ref-spin="0" ref-date="<%=QueryHelper.GetSpinDateVal(curDate2, 0, showType) %>" value="<%=QueryHelper.GetSpinDate(curDate2, 0, showType) %>" readonly="true" /></li>
            <li class="date"><input type="text" id="datepicker" style="width:100px;" ref-spin="0" ref-date="<%=QueryHelper.GetSpinDateVal(curDate, 0, showType) %>" value="<%=QueryHelper.GetSpinDate(curDate, 0, showType) %>" readonly="true" /></li>
            <li><asp:DropDownList ID="GroupTypeDropDown" runat="server"></asp:DropDownList></li>
            <li><asp:DropDownList ID="SubGroupDropDown" runat="server"></asp:DropDownList></li>
            <li style="margin-right:0;" class="queryinput"><input type="text" id="keywords" style="width:110px;" value="<%=keywords %>" /> <input type="button" value="" onclick="keywordsclick();" title="查找" /></li>
        </ul>
    </div>
    <div id="r_gridview">
        <table border="0" style="width:100%;" class="tablelist">
            <tr>
                <th style="width:140px;">分组名称</th>
                <th style="width:116px;"><a href="ItemCompare.aspx?date=<%=curDate %>&date2=<%=curDate2 %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=CountNumPrev&by=<%=this.sortHid.Value %>"><%=GetDateLabel(curDate2, showType) %>数量 <%=QueryHelper.GetSortArrow("CountNumPrev", sort, this.sortHid.Value) %></a></th>
                <th style="width:116px;"><a href="ItemCompare.aspx?date=<%=curDate %>&&date2=<%=curDate2 %>showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=CountNumCur&by=<%=this.sortHid.Value %>"><%=GetDateLabel(curDate, showType) %>数量 <%=QueryHelper.GetSortArrow("CountNumCur", sort, this.sortHid.Value) %></a></th>
                <th style="width:116px;"><a href="ItemCompare.aspx?date=<%=curDate %>&date2=<%=curDate2 %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=ShouRuPricePrev&by=<%=this.sortHid.Value %>"><%=GetDateLabel(curDate2, showType) %>收入 <%=QueryHelper.GetSortArrow("ShouRuPricePrev", sort, this.sortHid.Value) %></a></th>
                <th style="width:116px;"><a href="ItemCompare.aspx?date=<%=curDate %>&date2=<%=curDate2 %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=ShouRuPriceCur&by=<%=this.sortHid.Value %>"><%=GetDateLabel(curDate, showType) %>收入 <%=QueryHelper.GetSortArrow("ShouRuPriceCur", sort, this.sortHid.Value) %></a></th>
                <th style="width:116px;"><a href="ItemCompare.aspx?date=<%=curDate %>&date2=<%=curDate2 %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=ZhiChuPricePrev&by=<%=this.sortHid.Value %>"><%=GetDateLabel(curDate2, showType) %>支出 <%=QueryHelper.GetSortArrow("ZhiChuPricePrev", sort, this.sortHid.Value) %></a></th>
                <th style="width:116px;"><a href="ItemCompare.aspx?date=<%=curDate %>&date2=<%=curDate2 %>&showType=<%=showType %>&groupType=<%=groupType %>&subGroup=<%=subGroup %>&sort=ZhiChuPriceCur&by=<%=this.sortHid.Value %>"><%=GetDateLabel(curDate, showType) %>支出 <%=QueryHelper.GetSortArrow("ZhiChuPriceCur", sort, this.sortHid.Value) %></a></th>
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
                <asp:TemplateField HeaderText="之前数量" SortExpression="CountNumPrev">
                    <ItemTemplate>
                        <asp:Label ID="CountNumPrevLab" runat="server" Text='<%# Eval("CountNumPrev") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="countcolor" Width="116px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="当前数量" SortExpression="CountNumCur">
                    <ItemTemplate>
                        <asp:Label ID="CountNumCurLab" runat="server" Text='<%# Eval("CountNumCur") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="countcolor" Width="116px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="之前收入" SortExpression="ShouRuPricePrev">
                    <ItemTemplate>
                        <asp:Label ID="ShouRuLabPrev" runat="server" Text='<%# QueryHelper.GetPriceDot(Eval("ShouRuPricePrev"), "sr") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="shoucolor" Width="116px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="当前收入" SortExpression="ShouRuPriceCur">
                    <ItemTemplate>
                        <asp:Label ID="ShouRuLabCur" runat="server" Text='<%# QueryHelper.GetPriceDot(Eval("ShouRuPriceCur"), "sr") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="shoucolor" Width="116px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="之前支出" SortExpression="ZhiChuPricePrev">
                    <ItemTemplate>
                        <asp:Label ID="ZhiChuLabPrev" runat="server" Text='<%# QueryHelper.GetPriceDot(Eval("ZhiChuPricePrev"), "zc") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="zhicolor" Width="116px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="当前支出" SortExpression="ZhiChuPriceCur">
                    <ItemTemplate>
                        <asp:Label ID="ZhiChuLabCur" runat="server" Text='<%# QueryHelper.GetPriceDot(Eval("ZhiChuPriceCur"), "zc") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="zhicolor" Width="116px" />
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
                <th style="width:116px;"><asp:Label ID="CountNumPrevTotalLab" runat="server" Text="0" CssClass="countcolor totalprice"></asp:Label></th>
                <th style="width:116px;"><asp:Label ID="CountNumCurTotalLab" runat="server" Text="0" CssClass="countcolor totalprice"></asp:Label></th>
                <th style="width:116px;"><asp:Label ID="ShouPrevTotalLab" runat="server" Text="0" CssClass="shoucolor totalprice"></asp:Label></th>
                <th style="width:116px;"><asp:Label ID="ShouCurTotalLab" runat="server" Text="0" CssClass="shoucolor totalprice"></asp:Label></th>
                <th style="width:116px;"><asp:Label ID="ZhiPrevTotalLab" runat="server" Text="0" CssClass="zhicolor totalprice"></asp:Label></th>
                <th style="width:116px;"><asp:Label ID="ZhiCurTotalLab" runat="server" Text="0" CssClass="zhicolor totalprice"></asp:Label></th>
                <th class="cellleft"></th>
            </tr>
            <tr>
                <td></td>
                <td colspan="2"><asp:Label ID="JieCountNumTotalLab" runat="server" Text="0" CssClass="countcolor totalprice"></asp:Label></td>
                <td colspan="2"><asp:Label ID="JieShouRuTotalLab" runat="server" Text="0" CssClass="shoucolor totalprice"></asp:Label></td>
                <td colspan="2"><asp:Label ID="JieZhiChuTotalLab" runat="server" Text="0" CssClass="zhicolor totalprice"></asp:Label></td>
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
        $("#menu_div .system_ul li").eq(4).addClass("cur");

        //顶部菜单
        var thisname = "<%=this.titleHid.Value %>";
        cur_menu(thisname, ".query_ul");
    });
</script>
</asp:Content>
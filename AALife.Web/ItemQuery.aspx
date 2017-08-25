<%@ Page Title="消费明细" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="ItemQuery.aspx.cs" Inherits="ItemQuery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="js" Runat="Server">
<script type="text/javascript">
    $(function () {

        //编辑定位光标
        $(".tablegrid input[type=text]").eq(0).focus();

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
            defaultDate : "<%=curDate %>",
            onSelect: function (date, inst) {
                $(this).val("<%=QueryHelper.GetSpinDateVal(curDate, 0, showType) %>");
                location.href = "ItemQuery.aspx?date=" + date + "&showType=<%=showType %>&itemType=<%=HttpUtility.UrlEncode(itemType) %>&regionType=<%=HttpUtility.UrlEncode(regionType) %>&catId=<%=HttpUtility.UrlEncode(catId) %>&ztId=<%=HttpUtility.UrlEncode(ztId) %>&cardId=<%=HttpUtility.UrlEncode(cardId) %>&recommend=<%=HttpUtility.UrlEncode(recommend) %>&keywords=<%=HttpUtility.UrlEncode(keywords) %>&sort=<%=sort %>&by=<%=by %>";
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
                    location.href = "ItemQuery.aspx?date=<%=QueryHelper.GetSpinDateVal(curDate, 1, showType) %>&showType=<%=showType %>&itemType=<%=HttpUtility.UrlEncode(itemType) %>&regionType=<%=HttpUtility.UrlEncode(regionType) %>&catId=<%=HttpUtility.UrlEncode(catId) %>&ztId=<%=HttpUtility.UrlEncode(ztId) %>&cardId=<%=HttpUtility.UrlEncode(cardId) %>&recommend=<%=HttpUtility.UrlEncode(recommend) %>&keywords=<%=HttpUtility.UrlEncode(keywords) %>&sort=<%=sort %>&by=<%=by %>";
                    return false;
                } else {
                    $(this).attr("ref-spin", ui.value);
                    $(this).spinner("value", "<%=QueryHelper.GetSpinDate(curDate, -1, showType) %>");
                    location.href = "ItemQuery.aspx?date=<%=QueryHelper.GetSpinDateVal(curDate, -1, showType) %>&showType=<%=showType %>&itemType=<%=HttpUtility.UrlEncode(itemType) %>&regionType=<%=HttpUtility.UrlEncode(regionType) %>&catId=<%=HttpUtility.UrlEncode(catId) %>&ztId=<%=HttpUtility.UrlEncode(ztId) %>&cardId=<%=HttpUtility.UrlEncode(cardId) %>&recommend=<%=HttpUtility.UrlEncode(recommend) %>&keywords=<%=HttpUtility.UrlEncode(keywords) %>&sort=<%=sort %>&by=<%=by %>";
                    return false;
                }
            }
        });

        //编辑选择日期
        $(".itemdatebox").datepicker({
            changeMonth: true,
            changeYear: true
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
            location.href = "ItemQuery.aspx?showType=" + $("#<%=ShowTypeDropDown.ClientID %>").val() + "&itemType=<%=HttpUtility.UrlEncode(itemType) %>&regionType=<%=HttpUtility.UrlEncode(regionType) %>&catId=<%=HttpUtility.UrlEncode(catId) %>&ztId=<%=HttpUtility.UrlEncode(ztId) %>&cardId=<%=HttpUtility.UrlEncode(cardId) %>&recommend=<%=HttpUtility.UrlEncode(recommend) %>&keywords=<%=HttpUtility.UrlEncode(keywords) %>&sort=<%=sort %>&by=<%=by %>";
        }

        //分类下拉
        $("#<%=ItemTypeListBox.ClientID %>").multiselect({
            noneSelectedText: "选择分类",
            selectedText: '# 分类已选',
            checkAllText: "",
            uncheckAllText: "",
            selectedList: 2,
            minWidth: 85,
            height: 106,
            click: function(event, ui) {
                setTimeout(itemtypeclick, 0);
            },
            checkAll: function () {
                setTimeout(itemtypeclick, 0);
            },
            uncheckAll: function () {
                setTimeout(itemtypeclick, 0);
            }
        });

        function itemtypeclick(obj) {
            //alert($("select").val());
            location.href = "ItemQuery.aspx?date=<%=curDate %>&showType=<%=showType %>&itemType=" + encodeURIComponent($("#<%=ItemTypeListBox.ClientID %>").val()) + "&regionType=<%=HttpUtility.UrlEncode(regionType) %>&catId=<%=HttpUtility.UrlEncode(catId) %>&ztId=<%=HttpUtility.UrlEncode(ztId) %>&cardId=<%=HttpUtility.UrlEncode(cardId) %>&recommend=<%=HttpUtility.UrlEncode(recommend) %>&keywords=<%=HttpUtility.UrlEncode(keywords) %>&sort=<%=sort %>&by=<%=by %>";
        }

        //商品类别下拉
        $("#<%=CategoryTypeListBox.ClientID %>").multiselect({
            noneSelectedText: "选择商品类别",
            selectedText: '# 商品类别已选',
            selectedList: 2,
            minWidth: 130,
            click: function (event, ui) {
                setTimeout(categorytypeclick, 0);
            },
            checkAll: function(){
                setTimeout(categorytypeclick, 0);
            },
            uncheckAll: function(){
                setTimeout(categorytypeclick, 0);
            }
        });

        function categorytypeclick() {
            location.href = "ItemQuery.aspx?date=<%=curDate %>&showType=<%=showType %>&itemType=<%=HttpUtility.UrlEncode(itemType) %>&regionType=<%=HttpUtility.UrlEncode(regionType) %>&catId=" + encodeURIComponent($("#<%=CategoryTypeListBox.ClientID %>").val()) + "&ztId=<%=HttpUtility.UrlEncode(ztId) %>&cardId=<%=HttpUtility.UrlEncode(cardId) %>&recommend=<%=HttpUtility.UrlEncode(recommend) %>&keywords=<%=HttpUtility.UrlEncode(keywords) %>&sort=<%=sort %>&by=<%=by %>";
        }

        //区间下拉
        $("#<%=RegionTypeListBox.ClientID %>").multiselect({
            noneSelectedText: "选择区间",
            selectedText: '# 区间已选',
            header: false,
            selectedList: 2,
            minWidth: 85,
            height: 106,
            click: function (event, ui) {
                setTimeout(regiontypeclick, 0);
            }
        });

        function regiontypeclick() {
            location.href = "ItemQuery.aspx?date=<%=curDate %>&showType=<%=showType %>&itemType=<%=HttpUtility.UrlEncode(itemType) %>&regionType=" + encodeURIComponent($("#<%=RegionTypeListBox.ClientID %>").val()) + "&catId=<%=HttpUtility.UrlEncode(catId) %>&ztId=<%=HttpUtility.UrlEncode(ztId) %>&cardId=<%=HttpUtility.UrlEncode(cardId) %>&recommend=<%=HttpUtility.UrlEncode(recommend) %>&keywords=<%=HttpUtility.UrlEncode(keywords) %>&sort=<%=sort %>&by=<%=by %>";
        }

        //专题下拉
        $("#<%=ZhuanTiListBox.ClientID %>").multiselect({
            noneSelectedText: "选择专题",
            selectedText: '# 专题已选',
            header: false,
            selectedList: 1,
            minWidth: 90,
            height: 106,
            click: function (event, ui) {
                setTimeout(zhuanticlick, 0);
            }
        });

        function zhuanticlick() {
            location.href = "ItemQuery.aspx?date=<%=curDate %>&showType=<%=showType %>&itemType=<%=HttpUtility.UrlEncode(itemType) %>&regionType=<%=HttpUtility.UrlEncode(regionType) %>&catId=<%=HttpUtility.UrlEncode(catId) %>&ztId=" + encodeURIComponent($("#<%=ZhuanTiListBox.ClientID %>").val()) + "&cardId=<%=HttpUtility.UrlEncode(cardId) %>&recommend=<%=HttpUtility.UrlEncode(recommend) %>&keywords=<%=HttpUtility.UrlEncode(keywords) %>&sort=<%=sort %>&by=<%=by %>";
        }

        //钱包下拉
        $("#<%=CardListBox.ClientID %>").multiselect({
            noneSelectedText: "选择钱包",
            selectedText: '# 钱包已选',
            checkAllText: "",
            uncheckAllText: "",
            selectedList: 1,
            minWidth: 90,
            height: 106,
            click: function (event, ui) {
                setTimeout(cardclick, 0);
            },
            checkAll: function () {
                setTimeout(cardclick, 0);
            },
            uncheckAll: function () {
                setTimeout(cardclick, 0);
            }
        });

        function cardclick() {
            location.href = "ItemQuery.aspx?date=<%=curDate %>&showType=<%=showType %>&itemType=<%=HttpUtility.UrlEncode(itemType) %>&regionType=<%=HttpUtility.UrlEncode(regionType) %>&catId=<%=HttpUtility.UrlEncode(catId) %>&ztId=<%=HttpUtility.UrlEncode(ztId) %>&cardId=" + encodeURIComponent($("#<%=CardListBox.ClientID %>").val()) + "&recommend=<%=HttpUtility.UrlEncode(recommend) %>&keywords=<%=HttpUtility.UrlEncode(keywords) %>&sort=<%=sort %>&by=<%=by %>";
        }

        //推荐下拉
        $("#<%=RecommendListBox.ClientID %>").multiselect({
            noneSelectedText: "推荐否",
            header: false,
            selectedList: 2,
            minWidth: 60,
            height: 56,
            click: function (event, ui) {
                setTimeout(recommendclick, 0);
            }
        });

        function recommendclick() {
            location.href = "ItemQuery.aspx?date=<%=curDate %>&showType=<%=showType %>&itemType=<%=HttpUtility.UrlEncode(itemType) %>&regionType=<%=HttpUtility.UrlEncode(regionType) %>&catId=<%=HttpUtility.UrlEncode(catId) %>&ztId=<%=HttpUtility.UrlEncode(ztId) %>&cardId=<%=HttpUtility.UrlEncode(cardId) %>&recommend=" + encodeURIComponent($("#<%=RecommendListBox.ClientID %>").val()) + "&keywords=<%=HttpUtility.UrlEncode(keywords) %>&sort=<%=sort %>&by=<%=by %>";
        }

    });

    //查找关键字
    function keywordsclick() {
        var keywords = $("#keywords").val();
        if (keywords.trim() == "") {
            alert("关键字不能为空！");
            return;
        }

        location.href = "ItemQuery.aspx?date=<%=curDate %>&showType=a&itemType=<%=HttpUtility.UrlEncode(itemType) %>&regionType=<%=HttpUtility.UrlEncode(regionType) %>&catId=<%=HttpUtility.UrlEncode(catId) %>&ztId=<%=HttpUtility.UrlEncode(ztId) %>&cardId=<%=HttpUtility.UrlEncode(cardId) %>&recommend=<%=HttpUtility.UrlEncode(recommend) %>&keywords=" + encodeURIComponent(keywords) + "&sort=<%=sort %>&by=<%=by %>";
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
            <li><asp:ListBox ID="ItemTypeListBox" runat="server" SelectionMode="Multiple" Rows="1"></asp:ListBox></li>
            <li><asp:ListBox ID="CategoryTypeListBox" runat="server" SelectionMode="Multiple" Rows="1"></asp:ListBox></li>
            <li><asp:ListBox ID="RegionTypeListBox" runat="server" SelectionMode="Multiple" Rows="1"></asp:ListBox></li>
            <li><asp:ListBox ID="ZhuanTiListBox" runat="server" SelectionMode="Multiple" Rows="1"></asp:ListBox></li>
            <li><asp:ListBox ID="CardListBox" runat="server" SelectionMode="Multiple" Rows="1"></asp:ListBox></li>
            <li><asp:ListBox ID="RecommendListBox" runat="server" SelectionMode="Multiple" Rows="1"></asp:ListBox></li>
            <li style="margin-right:0;" class="queryinput"><input type="text" id="keywords" style="width:76px;" value="<%=keywords %>" /> <input type="button" value="" onclick="keywordsclick();" title="查找" /></li>
        </ul>
    </div>
    <div id="r_gridview">
        <table border="0" style="width:100%;" class="tablelist">
            <tr>
                <th style="width:65px;">选择</th>
                <th style="width:65px;"><a href="ItemQuery.aspx?date=<%=curDate %>&showType=<%=showType %>&itemType=<%=HttpUtility.UrlEncode(itemType) %>&regionType=<%=HttpUtility.UrlEncode(regionType) %>&catId=<%=HttpUtility.UrlEncode(catId) %>&ztId=<%=HttpUtility.UrlEncode(ztId) %>&cardId=<%=cardId %>&recommend=<%=HttpUtility.UrlEncode(recommend) %>&sort=ItemType&by=<%=this.sortHid.Value %>">分类 <%=QueryHelper.GetSortArrow("ItemType", sort, this.sortHid.Value) %></a></th>
                <th style="width:65px;">固定</th>
                <th style="width:120px;" class="cellleft"><a href="ItemQuery.aspx?date=<%=curDate %>&showType=<%=showType %>&itemType=<%=HttpUtility.UrlEncode(itemType) %>&regionType=<%=HttpUtility.UrlEncode(regionType) %>&catId=<%=HttpUtility.UrlEncode(catId) %>&ztId=<%=HttpUtility.UrlEncode(ztId) %>&cardId=<%=HttpUtility.UrlEncode(cardId) %>&recommend=<%=HttpUtility.UrlEncode(recommend) %>&sort=ItemName&by=<%=this.sortHid.Value %>">商品名称 <%=QueryHelper.GetSortArrow("ItemName", sort, this.sortHid.Value) %></a></th>
                <th style="width:90px;"><a href="ItemQuery.aspx?date=<%=curDate %>&showType=<%=showType %>&itemType=<%=HttpUtility.UrlEncode(itemType) %>&regionType=<%=HttpUtility.UrlEncode(regionType) %>&catId=<%=HttpUtility.UrlEncode(catId) %>&ztId=<%=HttpUtility.UrlEncode(ztId) %>&cardId=<%=HttpUtility.UrlEncode(cardId) %>&recommend=<%=HttpUtility.UrlEncode(recommend) %>&sort=CategoryTypeName&by=<%=this.sortHid.Value %>">商品类别 <%=QueryHelper.GetSortArrow("CategoryTypeName", sort, this.sortHid.Value) %></a></th>
                <th style="width:90px;" class="cellright"><a href="ItemQuery.aspx?date=<%=curDate %>&showType=<%=showType %>&itemType=<%=HttpUtility.UrlEncode(itemType) %>&regionType=<%=HttpUtility.UrlEncode(regionType) %>&catId=<%=HttpUtility.UrlEncode(catId) %>&ztId=<%=HttpUtility.UrlEncode(ztId) %>&cardId=<%=HttpUtility.UrlEncode(cardId) %>&recommend=<%=HttpUtility.UrlEncode(recommend) %>&sort=ItemPriceSort&by=<%=this.sortHid.Value %>">价格 <%=QueryHelper.GetSortArrow("ItemPriceSort", sort, this.sortHid.Value) %></a></th>
                <th style="width:90px;"><a href="ItemQuery.aspx?date=<%=curDate %>&showType=<%=showType %>&itemType=<%=HttpUtility.UrlEncode(itemType) %>&regionType=<%=HttpUtility.UrlEncode(regionType) %>&catId=<%=HttpUtility.UrlEncode(catId) %>&ztId=<%=HttpUtility.UrlEncode(ztId) %>&cardId=<%=HttpUtility.UrlEncode(cardId) %>&recommend=<%=HttpUtility.UrlEncode(recommend) %>&sort=ItemBuyDate&by=<%=this.sortHid.Value %>">日期 <%=QueryHelper.GetSortArrow("ItemBuyDate", sort, this.sortHid.Value) %></a></th>
                <th style="width:90px;">专题</th>
                <th style="width:90px;"><a href="ItemQuery.aspx?date=<%=curDate %>&showType=<%=showType %>&itemType=<%=HttpUtility.UrlEncode(itemType) %>&regionType=<%=HttpUtility.UrlEncode(regionType) %>&catId=<%=HttpUtility.UrlEncode(catId) %>&ztId=<%=HttpUtility.UrlEncode(ztId) %>&cardId=<%=HttpUtility.UrlEncode(cardId) %>&recommend=<%=HttpUtility.UrlEncode(recommend) %>&sort=CardName&by=<%=this.sortHid.Value %>">支付钱包 <%=QueryHelper.GetSortArrow("CardName", sort, this.sortHid.Value) %></a></th>
                <th style="width:65px;">推荐</th>
                <th class="cellleft">操作</th>
            </tr>
        </table>
        <asp:GridView ID="ItemGrid" runat="server" AutoGenerateColumns="False" CssClass="tablelist tablegrid"
            BorderWidth="0" Width="100%" BorderStyle="None" onrowcancelingedit="ItemGrid_RowCancelingEdit" 
            onrowdeleting="ItemGrid_RowDeleting" onrowediting="ItemGrid_RowEditing" AllowSorting="true" ShowHeader="false"
            onrowupdating="ItemGrid_RowUpdating" DataKeyNames="ItemID" OnRowDataBound="ItemGrid_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <EditItemTemplate>
                        <asp:CheckBox ID="ItemCheckBox" runat="server" Enabled="false" CssClass="radioinput"></asp:CheckBox>
                        <asp:HiddenField ID="RegionIDHid" runat="server" Value='<%# Eval("RegionID") %>' />
                    </EditItemTemplate> 
                    <ItemTemplate>
                        <asp:CheckBox ID="ItemCheckBox" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="ItemCheckBox_CheckedChanged" CssClass="radioinput"></asp:CheckBox>
                        <asp:HiddenField ID="ItemIDHid" runat="server" Value='<%# Eval("ItemID") %>' />                                
                        <asp:HiddenField ID="ItemAppIDHid" runat="server" Value='<%# Eval("ItemAppID") %>' />
                        <asp:HiddenField ID="RegionIDHid" runat="server" Value='<%# Eval("RegionID") %>' />
                    </ItemTemplate>                           
                    <ItemStyle Width="65px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="分类" SortExpression="ItemType">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ItemTypeDropDown" runat="server"></asp:DropDownList>
                        <asp:HiddenField ID="ItemTypeHid" runat="server" Value='<%# Eval("ItemType") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ItemTypeLab" runat="server" Text='<%# Eval("ItemTypeName") %>' CssClass='<%# QueryHelper.GetColorStr(Eval("ItemType").ToString()) %>'></asp:Label>
                        <asp:HiddenField ID="ItemTypeHid" runat="server" Value='<%# Eval("ItemType") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="65px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="固定" SortExpression="ItemRegionType">
                    <ItemTemplate>
                        <asp:Label ID="RegionTypeLab" runat="server" Text='<%# Eval("RegionTypeFull") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="65px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品名称" SortExpression="ItemName">
                    <EditItemTemplate>
                        <asp:TextBox ID="ItemNameBox" runat="server" Text='<%# Bind("ItemName") %>' MaxLength="20"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ItemNameLab" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="120px" CssClass="cellleft" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品类别" SortExpression="CategoryTypeName">
                    <EditItemTemplate>
                        <asp:DropDownList ID="CatTypeDropDown" runat="server"></asp:DropDownList>
                        <asp:HiddenField ID="CatTypeIDHid" runat="server" Value='<%# Eval("CategoryTypeID") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="CatTypeNameLab" runat="server" Text='<%# Eval("CategoryTypeName").ToString() == "" ? "未知" : Eval("CategoryTypeName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="90px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="价格" SortExpression="ItemPrice">
                    <EditItemTemplate>
                        <asp:TextBox ID="ItemPriceBox" runat="server" Text='<%# Bind("ItemPrice", "{0:0.###}") %>' onkeyup="getprice(this);" MaxLength="10"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ItemPriceLab" runat="server" Text='<%# QueryHelper.GetPriceDot(Eval("ItemPrice"), Eval("ItemType").ToString()) %>' CssClass='<%# QueryHelper.GetColorStr(Eval("ItemType").ToString()) %>'></asp:Label>
                        <asp:HiddenField ID="ItemPriceHid" runat="server" Value='<%# Bind("ItemPrice" )%>' />
                    </ItemTemplate>
                    <ItemStyle Width="90px" CssClass="cellright" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="日期" SortExpression="ItemBuyDate">
                    <EditItemTemplate>
                        <asp:TextBox ID="ItemBuyDateBox" runat="server" CssClass="itemdatebox" Text='<%# Bind("ItemBuyDate", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ItemBuyDateLab" runat="server" Text='<%# Eval("ItemBuyDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="90px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="专题" SortExpression="ZhuanTiID">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ZhuanTiDropDown" runat="server"></asp:DropDownList>
                        <asp:HiddenField ID="ZhuanTiIDHid" runat="server" Value='<%# Eval("ZhuanTiID") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ZhuanTiNameLab" runat="server" Text='<%# Eval("ZhuanTiName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="90px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="支付钱包" SortExpression="CardID">
                    <EditItemTemplate>
                        <asp:DropDownList ID="CardDropDown" runat="server"></asp:DropDownList>
                        <asp:HiddenField ID="CardIDHid" runat="server" Value='<%# Eval("CardID") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="CardNameLab" runat="server" Text='<%# Eval("CardName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="90px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="推荐" SortExpression="Recommend">
                    <EditItemTemplate>
                        <asp:CheckBox ID="RecommendBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("Recommend")) %>' CssClass="radioinput"></asp:CheckBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="RecommendBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("Recommend")) %>' AutoPostBack="true" OnCheckedChanged="RecommendBox_CheckedChanged" CssClass="radioinput"></asp:CheckBox>
                    </ItemTemplate>
                    <ItemStyle Width="65px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CssClass="editlink"
                            CommandName="Update" Text="更新"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CssClass="editlink"
                            CommandName="Cancel" Text="取消"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="baselink"
                            CommandName="Edit" Text="修改"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CssClass="baselink"
                            CommandName="Delete" Text="删除" OnClientClick="return confirm('如果是固定消费将删除整个区间。\n确定要删除吗？');"></asp:LinkButton>
                    </ItemTemplate>
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
        <p>共有收入 <asp:Label ID="ShouCountLab" runat="server" Text="0" CssClass="shoucolor totalprice"></asp:Label> 比，金额 <asp:Label ID="ShouTotalLab" runat="server" Text="0" CssClass="shoucolor totalprice"></asp:Label>；
        支出 <asp:Label ID="ZhiCountLab" runat="server" Text="0" CssClass="zhicolor totalprice"></asp:Label> 比，金额 <asp:Label ID="ZhiTotalLab" runat="server" Text="0" CssClass="zhicolor totalprice"></asp:Label>；
        结存 <asp:Label ID="JieCunTotalLab" runat="server" Text="0" CssClass="jiecolor totalprice"></asp:Label></p>
        <p>共有借入 <asp:Label ID="JieRuCountLab" runat="server" Text="0" CssClass="shoucolor"></asp:Label> 比，金额 <asp:Label ID="JieRuTotalLab" runat="server" Text="0" CssClass="shoucolor"></asp:Label>；
        还出 <asp:Label ID="HuanChuCountLab" runat="server" Text="0" CssClass="zhicolor"></asp:Label> 比，金额 <asp:Label ID="HuanChuTotalLab" runat="server" Text="0" CssClass="zhicolor"></asp:Label>；
        未还 <asp:Label ID="WeiHuanTotalLab" runat="server" Text="0" CssClass="jiecolor totalprice"></asp:Label>；
        共有还入 <asp:Label ID="HuanRuCountLab" runat="server" Text="0" CssClass="shoucolor"></asp:Label> 比，金额 <asp:Label ID="HuanRuTotalLab" runat="server" Text="0" CssClass="shoucolor"></asp:Label>；
        借出 <asp:Label ID="JieChuCountLab" runat="server" Text="0" CssClass="zhicolor"></asp:Label> 比，金额 <asp:Label ID="JieChuTotalLab" runat="server" Text="0" CssClass="zhicolor"></asp:Label>；
        欠还 <asp:Label ID="QianHuanTotalLab" runat="server" Text="0" CssClass="jiecolor totalprice"></asp:Label></p>
        <em><asp:Label ID="MyMoney" runat="server" Text="0"></asp:Label></em>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="end" Runat="Server">
<script type="text/javascript">
    $(function () {
        //设置当前菜单
        $("#menu_div .system_ul li").eq(2).addClass("cur");

        //顶部菜单
        var thisname = "<%=this.titleHid.Value %>";
        cur_menu(thisname, ".query_ul");
    });
</script>
</asp:Content>
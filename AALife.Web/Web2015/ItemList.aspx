<%@ Page Title="每日消费" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true"  CodeFile="ItemList.aspx.cs" Inherits="ItemList_2015" %>

<%@ Register Src="UserControl/DayMenu.ascx" TagName="DayMenu" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    $(function () {
        //$("#<%=ItemNameEmpIns.ClientID %>").focus();

        $("#<%=ItemNameEmpIns.ClientID %>").autocomplete({
            source: "/AutoItemNameJson.aspx",
            minLength: 1,
            select: function (event, ui) {
                if (ui.item.id != 0) {
                    $("#<%=ItemNameEmpIns.ClientID %>").val(ui.item.id);
                }
            }
        });
        
        $("#datechoose").datepicker({
            showOtherMonths: true,
            selectOtherMonths: true,
            defaultDate: "<%=Session["TodayDate"].ToString() %>",
            onSelect: function (date, format) {
                location.href = "ItemList.aspx?date=" + date;
            }
        });

        $(".itemdatebox").datepicker({
            showOtherMonths: true,
            selectOtherMonths: true
        });

        $("#datepicker").click(function () { datechoose($(this)); });

        if ($(".maindiv").height() > 331) {
            $(".maindiv").height(331);
            $(".maindiv").css("overflow-y", "auto");
        }

        fixhead();
        fixheadtop();
        if (/Trident/.test(navigator.userAgent)) {
            var _width = $(".tablelist").width() + 1;
            $(".tablehead").css("width", _width);
        }
    });

    //修改专题
    function zhuantiedit(ztid, itemid) {
        //alert(ztid);
        $("#<%=ZhuanTiDown.ClientID %>").val(ztid);
        $("#<%=ItemIDHid.ClientID %>").val(itemid);
        $("#zhuantiedit").show();
    }
    function zhuanticlose() {
        $("#zhuantiedit").hide();
    }
</script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose"></div>
<div id="zhuantiedit">
    <a href="javascript:zhuanticlose();">×</a>
    <p>修改专题</p>
    <asp:DropDownList ID="ZhuanTiDown" runat="server"></asp:DropDownList>
    <asp:Button ID="SubmitButtom" runat="server" Text="修改" CssClass="btninput" OnClick="SubmitButtom_Click" />
    <asp:HiddenField ID="ItemIDHid" runat="server" Value="0" />
</div>
<div id="content">
    <!--内容开始-->
    <uc1:DayMenu ID="DayMenu1" runat="server" />
    <div class="maindiv">
        <asp:GridView ID="List" runat="server" AutoGenerateColumns="False" CssClass="tablelist"
            BorderWidth="1px" Width="100%" onrowcancelingedit="List_RowCancelingEdit" 
            onrowdeleting="List_RowDeleting" onrowediting="List_RowEditing" 
            onrowupdating="List_RowUpdating" DataKeyNames="ItemID">
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <ItemTemplate>
                        <asp:CheckBox ID="ItemCheckBox" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="ItemCheckBox_CheckedChanged" CssClass="radioinput"></asp:CheckBox>
                        <asp:HiddenField ID="ItemIDHid" runat="server" Value='<%# Eval("ItemID") %>' />
                        <asp:HiddenField ID="ItemAppIDHid" runat="server" Value='<%# Eval("ItemAppID") %>' />
                        <asp:HiddenField ID="RegionIDHid" runat="server" Value='<%# Eval("RegionID") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:CheckBox ID="ItemCheckBox" runat="server" Enabled="false" CssClass="radioinput"></asp:CheckBox>
                        <asp:HiddenField ID="RegionIDHid" runat="server" Value='<%# Eval("RegionID") %>' />
                    </EditItemTemplate>                            
                    <HeaderStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="分类" SortExpression="ItemType">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ItemTypeDropDown" runat="server"></asp:DropDownList>
                        <asp:HiddenField ID="ItemTypeIDHid" runat="server" Value='<%# Eval("ItemType") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ItemTypeLab" runat="server" Text='<%# Bind("ItemTypeName") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品名称" SortExpression="ItemName">
                    <EditItemTemplate>
                        <asp:TextBox ID="ItemNameBox" runat="server" Text='<%# Bind("ItemName") %>' MaxLength="20"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <div class="cellregion"><asp:Label ID="ItemNameLab" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                            <asp:Label ID="Label13" runat="server" CssClass="regionname" Visible='<%# !Eval("RegionType").ToString().Equals("") %>' Text='<%# Bind("RegionTypeName") %>'></asp:Label>
                            <asp:HyperLink ID="Label14" runat="server" CssClass="zhuantiname" Visible='<%# Convert.ToInt32(Eval("ZhuanTiID")) > 0 %>' Text='专' NavigateUrl="UserZhuanTi.aspx" ToolTip="专题列表"></asp:HyperLink>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Width="18%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品类别" SortExpression="CategoryTypeName">
                    <EditItemTemplate>
                        <asp:DropDownList ID="CatTypeDropDown" runat="server"></asp:DropDownList>
                        <asp:HiddenField ID="CatTypeIDHid" runat="server" Value='<%# Bind("CategoryTypeID") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="CatTypeNameLab" runat="server" Text='<%# Eval("CategoryTypeName").ToString() == "" ? "未知" : Eval("CategoryTypeName") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="12%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="价格" SortExpression="ItemPrice">
                    <EditItemTemplate>
                        <asp:TextBox ID="ItemPriceBox" runat="server" Text='<%# Bind("ItemPrice", "{0:0.###}") %>' onkeyup="getprice(this);" MaxLength="10"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        ￥<asp:Label ID="ItemPriceLab" runat="server" Text='<%# Bind("ItemPrice", "{0:0.0##}") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="12%" CssClass="cellprice" />
                    <ItemStyle CssClass="cellprice" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="日期" SortExpression="ItemBuyDate">
                    <EditItemTemplate>
                        <asp:TextBox ID="ItemBuyDateBox" runat="server" CssClass="itemdatebox" Text='<%# Bind("ItemBuyDate", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ItemBuyDateLab" runat="server" Text='<%# Eval("ItemBuyDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="12%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="支付钱包" SortExpression="CardID">
                    <EditItemTemplate>
                        <asp:DropDownList ID="CardDropDown" runat="server"></asp:DropDownList>
                        <asp:HiddenField ID="CardIDHid" runat="server" Value='<%# Eval("CardID") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="CardNameLab" runat="server" Text='<%# Eval("CardName") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="12%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="推荐" SortExpression="Recommend">
                    <EditItemTemplate>
                        <asp:CheckBox ID="RecommendBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("Recommend")) %>' Enabled="false" CssClass="radioinput"></asp:CheckBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="RecommendBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("Recommend")) %>' AutoPostBack="true" OnCheckedChanged="RecommendBox_CheckedChanged" CssClass="radioinput"></asp:CheckBox>
                    </ItemTemplate>
                    <HeaderStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandName="Edit" Text="修改"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Delete" Text="删除" OnClientClick="return confirm('如果是固定消费将删除整个区间。\n确定要删除吗？');"></asp:LinkButton>
                        &nbsp;<a href="javascript:zhuantiedit('<%# Eval("ZhuanTiID") %>', '<%# Eval("ItemID") %>');">专题</a>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                            CommandName="Update" Text="更新"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="取消"></asp:LinkButton>
                    </EditItemTemplate>
                    <HeaderStyle Width="13%" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
                    <tr>
                        <th style="width:7%;">选择</th>
                        <th style="width:7%;">分类</th>
                        <th style="width:18%;">商品名称</th>
                        <th style="width:12%;">商品类别</th>
                        <th style="width:12%;" class="cellprice">价格</th>
                        <th style="width:12%;">日期</th>
                        <th style="width:12%;">支付钱包</th>
                        <th style="width:7%;">推荐</th>
                        <th style="width:13%;">操作</th>
                    </tr>
                    <tr>
                        <td colspan="9">没有消费记录。</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:GridView>
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist tableadd">
            <tr>
                <td style="width:7%;"><asp:CheckBox ID="ItemCheckBoxEmpIns" runat="server" Checked="true" Enabled="false" CssClass="radioinput"></asp:CheckBox></td>
                <td style="width:7%;" class="typeselect"><asp:DropDownList ID="ItemTypeEmpIns" runat="server"></asp:DropDownList></td>
                <td style="width:18%;"><asp:TextBox ID="ItemNameEmpIns" runat="server" MaxLength="20"></asp:TextBox></td>
                <td style="width:12%;" class="typeselect"><asp:DropDownList ID="CatTypeEmpIns" runat="server"></asp:DropDownList></td>
                <td style="width:12%;" class="cellprice"><asp:TextBox ID="ItemPriceEmpIns" runat="server" onkeyup="getprice(this);" MaxLength="10"></asp:TextBox></td>
                <td style="width:12%;"><asp:Label ID="ItemBuyDateEmpIns" runat="server"><%=Session["TodayDate"].ToString() %></asp:Label></td>
                <td style="width:12%;" class="typeselect"><asp:DropDownList ID="CardEmpIns" runat="server"></asp:DropDownList></td>
                <td style="width:7%;"><asp:CheckBox ID="RecommendEmpIns" runat="server" Enabled="false" CssClass="radioinput"></asp:CheckBox></td>
                <td style="width:13%;"><asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">添加</asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">X2</asp:LinkButton></td>
            </tr>
        </table>
    </div>
    <div class="h10"></div>
    <div class="left">
        <div class="title"><img src="/Images/Others/chart_organisation.png" alt="" title="" />&nbsp;&nbsp;收支统计</div>
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
            <tr>
                <th style="width:25%;">收入</th>
                <th style="width:25%;" class="cellprice">￥<asp:Label ID="Label2" runat="server"></asp:Label></th>
                <th style="width:25%;">支出</th>
                <th style="width:25%;" class="cellprice">￥<asp:Label ID="Label4" runat="server"></asp:Label></th>
            </tr>
            <tr>
                <td>月入</td>
                <td class="cellprice">￥<asp:Label ID="Label1" runat="server"></asp:Label></td>
                <td>月出</td>
                <td class="cellprice">￥<asp:Label ID="Label3" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>年入</td>
                <td class="cellprice">￥<asp:Label ID="Label11" runat="server"></asp:Label></td>
                <td>年出</td>
                <td class="cellprice">￥<asp:Label ID="Label12" runat="server"></asp:Label></td>
            </tr>
        </table>    
    </div>
    <div class="right">
        <div class="title"><img src="/Images/Others/coins.png" alt="" title="" />&nbsp;&nbsp;本月借还</div>        
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
            <tr>
                <td style="width:25%;" class="priceblue">借出</td>
                <td style="width:25%;" class="cellprice priceblue">￥<asp:Label ID="Label5" runat="server"></asp:Label></td>
                <td style="width:25%;" class="pricered">借入</td>
                <td style="width:25%;" class="cellprice pricered">￥<asp:Label ID="Label7" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="pricered">还入</td>
                <td class="cellprice pricered">￥<asp:Label ID="Label6" runat="server"></asp:Label></td>
                <td class="priceblue">还出</td>
                <td class="cellprice priceblue">￥<asp:Label ID="Label8" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th class="<%= ItemHelper.JieHuanColor(this.Label9.Text, 1) %>">未还</th>
                <th class="cellprice <%= ItemHelper.JieHuanColor(this.Label9.Text, 1) %>">￥<asp:Label ID="Label9" runat="server"></asp:Label></th>
                <th class="<%= ItemHelper.JieHuanColor(this.Label10.Text, 1) %>">欠还</th>
                <th class="cellprice <%= ItemHelper.JieHuanColor(this.Label10.Text, 1) %>">￥<asp:Label ID="Label10" runat="server"></asp:Label></th>
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
        $("#menu table tr td").eq(0).addClass("on");
    });
</script>
</asp:Content>

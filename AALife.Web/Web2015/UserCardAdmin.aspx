<%@ Page Title="钱包管理" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserCardAdmin.aspx.cs" Inherits="UserCardAdmin" %>

<%@ Register Src="UserControl/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    $(function () {
        fixhead();
    });

    //转账
    function zhuanzhangedit(cdid, cardmoney) {
        //alert(ztid);
        $("#<%=CardIDEditHid.ClientID %>").val(cdid);
        $("#<%=CardMoneyEditHid.ClientID %>").val(cardmoney);
        $("#zhuantiedit").show();
    }
    function zhuanzhangclose() {
        $("#zhuantiedit").hide();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose"></div>
<div id="zhuantiedit">
    <a href="javascript:zhuanzhangclose();">×</a>
    <p>转入</p>
    <asp:DropDownList ID="CardDownEdit" runat="server" Width="80px"></asp:DropDownList>
    <asp:TextBox ID="CardMoneyEdit" runat="server" Width="58px"></asp:TextBox>
    <asp:Button ID="SubmitButtom" runat="server" Text="确定" CssClass="btninput" OnClick="SubmitButtom_Click" />
    <asp:HiddenField ID="CardMoneyEditHid" runat="server" Value="0" />
    <asp:HiddenField ID="CardIDEditHid" runat="server" Value="0" />
</div>
<div id="content">
    <!--内容开始-->    
    <uc6:UserMenu ID="UserMenu1" runat="server" />
    <div class="maindiv userdiv">
        <asp:GridView ID="CardList" runat="server" AutoGenerateColumns="False" CssClass="tablelist"
            Width="100%" BorderWidth="1px" ShowFooter="true"
            onrowcancelingedit="CardList_RowCancelingEdit" 
            onrowupdating="CardList_RowUpdating" onrowdeleting="CardList_RowDeleting" 
            onrowediting="CardList_RowEditing" DataKeyNames="CDID">
            <Columns>
                <asp:TemplateField HeaderText="编号">
                    <ItemTemplate>
                        <%# CardList.Rows.Count + 1%>
                    </ItemTemplate>
                    <FooterTemplate>
                        <strong>总计</strong>
                    </FooterTemplate>
                    <HeaderStyle Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="名称">
                    <ItemTemplate>
                        <asp:Label ID="CardNameLab" runat="server" Text='<%# Eval("CardName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="CardNameBox" runat="server" Text='<%# Bind("CardName") %>' MaxLength="20"></asp:TextBox>
                        <asp:HiddenField ID="CardNameHid" runat="server" Value='<%# Eval("CardName") %>' />
                    </EditItemTemplate>
                    <HeaderStyle Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="余额">
                    <ItemTemplate>
                        ￥<asp:Label ID="CardMoneyLab" runat="server" Text='<%# Eval("CardBalance", "{0:0.0##}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="CardMoneyBox" runat="server" Text='<%# Bind("CardBalance", "{0:0.###}") %>' onkeyup="getprice(this);" MaxLength="10"></asp:TextBox>
                        <asp:HiddenField ID="CardMoneyHid" runat="server" Value='<%# Eval("CardMoney") %>' />
                        <asp:HiddenField ID="MoneyStartHid" runat="server" Value='<%# Eval("MoneyStart") %>' />
                        <asp:HiddenField ID="CardBalanceHid" runat="server" Value='<%# Eval("CardBalance") %>' />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <strong>￥<%=TotalMoney.ToString("0.0##") %></strong>
                    </FooterTemplate>
                    <HeaderStyle Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="转账">
                    <ItemTemplate>
                        <a href="javascript:zhuanzhangedit('<%# Eval("CDID") %>', '<%# Eval("CardMoney") %>');">转出</a>
                        &nbsp;&nbsp;<a href="UserZhuanZhangDetail.aspx">明细</a>
                    </ItemTemplate>
                    <HeaderStyle Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        &nbsp;&nbsp;<asp:LinkButton ID="CatButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="修改"></asp:LinkButton>
                        &nbsp;&nbsp;<asp:LinkButton ID="CatButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="删除" OnClientClick="return confirm('确定要删除吗？');"></asp:LinkButton>
                        &nbsp;&nbsp;<a href="UserCardDetail.aspx?cardId=<%# Eval("CDID") %>">详细</a>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="CatButton1" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                        &nbsp;&nbsp;<asp:LinkButton ID="CatButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                    </EditItemTemplate>
                    <HeaderStyle Width="20%" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist tableadd">
            <tr>
                <td style="width:20%;"></td>
                <td style="width:20%;"><asp:TextBox ID="CardNameEmpIns" runat="server" MaxLength="20"></asp:TextBox></td>
                <td style="width:20%;"><asp:TextBox ID="CardMoneyEmpIns" runat="server" onkeyup="getprice(this);" MaxLength="10"></asp:TextBox></td>
                <td style="width:20%;"></td>
                <td style="width:20%;"><asp:LinkButton ID="Button1" runat="server" Text="添加" onclick="Button1_Click"></asp:LinkButton></td>
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
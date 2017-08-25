<%@ Page Title="钱包管理" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserCardAdmin.aspx.cs" Inherits="UserCardAdmin" %>

<%@ Register Src="UserControl/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="js" Runat="Server">
<script type="text/javascript">
    $(function () {

        //编辑定位光标
        $(".tablegrid input[type=text]").eq(0).focus();

        //钱包转账
        var dialog = $("#dialog").dialog({
            autoOpen: false
        });
        dialog.parent().appendTo($("form:first"));
        
        //转账日期
        $("#<%=CardDateEdit.ClientID %>").datepicker({
            changeMonth: true,
            changeYear: true
        });

    });

    //转账
    function showdialog(cdid, cardmoney) {
        $("#<%=CardIDEditHid.ClientID %>").val(cdid);
        $("#<%=CardMoneyEditHid.ClientID %>").val(cardmoney);
        $("#dialog").dialog("open");
    }

</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" Runat="Server">
<div id="dialog" title="钱包转账">
    <p>转入钱包：<asp:DropDownList ID="CardDownEdit" runat="server"></asp:DropDownList></p>
    <p>转入日期：<asp:TextBox ID="CardDateEdit" runat="server"></asp:TextBox></p>
    <p>转入金额：<asp:TextBox ID="CardMoneyEdit" runat="server" onkeyup="getprice(this);"></asp:TextBox></p>
    <p><asp:Button ID="SubmitButtom" runat="server" Text="确定" CssClass="btninput" OnClick="SubmitButtom_Click" /></p>
    <asp:HiddenField ID="CardMoneyEditHid" runat="server" Value="0" />
    <asp:HiddenField ID="CardIDEditHid" runat="server" Value="0" />
</div>
<div id="r_content">
    <!--内容开始-->    
    <uc6:UserMenu ID="UserMenu1" runat="server" />
    <div id="r_tablemenu">
        <div class="r_usergrid">
            <div class="tips"><img src="theme/images/pinber_01.gif" title="钱包管理" /> 钱包管理：是管理您的钱包、银行卡、支付宝等多种消费账户。</div>
            <table border="0" style="width:100%;" class="tablelist">
                <tr>
                    <th style="width:154px;">编号</th>
                    <th style="width:154px;">钱包名称</th>
                    <th style="width:154px;">余额</th>
                    <th style="width:154px;">转账</th>
                    <th style="width:154px;">首页显示</th>
                    <th>操作</th>
                </tr>
            </table>
            <asp:GridView ID="CardList" runat="server" AutoGenerateColumns="False" CssClass="tablelist tablegrid"
                Width="100%" BorderWidth="1px" BorderStyle="None" ShowFooter="true"
                onrowcancelingedit="CardList_RowCancelingEdit" ShowHeader="false"
                onrowupdating="CardList_RowUpdating" onrowdeleting="CardList_RowDeleting" 
                onrowediting="CardList_RowEditing" DataKeyNames="CDID" OnRowDataBound="CardList_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="编号">
                        <ItemTemplate>
                            <%# CardList.Rows.Count + 1%>
                        </ItemTemplate>
                        <FooterTemplate>
                            <strong>总计</strong>
                        </FooterTemplate>
                        <ItemStyle Width="154px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="钱包名称">
                        <ItemTemplate>
                            <asp:Label ID="CardNameLab" runat="server" Text='<%# Eval("CardName") %>'></asp:Label>
                            <asp:HiddenField ID="CardIDHid" runat="server" Value='<%# Eval("CDID") %>' />  
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="CardNameBox" runat="server" Text='<%# Bind("CardName") %>' MaxLength="20"></asp:TextBox>
                            <asp:HiddenField ID="CardNameHid" runat="server" Value='<%# Eval("CardName") %>' />
                        </EditItemTemplate>
                        <ItemStyle Width="154px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="余额">
                        <ItemTemplate>
                            <asp:Label ID="CardMoneyLab" runat="server" Text='<%# Eval("CardBalance", "{0:0.0##}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="CardMoneyBox" runat="server" Text='<%# Bind("CardBalance", "{0:0.###}") %>' onkeyup="getprice(this);" MaxLength="10"></asp:TextBox>
                            <asp:HiddenField ID="CardMoneyHid" runat="server" Value='<%# Eval("CardMoney") %>' />
                            <asp:HiddenField ID="MoneyStartHid" runat="server" Value='<%# Eval("MoneyStart") %>' />
                            <asp:HiddenField ID="CardBalanceHid" runat="server" Value='<%# Eval("CardBalance") %>' />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <strong><%=TotalMoney.ToString("0.0##") %></strong>
                        </FooterTemplate>
                        <ItemStyle Width="154px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="转账">
                        <ItemTemplate>
                            <a href="javascript:void();" onclick="showdialog('<%# Eval("CDID") %>', '<%# Eval("CardMoney") %>');" class="baselink">转出</a>
                            &nbsp;&nbsp;<a href="UserZhuanZhangDetail.aspx" class="baselink">明细</a>
                        </ItemTemplate>
                        <ItemStyle Width="154px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="首页显示" SortExpression="CardShow">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CardShowBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("CardShow")) %>' CssClass="radioinput"></asp:CheckBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CardShowBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("CardShow")) %>' AutoPostBack="true" OnCheckedChanged="CardShowBox_CheckedChanged" CssClass="radioinput"></asp:CheckBox>
                        </ItemTemplate>
                        <ItemStyle Width="154px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            &nbsp;&nbsp;<asp:LinkButton ID="CatButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="修改" CssClass="baselink"></asp:LinkButton>
                            &nbsp;&nbsp;<asp:LinkButton ID="CatButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="删除" OnClientClick="return confirm('确定要删除吗？');" CssClass="baselink"></asp:LinkButton>
                            &nbsp;&nbsp;<a href="ItemQuery.aspx?date=&showType=m&cardId=<%# Eval("CDID") %>" class="baselink">查看</a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="CatButton1" runat="server" CausesValidation="True" CommandName="Update" Text="更新" CssClass="editlink"></asp:LinkButton>
                            &nbsp;&nbsp;<asp:LinkButton ID="CatButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" CssClass="editlink"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <table border="1" style="width:100%;" class="tablelist tableadd">
                <tr>
                    <th style="width:154px;"><asp:Label ID="CardIdEmpIns" runat="server" /></th>
                    <th style="width:154px;"><asp:TextBox ID="CardNameEmpIns" runat="server" MaxLength="20"></asp:TextBox></th>
                    <th style="width:154px;"><asp:TextBox ID="CardMoneyEmpIns" runat="server" onkeyup="getprice(this);" MaxLength="10"></asp:TextBox></th>
                    <th style="width:154px;"></th>
                    <th style="width:154px;"></th>
                    <th><asp:Button ID="Button1" runat="server" Text="添加" onclick="Button1_Click" CssClass="btninput"></asp:Button></th>
                </tr>
            </table>
        </div>
    </div>
    <!--内容结束-->
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="end" Runat="Server">
<script type="text/javascript">
    $(function () {
        //设置当前菜单
        $(".usermenu .u22").addClass("on");

        //顶部菜单
        var thisname = $(".usermenu .u22").children().text().trim();
        cur_menu(thisname, ".user_ul");
    });
</script>
</asp:Content>
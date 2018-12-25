<%@ Page Title="类别管理" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" Inherits="UserCategoryAdmin" Codebehind="UserCategoryAdmin.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    $(function () {	
        fixhead();
        fixheadtop();
    });
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose"></div>
<div id="content">
    <!--内容开始-->
    <div class="title"><img src="/Images/Others/chart_organisation.png" alt="" title="" />&nbsp;&nbsp;类别管理</div>        
    <table cellspacing="0" border="0" style="width:100%;" class="tabledate">
        <tr>
            <td>类别预警范围计算：先由【预算-预算x(预算率/100)】得出值，再根据预算加减值得出范围。&nbsp;&nbsp;&nbsp;&nbsp;<a href="UserAdmin.aspx#info">预算率设置 >></a></td>
        </tr>
    </table>
    <div class="maindiv">
        <asp:GridView ID="CatTypeList" runat="server" AutoGenerateColumns="False" CssClass="tablelist"
            Width="100%" BorderWidth="1px"
            onrowcancelingedit="CatTypeList_RowCancelingEdit" 
            onrowupdating="CatTypeList_RowUpdating" onrowdeleting="CatTypeList_RowDeleting" 
            onrowediting="CatTypeList_RowEditing" DataKeyNames="CategoryTypeID">
            <Columns>
                <asp:TemplateField HeaderText="编号">
                    <ItemTemplate>
                        <%# CatTypeList.Rows.Count + 1%>
                    </ItemTemplate>
                    <HeaderStyle Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类别名称">
                    <ItemTemplate>
                        <asp:Label ID="CatTypeNameLab" runat="server" Text='<%# Eval("CategoryTypeName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="CatTypeNameBox" runat="server" Text='<%# Bind("CategoryTypeName") %>' MaxLength="20"></asp:TextBox>
                        <asp:HiddenField ID="CatTypeNameHid" runat="server" Value='<%# Eval("CategoryTypeName") %>' />
                    </EditItemTemplate>
                    <HeaderStyle Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="预算">
                    <ItemTemplate>
                        <asp:Label ID="CatTypePriceLab" runat="server" Text='<%# Eval("CategoryTypePrice", "{0:0.###}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="CatTypePriceBox" runat="server" Text='<%# Bind("CategoryTypePrice", "{0:0.###}") %>' onkeyup="getpriceint(this);" MaxLength="10"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="预警范围">
                    <ItemTemplate>
                        <asp:Label ID="CatTypeRateLab" runat="server" Text='<%# Eval("CategoryTypeRate") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="CatButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="修改"></asp:LinkButton>
                        &nbsp;&nbsp;<asp:LinkButton ID="CatButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="删除" OnClientClick="return confirm('确定要删除吗？');"></asp:LinkButton>
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
                <td style="width:20%;"><asp:TextBox ID="CatTypeNameEmpIns" runat="server" MaxLength="20"></asp:TextBox></td>
                <td style="width:20%;"><asp:TextBox ID="CatTypePriceEmpIns" runat="server" onkeyup="getpriceint(this);" MaxLength="10"></asp:TextBox></td>
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
        $(".tableadd tr td").eq(0).html(Number($(".tablelist").eq(0).find("tr:last td").eq(0).html()) + 1);
    });
</script>
</asp:Content>
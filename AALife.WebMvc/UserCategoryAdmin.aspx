<%@ Page Title="类别管理" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" Inherits="AALife.WebMvc.Web2018.UserCategoryAdmin" Codebehind="UserCategoryAdmin.aspx.cs" %>

<%@ Register Src="UserControl/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="js" Runat="Server">
<script type="text/javascript">
    $(function () {

        //编辑定位光标
        $(".tablegrid input[type=text]").eq(0).focus();

    });
</script> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" Runat="Server">
<div id="r_content">
    <!--内容开始-->
    <uc6:UserMenu ID="UserMenu1" runat="server" />
    <div id="r_tablemenu">
        <div class="r_usergrid">
            <p class="tips" style="width:924px;"><img src="theme/images/pinber_01.gif" title="类别管理" /> 类别预警范围计算：先由【预算-预算x(预算率/100)】得出值，再根据预算加减值得出范围。&nbsp;&nbsp;&nbsp;&nbsp;<a href="UserAdmin.aspx#info">预算率设置 >></a></p>
            <table border="0" style="width:924px;" class="tablelist">
                <tr>
                    <th style="width:185px;">编号</th>
                    <th style="width:185px;">类别名称</th>
                    <th style="width:185px;">预算</th>
                    <th style="width:185px;">预警范围</th>
                    <th>操作</th>
                </tr>
            </table>
            <asp:GridView ID="CatTypeList" runat="server" AutoGenerateColumns="False" CssClass="tablelist tablegrid"
                Width="924px" BorderWidth="1px" BorderStyle="None"
                onrowcancelingedit="CatTypeList_RowCancelingEdit" ShowHeader="false"
                onrowupdating="CatTypeList_RowUpdating" onrowdeleting="CatTypeList_RowDeleting" 
                onrowediting="CatTypeList_RowEditing" DataKeyNames="CategoryTypeID" OnRowDataBound="CatTypeList_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="编号">
                        <ItemTemplate>
                            <%# CatTypeList.Rows.Count + 1%>
                        </ItemTemplate>
                        <ItemStyle Width="185px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="类别名称">
                        <ItemTemplate>
                            <asp:Label ID="CatTypeNameLab" runat="server" Text='<%# Eval("CategoryTypeName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="CatTypeNameBox" runat="server" Text='<%# Bind("CategoryTypeName") %>' MaxLength="20"></asp:TextBox>
                            <asp:HiddenField ID="CatTypeNameHid" runat="server" Value='<%# Eval("CategoryTypeName") %>' />
                        </EditItemTemplate>
                        <ItemStyle Width="185px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="预算">
                        <ItemTemplate>
                            <asp:Label ID="CatTypePriceLab" runat="server" Text='<%# Eval("CategoryTypePrice", "{0:0.###}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="CatTypePriceBox" runat="server" Text='<%# Bind("CategoryTypePrice", "{0:0.###}") %>' onkeyup="getpriceint(this);" MaxLength="10"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="185px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="预警范围">
                        <ItemTemplate>
                            <asp:Label ID="CatTypeRateLab" runat="server" Text='<%# Eval("CategoryTypeRate") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="185px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="CatButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="修改" CssClass="baselink"></asp:LinkButton>
                            &nbsp;&nbsp;<asp:LinkButton ID="CatButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="删除" OnClientClick="return confirm('确定要删除吗？');" CssClass="baselink"></asp:LinkButton>
                            &nbsp;&nbsp;<a href="ItemQuery.aspx?date=&showType=m&catId=<%# Eval("CategoryTypeID") %>" class="baselink">查看</a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="CatButton1" runat="server" CausesValidation="True" CommandName="Update" Text="更新" CssClass="editlink"></asp:LinkButton>
                            &nbsp;&nbsp;<asp:LinkButton ID="CatButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" CssClass="editlink"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <table border="0" style="width:924px;" class="tablelist tableadd">
                <tr>
                    <th style="width:185px;"><asp:Label ID="CatIdEmpIns" runat="server" /></th>
                    <th style="width:185px;"><asp:TextBox ID="CatTypeNameEmpIns" runat="server" MaxLength="20"></asp:TextBox></th>
                    <th style="width:185px;"><asp:TextBox ID="CatTypePriceEmpIns" runat="server" onkeyup="getpriceint(this);" MaxLength="10"></asp:TextBox></th>
                    <th style="width:185px;"></th>
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
        $(".usermenu .u15").addClass("on");

        //顶部菜单
        var thisname = $(".usermenu .u15").children().text().trim();
        cur_menu(thisname, ".user_ul");
    });
</script>
</asp:Content>
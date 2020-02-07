<%@ Page Title="用户专题" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserZhuanTi.aspx.cs" Inherits="UserZhuanTi" %>

<%@ Register Src="UserControl/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    $(function () {	
        fixhead();
    });
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose"></div>
<div id="content">
    <!--内容开始-->    
    <uc6:UserMenu ID="UserMenu1" runat="server" />
    <div class="maindiv userdiv">
        <asp:GridView ID="List" runat="server" AutoGenerateColumns="False" CssClass="tablelist"
            Width="100%" BorderWidth="1px"
            onrowcancelingedit="List_RowCancelingEdit" 
            onrowupdating="List_RowUpdating" onrowdeleting="List_RowDeleting"
            onrowediting="List_RowEditing" DataKeyNames="ZTID">
            <Columns>
                <asp:TemplateField HeaderText="专题图片">
                    <ItemTemplate>
                        <asp:Image ID="ZhuanTiImage" runat="server" ImageUrl='<%# "/Images/ZhuanTi/" + Eval("ZhuanTiImage") %>' Width="130px" Height="50px" />
                        <asp:HiddenField ID="ZhuanTiImageHid" runat="server" Value='<%# Eval("ZhuanTiImage") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="ZhuanTiImageHid" runat="server" Value='<%# Eval("ZhuanTiImage") %>' />
                        <asp:FileUpload ID="ZhuanTiImageUpload" runat="server" Size="4" />
                    </EditItemTemplate>
                    <HeaderStyle Width="20%" />
                    <ItemStyle Height="50px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="专题名称">
                    <ItemTemplate>
                        <asp:Label ID="ZhuanTiNameLab" runat="server" Text='<%# Eval("ZhuanTiName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="ZhuanTiNameBox" runat="server" Text='<%# Bind("ZhuanTiName") %>' MaxLength="20"></asp:TextBox>
                        <asp:HiddenField ID="ZhuanTiNameHid" runat="server" Value='<%# Eval("ZhuanTiName") %>' />
                    </EditItemTemplate>
                    <HeaderStyle Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="专题区间">
                    <ItemTemplate>
                        <asp:Label ID="ZhuanTiDateLab" runat="server" Text='<%# Eval("ZhuanTiDate") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="结存金额">
                    <ItemTemplate>
                        ￥<asp:Label ID="ZhuanTiJieCunLab" runat="server" Text='<%# Eval("ZhuanTiJieCun", "{0:0.0##}") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="Button1" runat="server" CausesValidation="False" CommandName="Edit" Text="修改"></asp:LinkButton>
                        &nbsp;&nbsp;<asp:LinkButton ID="Button2" runat="server" CausesValidation="False" CommandName="Delete" Text="删除" OnClientClick="return confirm('确定要删除专题和专题消费吗？');"></asp:LinkButton>
                        &nbsp;&nbsp;<a href="UserZhuanTiShow.aspx?ztid=<%# Eval("ZTID") %>">查看</a>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="Button1" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                        &nbsp;&nbsp;<asp:LinkButton ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                    </EditItemTemplate>
                    <HeaderStyle Width="20%" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
                    <tr>
                        <th style="width:20%;">专题图片</th>
                        <th style="width:20%;">专题名称</th>
                        <th style="width:20%;">专题区间</th>
                        <th style="width:20%;">结存金额</th>
                        <th style="width:20%;">操作</th>
                    </tr>
                    <tr>
                        <td colspan="5">没有专题记录。</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:GridView>
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist tableadd">
            <tr>
                <td style="width:20%;"><asp:FileUpload ID="ZhuanTiImageIns" runat="server" Size="4" /></td>
                <td style="width:20%;"><asp:TextBox ID="ZhuanTiNameIns" runat="server" MaxLength="20"></asp:TextBox></td>
                <td style="width:20%;"></td>
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
        $("#content .tabletitle .u6").addClass("on");
    });
</script>
</asp:Content>
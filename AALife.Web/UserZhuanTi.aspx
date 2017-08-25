<%@ Page Title="用户专题" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserZhuanTi.aspx.cs" Inherits="UserZhuanTi" %>

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
            <p class="tips"><img src="theme/images/pinber_01.gif" title="用户专题" /> 用户专题：用于记录相关专题全部消费，比如小孩专题、学习专题、汽车专题等。</p>
            <table border="0" style="width:100%;" class="tablelist">
                <tr>
                    <th style="width:185px;">专题图片</th>
                    <th style="width:185px;">专题名称</th>
                    <th style="width:185px;">专题区间</th>
                    <th style="width:185px;">结存金额</th>
                    <th>操作</th>
                </tr>
            </table>
            <asp:GridView ID="List" runat="server" AutoGenerateColumns="False" CssClass="tablelist tablegrid"
                Width="100%" BorderWidth="1px" BorderStyle="None"
                onrowcancelingedit="List_RowCancelingEdit" ShowHeader="false"
                onrowupdating="List_RowUpdating" onrowdeleting="List_RowDeleting"
                onrowediting="List_RowEditing" DataKeyNames="ZTID" OnRowDataBound="List_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="专题图片">
                        <ItemTemplate>
                            <asp:Image ID="ZhuanTiImage" runat="server" ImageUrl='<%# "/Images/ZhuanTi/" + Eval("ZhuanTiImage") %>' Width="185px" Height="50px" />
                            <asp:HiddenField ID="ZhuanTiImageHid" runat="server" Value='<%# Eval("ZhuanTiImage") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:HiddenField ID="ZhuanTiImageHid" runat="server" Value='<%# Eval("ZhuanTiImage") %>' />
                            <asp:FileUpload ID="ZhuanTiImageUpload" runat="server" Size="4" />
                        </EditItemTemplate>
                        <ItemStyle Height="50px" Width="185px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="专题名称">
                        <ItemTemplate>
                            <asp:Label ID="ZhuanTiNameLab" runat="server" Text='<%# Eval("ZhuanTiName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="ZhuanTiNameBox" runat="server" Text='<%# Bind("ZhuanTiName") %>' MaxLength="20"></asp:TextBox>
                            <asp:HiddenField ID="ZhuanTiNameHid" runat="server" Value='<%# Eval("ZhuanTiName") %>' />
                        </EditItemTemplate>
                        <ItemStyle Width="185px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="专题区间">
                        <ItemTemplate>
                            <asp:Label ID="ZhuanTiDateLab" runat="server" Text='<%# Eval("ZhuanTiDate") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="185px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="结存金额">
                        <ItemTemplate>
                            <asp:Label ID="ZhuanTiJieCunLab" runat="server" Text='<%# Eval("ZhuanTiJieCun", "{0:0.0##}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="185px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="Button1" runat="server" CausesValidation="False" CommandName="Edit" Text="修改" CssClass="baselink"></asp:LinkButton>
                            &nbsp;&nbsp;<asp:LinkButton ID="Button2" runat="server" CausesValidation="False" CommandName="Delete" Text="删除" OnClientClick="return confirm('确定要删除专题和专题消费吗？');" CssClass="baselink"></asp:LinkButton>
                            &nbsp;&nbsp;<a href="ItemQuery.aspx?date=&showType=m&ztId=<%# Eval("ZTID") %>" class="baselink">查看</a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="Button1" runat="server" CausesValidation="True" CommandName="Update" Text="更新" CssClass="editlink"></asp:LinkButton>
                            &nbsp;&nbsp;<asp:LinkButton ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" CssClass="editlink"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    没有专题记录。
                </EmptyDataTemplate>
            </asp:GridView>
            <table border="1" style="width:100%;" class="tablelist tableadd">
                <tr>
                    <th style="width:185px;"><asp:FileUpload ID="ZhuanTiImageIns" runat="server" Size="4" /></th>
                    <th style="width:185px;"><asp:TextBox ID="ZhuanTiNameIns" runat="server" MaxLength="20"></asp:TextBox></th>
                    <th style="width:185px;"></th>
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
        $(".usermenu .u20").addClass("on");

        //顶部菜单
        var thisname = $(".usermenu .u20").children().text().trim();
        cur_menu(thisname, ".user_ul");
    });
</script>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminUserList.aspx.cs" Inherits="AdminUserList" %>

<%@ Register Src="UserControl/AdminMenu.ascx" TagName="AdminMenu" TagPrefix="uc8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>用户列表|后台管理</title>
<link href="common/style.css" type="text/css" rel="stylesheet" />
<link rel="Shortcut Icon" href="favicon.ico" />
<script type="text/javascript" src="common/jquery.min.js"></script>
<script type="text/javascript" src="common/jquery.lazyload.min.js"></script>
<script type="text/javascript">
    $(function () {
        $(".lazyimg").lazyload();

        $("#KeyBox").focus();
    });
</script>
</head>
<body>
<form id="form1" runat="server">
<div id="Box">
    <uc8:AdminMenu ID="AdminMenu1" runat="server" />
    <script type="text/javascript">
        $("#Box #TopMenu .m2").addClass("cur");
    </script>
    <div id="Main">
        <div id="Left" style="width:100%;">
            <div id="ItemList">
                <div>
                    <asp:Label ID="Label1" runat="server" />&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="&lt; 昨天" CssClass="btninput" OnClick="Button2_Click" UseSubmitBehavior="false" />
                    <asp:Button ID="Button3" runat="server" Text="明天 &gt;" CssClass="btninput" OnClick="Button3_Click" UseSubmitBehavior="false" />
                    <asp:Button ID="Button1" runat="server" Text="查看所有用户列表" CssClass="btninput" OnClick="Button1_Click" UseSubmitBehavior="false" />&nbsp;&nbsp;
                    关键字：<asp:TextBox ID="KeyBox" runat="server" Width="120px"></asp:TextBox>
                    <asp:Button ID="Button4" runat="server" Text="搜索" CssClass="btninput" OnClick="Button4_Click" />
                </div>
                <div class="h10"></div>
                <asp:GridView ID="AdminList" runat="server" AutoGenerateColumns="False" CssClass="usertable"
                    BorderWidth="0px" Width="100%" onrowcancelingedit="List_RowCancelingEdit" 
                    onrowdeleting="List_RowDeleting" onrowupdating="List_RowUpdating" 
                    OnPageIndexChanging="List_PageIndexChanging" 
                    onrowediting="List_RowEditing" DataKeyNames="UserID">
                    <Columns>
                        <asp:TemplateField HeaderText="查看">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "AdminUserDetail.aspx?userId=" + Eval("UserID") %>'>详细</asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="UserID" HeaderText="编号" ReadOnly="True" SortExpression="UserID" >
                            <HeaderStyle Width="5%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="用户名" SortExpression="UserName">
                            <ItemTemplate>
                                <asp:Label ID="UserNameLab" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:HiddenField ID="UserNameHid" runat="server" Value='<%# Eval("UserName") %>' />
                                <asp:TextBox ID="UserNameBox" runat="server" Text='<%# Bind("UserName") %>' Width="90px"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="8%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="密码" SortExpression="UserPassword">
                            <ItemTemplate>
                                <asp:Label ID="UserPassLab" runat="server" Text='<%# Eval("UserPassword") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="UserPassBox" runat="server" Text='<%# Bind("UserPassword") %>' Width="90px"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="8%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="昵称" SortExpression="UserNickName">
                            <ItemTemplate>
                                <asp:Label ID="UserNickLab" runat="server" Text='<%# Eval("UserNickName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="UserNickBox" runat="server" Text='<%# Bind("UserNickName") %>' Width="90px"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="8%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="头像">
                            <ItemTemplate>
                                <asp:Image ID="UserImageCon" runat="server" CssClass="lazyimg" ImageUrl='/Images/Others/loading.gif' data-original='<%# Eval("UserImage").ToString().StartsWith("http") ? Eval("UserImage") : "/Images/Users/" + Eval("UserImage") %>' Width="50px" Height="50px" />
                                <asp:HiddenField ID="UserImageHid" runat="server" Value='<%# Eval("UserImage") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:HiddenField ID="UserImageHid" runat="server" Value='<%# Eval("UserImage") %>' />
                                <asp:FileUpload ID="UserImageUpload" runat="server" Size="4" Width="90px" />
                            </EditItemTemplate>
                            <HeaderStyle Width="8%" />
                            <ItemStyle Height="52px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="邮箱" SortExpression="UserEmail">
                            <ItemTemplate>
                                <asp:Label ID="UserEmailLab" runat="server" Text='<%# Eval("UserEmail") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="UserEmailBox" runat="server" Text='<%# Bind("UserEmail") %>' Width="140px"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="12%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="注册日期">
                            <ItemTemplate>
                                <asp:Label ID="CreateDateLab" runat="server" Text='<%# Eval("CreateDate") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="8%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="修改日期">
                            <ItemTemplate>
                                <asp:Label ID="ModifyDateLab" runat="server" Text='<%# Eval("ModifyDate") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="8%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="样式" SortExpression="UserTheme">
                            <ItemTemplate>
                                <asp:Label ID="UserThemeLab" runat="server" Text='<%# Eval("UserTheme") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="UserThemeBox" runat="server" Text='<%# Bind("UserTheme") %>' Width="50px"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="等级" SortExpression="UserLevel">
                            <ItemTemplate>
                                <asp:Label ID="UserLevelLab" runat="server" Text='<%# Eval("UserLevel") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="UserLevelBox" runat="server" Text='<%# Bind("UserLevel") %>' Width="50px"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="来自" SortExpression="UserFrom">
                            <ItemTemplate>
                                <asp:Label ID="UserFromLab" runat="server" Text='<%# Eval("UserFrom") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="UserFromBox" runat="server" Text='<%# Bind("UserFrom") %>' Width="50px"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="预警率" SortExpression="CategoryRate">
                            <ItemTemplate>
                                <asp:Label ID="CategoryRateLab" runat="server" Text='<%# Eval("CategoryRate", "{0:0.###}") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="CategoryRateBox" runat="server" Text='<%# Bind("CategoryRate", "{0:0.###}") %>' Width="50px"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="钱包" SortExpression="UserMoney">
                            <ItemTemplate>
                                <asp:Label ID="UserMoneyLab" runat="server" Text='<%# Eval("UserMoney", "{0:0.0##}") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="UserMoneyBox" runat="server" Text='<%# Bind("UserMoney", "{0:0.0##}") %>' Width="50px"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="编辑"></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="删除" OnClientClick="return confirm('确定要删除吗？');"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                            </EditItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="bottompager" />
                    <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" PageButtonCount="20" />
                </asp:GridView>
                <asp:HiddenField ID="TypeHid" runat="server" Value="0" />
            </div>
        </div>
        <div class="clear"></div>
    </div>
</div>
</form>
</body>
</html>

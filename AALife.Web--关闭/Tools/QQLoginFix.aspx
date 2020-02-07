<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QQLoginFix.aspx.cs" Inherits="Tools_QQLoginFix" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>AppID</td>
                <td><asp:TextBox ID="AppID" runat="server" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>AppKey</td>
                <td><asp:TextBox ID="AppKey" runat="server" Width="300px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>OpenID</td>
                <td><asp:TextBox ID="OpenIDBox" runat="server" Width="300px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>AccessToken</td>
                <td><asp:TextBox ID="AccessTokenBox" runat="server" Width="300px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>FixImage</td>
                <td><asp:CheckBox ID="FixImageBox" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Button ID="GetQQImageButton" OnClick="GetQQImageButton_Click" runat="server" Text="Start Fix" /></td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">{ sj : 100761541, ce4be793ba404ce2e79a1b5ea0840b7f }, { qz : 100651351, e358f5d6c4c5cd822419911c13a18e73 }</td>
            </tr>
        </table>
        <p><asp:Label ID="ResultLabel" runat="server" ForeColor="Blue"></asp:Label></p>
    </div>
    </form>
</body>
</html>

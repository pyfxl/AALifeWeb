<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MsmqTest.aspx.cs" Inherits="AALifeWeb_MsmqTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button3" runat="server" Text="Create" OnClick="Button3_Click" />
        <asp:Button ID="Button1" runat="server" Text="Delete" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Receive" OnClick="Button2_Click" />
    </div>
    <div>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>

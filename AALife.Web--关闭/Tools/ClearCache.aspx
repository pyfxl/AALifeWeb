<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClearCache.aspx.cs" Inherits="ClearCache" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title></title>
<script type="text/javascript" src="/Web2015/common/jquery.min.js"></script>
<script type="text/javascript" src="/Web2015/common/jquery.cookie.min.js"></script>
<script type="text/javascript">
    $(function () {
        if ($.cookie("message") == "1") {
            $.cookie("message", "undefined");
        }

        setTimeout(window.close, 2000);
    });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        清除完成，2秒后自动关闭。
    </div>
    </form>
</body>
</html>

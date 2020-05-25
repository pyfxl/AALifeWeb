<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginCode.aspx.cs" Inherits="AALife.WebMvc.BookDemo.LoginCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <title></title>
    <script src="/Scripts/jquery-3.4.1.min.js"></script>
    <script src="/Scripts/qrcode.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.4.1.min.js"></script>
    <script src="/signalr/hubs"></script>
</head>
<body>
    <script type="text/javascript">
        $(function () {
            //钉钉二维码
            var qrcode = new QRCode(document.getElementById("qrcode"), {
                width : 150,
                height : 150
            });
            qrcode.makeCode("manager8822");

            //引用自动生成的集线器代理
            var chat = $.connection.serverHub;

            //定义服务器调用的客户端sendMessage来显示新消息
            chat.client.sendMessage = function (name, message)
            {
                //alert(message);
                if (message == "ok") {
                    window.location.href = "/Default.aspx";
                }
            }

            //开始连接服务器
            $.connection.hub.start().done(function () {
                //调用服务器端集线器的Send方法
                //chat.server.sendMsg("1");
            })
        });
    </script>
    <div id="qrcode"></div>
</body>
</html>

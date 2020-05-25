﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Client.aspx.cs" Inherits="AALife.WebMvc.BookDemo.Client" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <title></title>
    <script src="/Scripts/jquery-3.4.1.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.4.1.min.js"></script>
    <script src="/signalr/hubs"></script>
</head>
<body>

    <div class="container">
        <input type="text" id="message" />
        <input type="button" id="sendmessage" value="Send" />
        <input type="hidden" id="displayname" />
        <ul id="messageBox"></ul>
    </div>

    <script>
        $(function () {
            //引用自动生成的集线器代理
            var chat = $.connection.serverHub;
            //定义服务器调用的客户端sendMessage来显示新消息
            chat.client.sendMessage = function (name, message)
            {
                //向页面添加消息
                $("#messageBox").append('<li><strong style="color:green">'+htmlEncode(name)+'</strong>:'+htmlEncode(message)+'</li>');
            }
            //设置焦点到输入框
            $('#message').focus();
            //开始连接服务器
            $.connection.hub.start().done(function () {
                $('#sendmessage').click(function () {
                    //调用服务器端集线器的Send方法
                    chat.server.sendMsg($('#message').val());
                    //清空输入框信息并获取焦点
                    $("#message").val('').focus();
                })
            })
        });
        //为显示的消息进行html编码
        function htmlEncode(value)
        {
            var encodeValue = $('<div/>').text(value).html();
            return encodeValue;
        }
    </script>

</body>
</html>

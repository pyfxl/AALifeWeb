<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="AALife.WebMvc.BookDemo.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <title></title>
    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <script src="/Scripts/jquery-3.4.1.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.4.1.min.js"></script>
    <script src="/signalr/hubs"></script>
</head>
<body>
    <div class="container">
        <div class="row text-center">
            <div class="col-md-12"><h1>Book Manager</h1></div>
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-12">
                <input type="text" id="TextBox1" name="TextBox1" class="form-control" autocomplete="off" />
            </div>
        </div>
        <div class="row">&nbsp;</div>
        <div class="row" style="height: 450px; overflow: auto;">
            <div class="col-md-12" id="notes"></div>
        </div>
        <audio controls="controls" autoplay="autoplay" id="audio" style="display: none;">
            Your browser does not support the audio tag.
        </audio>
        <%--<input type="button" value="Play" onclick="play('语音提示/感谢您的使用.mp3')" />--%>
    </div>
    <script>
        //用户id
        var user = "";
        var client = "";

        $(function () {
            var element = $("#TextBox1");
            element.focus();
            element.blur(function () {
                element.focus();
            });

            //不能自动播放
            //setTimeout(function () { showmsg('欢迎使用图书借阅系统'); }, 1000);

            //引用自动生成的集线器代理
            var chat = $.connection.serverHub;
            
            //定义服务器调用的客户端sendMessage来显示新消息
            chat.client.sendMessage = function (name, message)
            {
                client = name;
                console.log(name);
                $("#TextBox1").val(message);
                send();
            }
            
            //开始连接服务器
            $.connection.hub.start().done(function () {
                //调用服务器端集线器的Send方法
                //chat.server.sendMsg(msg);
            });

            //输入操作
            element.on("keyup", function (e) {
                if (e.keyCode == 13) {
                    send();                    
                }
            });

            //调用方法
            function send() {

                var str = $("#TextBox1").val();

                if (isisbn(str)) {
                    //未登录
                    if (user == "") {
                        showmsg('请扫登录二维码');
                        return;
                    }
                    //检查isbn
                    $.getJSON("/api/v1/BookApi", function (data) {
                        var html = getnote(data["Message"], data["MsgType"]);
                        showhtml('借书成功', html);
                    });
                } else {
                    //检查登录
                    $.getJSON("/api/v1/BookApi?id=" + str, function (data) {
                        if (data["Code"] == "1") {
                            user = str;
                            showmsg('请拿出要借的书扫描图书条码');
                            callclient();
                        } else {
                            showmsg('登录失败');
                        }
                    });
                }
            }

            //获取消息模板
            function getnote(str, type) {
                return '<div class="alert alert-' + type + '" role="alert">' + str + '</div>';
            }

            //清除文本框
            function clear() {
                element.val("");
            }

            //显示提示
            function showmsg(msg) {
                var html = getnote(msg, "info");
                $("#notes").prepend(html);
                playmsg(msg);
            }

            //显示内容
            function showhtml(msg, html) {
                $("#notes").prepend(html);
                playmsg(msg);
            }

            //播放语音
            function playmsg(msg) {
                play('语音提示/' + msg + '.mp3');
                clear();
            }

            //扫码回调
            function callclient() {
                if (client != 2) return;
                //开始连接服务器
                $.connection.hub.start().done(function () {
                    //调用服务器端集线器的Send方法
                    chat.server.sendMsg(2, "ok");
                });
            }

        });

        //判断是否isbn
        function isisbn(str) {
            return /^(\d{13})$/.test(str);
        }

        //播放提示音
        function play(url) {
            var a = document.getElementById('audio');
            a.src = url;
            a.play();
        }
    </script>
</body>
</html>

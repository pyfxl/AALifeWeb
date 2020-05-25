<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Scan.aspx.cs" Inherits="AALife.WebMvc.BookDemo.Scan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <title></title>
    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <script src="/Scripts/jquery-3.4.1.min.js"></script>
    <script src="https://g.alicdn.com/dingding/dingtalk-jsapi/2.10.3/dingtalk.open.js"></script>
    <script src="/Scripts/jquery.signalR-2.4.1.min.js"></script>
    <script src="/signalr/hubs"></script>
</head>
<body>
    <div class="container">
        
        <div class="row">
            <div class="col-md-9"><h3>Scan</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-md-9">
                <button type="button" class="btn btn-lg btn-primary" onclick="scan()">扫码</button>
                <button type="button" class="btn btn-lg btn-primary" onclick="send(0)">失败</button>
                <button type="button" class="btn btn-lg btn-primary" onclick="send('manager8822')">成功</button>
            </div>
        </div>
    </div>
    <script>
        //远程调用
        function send(msg) {
            console.log(msg);

            //引用自动生成的集线器代理
            var chat = $.connection.serverHub;

            //定义服务器调用的客户端sendMessage来显示新消息
            chat.client.sendMessage = function (name, message)
            {
                console.log(name);
                //alert(message);
            }

            //开始连接服务器
            $.connection.hub.start().done(function () {
                //调用服务器端集线器的Send方法
                chat.server.sendMsg(1, msg);
            });
        }

        //扫码 可以用
        function scan() {
            dd.ready(function () {
                dd.biz.util.scan({
                    type: 'all', // type 为 all、qrCode、barCode，默认是all。
                    onSuccess: function (data) {
                        //onSuccess将在扫码成功之后回调
                        /* data结构
                         { 'text': String}
                         */
                        //alert(JSON.stringify(data));
                        //$("#code").val(data["text"]);
                        send(data["text"]);
                    },
                    onFail: function (err) {}
                })
            });
        }

    </script>
</body>
</html>

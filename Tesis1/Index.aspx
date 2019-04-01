﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Tesis1.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Content/web.css" />
</head>
<body>
    <form id="form1" runat="server" onsubmit="return false">
        <div id="container">
            <!-- Stream video via webcam -->
            <div class="video-wrap">
                <video class="videoElement" id="video" playsinline autoplay></video>
            </div>

            <div class="btn-primary">
                <button id="snap" onclick="EnviarImagen()">Capture</button>
            </div>

            <!-- Webcam video snapshot -->
            <canvas id="canvas" width="640" height="480"></canvas>
        </div>
    </form>
    <!--Script references. -->
    <!--Reference the jQuery library. -->
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <!--Reference the SignalR library. -->
    <script src="Scripts/jquery.signalR-2.2.2.min.js"></script>
    <!--Reference the autogenerated SignalR hub script. -->
    <script src="signalr/hubs"></script>
    <!--Add script to update the page and send messages.-->
    <script type="text/javascript" src="/Scripts/webcam.js"></script>
    <script>
        //Timer para capturar frames cada 6 segundos
        window.setInterval(function () {            
            EnviarImagen();
        }, 6000);
    </script>
</body>
</html>

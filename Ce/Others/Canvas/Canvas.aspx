<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Canvas.aspx.cs" Inherits="WebApplication2.Canvas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title> 
    <style type="text/css">
        #mycanvas {
        border:1px solid #b6ff00;
        
        }
    </style>  
</head>
<body>
   <canvas id="mycanvas" width="800" height="800" >
      当前浏览器不支持canvas，请换浏览器访问
   </canvas>
</body>
</html>
<script type="text/ecmascript">
    window.onload=function(){
    var canvas = document.getElementById('mycanvas');
    var context = canvas.getContext('2d');

    context.beginPath();
    context.moveTo(0,0);
    context.lineTo(400, 400);
    context.lineTo(0, 800);
    context.lineTo(0,0);
    context.lineWidth= 0;
    context.closePath();
    context.fillStyle = "rgb(230,22,133)";
    context.fill();

    context.beginPath();
    context.moveTo(0, 0);
    context.lineTo(800, 0);
    context.lineTo(400, 400);
    context.lineTo(0, 0);
    context.lineWidth = 0;
    context.closePath();
    context.fillStyle = "rgb(140,222,33)";
    context.fill();

    context.beginPath();
    context.moveTo(800, 0);
    context.lineTo(800, 400);
    context.lineTo(600, 600);
    context.lineTo(600, 200);
    context.moveTo(800, 0);
    context.lineWidth = 0;
    context.closePath();
    context.fillStyle = "rgb(40,122,133)";
    context.fill();

    context.beginPath();
    context.moveTo(600, 200);
    context.lineTo(600, 600);
    context.lineTo(400, 400);
    context.moveTo(600, 200);
    context.lineWidth = 0;
    context.closePath();
    context.fillStyle = "rgb(210,2,13)";
    context.fill();

    context.beginPath();
    context.moveTo(400, 400);
    context.lineTo(600, 600);
    context.lineTo(400, 800);
    context.lineTo(200, 600);
    context.moveTo(800, 800);
    context.lineWidth = 0;
    context.closePath();
    context.fillStyle = "rgb(10,22,133)";
    context.fill();

    context.beginPath();
    context.moveTo(0, 800);
    context.lineTo(200, 600);
    context.lineTo(400, 800);  
    context.moveTo(0, 800);
    context.lineWidth = 0;
    context.closePath();
    context.fillStyle = "rgb(10,222,53)";
    context.fill();

    context.beginPath();
    context.moveTo(800, 400);
    context.lineTo(800, 800);
    context.lineTo(400, 800);
    context.moveTo(800, 400);
    context.lineWidth = 0;
    context.closePath();
    context.fillStyle = "rgb(70,122,53)";
    context.fill();


    for (var i = 0; i < 10; i++) {
        context.beginPath();
        context.arc(i * 84, 200, 40, 0, 0.2 * i * Math.PI);
        context.fillStyle="rgb(123,243,22)"
        context.closePath();
        context.fill();
    }



    //context.strokeStyle = "#324356";
    //context.stroke();
    }
</script>

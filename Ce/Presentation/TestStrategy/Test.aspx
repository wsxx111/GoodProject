<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="TestStrategy.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/JS/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="data:text/javascript;base64,YWxlcnQoJ+aIkeaYr+iwgScp"></script>
    <style type="text/css">
        .Da {
            background-color: #ff6a00;
            width:expression(document.body.clientWidth>800?"300px":"200px");           
        }

        .Ol {
            list-style: none;
        }

        #Link {
            text-decoration: none;
        }
    </style>
</head>
<body>
    <div class="Da">
        <div>
            <input type="text" name="txtname" value=" " />
            <input type="button" name="btnclick" value="确定" />
            <a id="Link" href="http://www.baidu.com" target="_blank">北京</a>
        </div>
        <div>
            <ul class="Ol">
                <li id="first">1</li>
                <li id="second">2</li>
                <li id="third">3</li>
            </ul>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">
    $(function () {

        alert(document.body.clientWidth > 800);
        //$("#first").removeAttr("id");
        //alert($("#first").attr("id"));
        //alert($("#second").offset().top);     

    });
</script>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestStyle.aspx.cs" Inherits="WebApplication2.TestStyle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>测</title>
    <style type="text/css">
        * {
            margin: 0px;
            padding: 0px;
        }

        .TitDesc {
            position: absolute;
        }


        .container {
            /* 自定义开始 */
            margin: 0px auto;
            /* 自定义结束 */
            position: relative;
            overflow: hidden;
            background-size: 100% 100%;
            background-repeat: no-repeat;
            background-image: url(/Imgs/bj.jpg);
            width: 800px;
            height: 200px;
        }

        /*.elem {*/
        /* 自定义开始 */
        /*-moz-animation: flystyle 10s infinite linear;
            -o-animation: flystyle 10s infinite linear;
            -webkit-animation: flystyle 10s infinite linear;
            animation: flystyle 10s infinite linear;
            top: 0px;
            background-size: 100% 100%;
            background-repeat: no-repeat;
            background-image: url(/Imgs/font.png);
            width: 340px;
            height: 120px;*/
        /*color: red;
            font-weight:bold;
            font-family:'Microsoft YaHei';
            font-size: 27px;*/
        /* 自定义结束 */
        /*position: absolute;*/
        /*}*/
        .elem {
            width: 340px;
            height: 120px;
            background-image: url(/Imgs/font.png);
            position: absolute;
            -webkit-animation: bounceInRight 10s infinite ease both;
            -moz-animation: bounceInRight 10s infinite ease both;
            -o-animation: bounceInRight 10s infinite ease both;
            animation: bounceInRight 10s infinite ease both;
        }

        .elem2 {
            width: 240px;
            height: 80px;
            background-size: 100% 100%;
            background-image: url(/Imgs/font2.png);
            position: absolute;
            -webkit-animation: bounceInRight2 10s infinite ease both;
            -moz-animation: bounceInRight2 10s infinite ease both;
            -o-animation: bounceInRight2 10s infinite ease both;
            animation: bounceInRight2 10s infinite ease both;
        }

        .elem3 {
            width: 140px;
            height: 50px;
            background-size: 100% 100%;
            background-image: url(/Imgs/font3.png);
            position: absolute;
            -webkit-animation: bounceInUp 10s infinite ease both;
            -moz-animation: bounceInUp 10s infinite ease both;
            -o-animation: bounceInUp 10s infinite ease both;
            animation: bounceInUp 10s infinite ease both;
        }

        .elem31 {
            width: 140px;
            height: 50px;
            background-size: 100% 100%;
            background-image: url(/Imgs/font3.png);
            position: absolute;
            -webkit-animation: bounceInEpx 10s infinite ease both;
            -moz-animation: bounceInEpx 10s infinite ease both;
            -o-animation: bounceInEpx 10s infinite ease both;
            animation: bounceInEpx 10s infinite ease both;
        }


        @keyframes bounceInRight {
            0% {
                opacity: 0;
                transform: translateX(2000px);
            }

            10% {
                opacity: 1;
                transform: translateX(100px);
            }

            12% {
                transform: translateX(120px);
            }

            18% {
                transform: scale(1) translateX(120px);
            }

            20% {
                transform: scale(1.4) translateX(120px);
            }

            22% {
                transform: scale(1) translateX(120px);
            }

            26% {
                transform: scale(1) translateX(120px);
            }

            28% {
                transform: scale(1.4) translateX(120px);
            }

            32% {
                transform: scale(1) translateX(120px);
            }

            100% {
                transform: scale(1) translateX(120px);
            }
        }



        @keyframes bounceInRight2 {
            0% {
                opacity: 0;
                -moz-transform: translate(2000px,0px);
                -ms-transform: translate(2000px,0px);
                -o-transform: translate(2000px,0px);
                -webkit-transform: translate(2000px,0px);
                transform: translate(2000px,0px);
            }

            14% {
                opacity: 1;
                -moz-transform: translate(540px,0px);
                -ms-transform: translate(540px,0px);
                -o-transform: translate(540px,0px);
                -webkit-transform: translate(540px,0px);
                transform: translate(540px,0px);
            }

            21% {
                -moz-transform: translate(560px,0px);
                -ms-transform: translate(560px,0px);
                -o-transform: translate(560px,0px);
                -webkit-transform: translate(560px,0px);
                transform: translate(560px,0px);
            }

            22% {
                -moz-transform: translate(560px,0px) scale(1);
                -o-transform: translate(560px,0px) scale(1);
                -ms-transform: translate(560px,0px) scale(1);
                -webkit-transform: translate(560px,0px) scale(1);
                transform: translate(560px,0px) scale(1);
            }

            23% {
                -moz-transform: translate(560px,0px) scale(1.5);
                -o-transform: translate(560px,0px) scale(1.5);
                -ms-transform: translate(560px,0px) scale(1.5);
                -webkit-transform: translate(560px,0px) scale(1.5);
                transform: translate(560px,0px) scale(1.5);
            }

            24% {
                -moz-transform: translate(560px,0px) scale(1);
                -ms-transform: translate(560px,0px) scale(1);
                -o-transform: translate(560px,0px) scale(1);
                -webkit-transform: translate(560px,0px) scale(1);
                transform: translate(560px,0px) scale(1);
            }

            32% {
                -moz-transform: translate(560px,0px) scale(1);
                -ms-transform: translate(560px,0px) scale(1);
                -o-transform: translate(560px,0px) scale(1);
                -webkit-transform: translate(560px,0px) scale(1);
                transform: translate(560px,0px) scale(1);
            }

            33% {
                -moz-transform: translate(560px,0px) scale(1.4);
                -ms-transform: translate(560px,0px) scale(1.4);
                -o-transform: translate(560px,0px) scale(1.4);
                -webkit-transform: translate(560px,0px) scale(1.4);
                transform: translate(560px,0px) scale(1.4);
            }

            34% {
                -moz-transform: translate(560px,0px) scale(1);
                -ms-transform: translate(560px,0px) scale(1);
                -o-transform: translate(560px,0px) scale(1);
                -webkit-transform: translate(560px,0px) scale(1);
                transform: translate(560px,0px) scale(1);
            }

            100% {
                -moz-transform: translate(560px);
                -ms-transform: translate(560px);
                -o-transform: translate(560px);
                -webkit-transform: translate(560px);
                transform: translate(560px);
            }
        }

        @keyframes bounceInUp {
            0% {
                opacity: 0;
                -moz-transform: translate(400px,70px);
                -ms-transform: translate(400px,70px);
                -o-transform: translate(400px,70px);
                -webkit-transform: translate(400px,70px);
                transform: translate(400px,120px);
            }

            50% {
                opacity: 0;
                -moz-transform: translate(400px,120px);
                -ms-transform: translate(400px,120px);
                -o-transform: translate(400px,120px);
                -webkit-transform: translate(400px,120px);
                transform: translate(400px,120px);
            }

            52% {
                opacity: 1;
                -moz-transform: translate(400px,80px);
                -ms-transform: translate(400px,80px);
                -o-transform: translate(400px,80px);
                -webkit-transform: translate(400px,80px);
                transform: translate(400px,80px);
            }

            54% {
                -moz-transform: translate(400px,98px);
                -ms-transform: translate(400px,98px);
                -o-transform: translate(400px,98px);
                -webkit-transform: translate(400px,98px);
                transform: translate(400px,98px);
            }

            100% {
                -moz-transform: translate(400px,98px);
                -ms-transform: translate(400px,98px);
                -o-transform: translate(400px,98px);
                -webkit-transform: translate(560px);
                transform: translate(400px,98px);
            }
        }

        @keyframes bounceInEpx {
            0% {
                opacity: 0;
                -moz-transform: translate(400px,70px) scale(1);
                -ms-transform: translate(400px,70px) scale(1);
                -o-transform: translate(400px,70px) scale(1);
                -webkit-transform: translate(400px,70px) scale(1);
                transform: translate(400px,98px) scale(1);
                transform-origin:top;
            }

            60% {
                opacity: 0;
                -moz-transform: translate(400px,98px) scale(1);
                -ms-transform: translate(400px,98px) scale(1); 
                -o-transform: translate(400px,98px) scale(1);
                -webkit-transform: translate(400px,98px) scale(1);
                transform: translate(400px,98px) scale(1);
                transform-origin:top;
            }

            61% {
                opacity: 1;
                -moz-transform: translate(400px,70px) scale(1.5);
                -ms-transform: translate(400px,70px) scale(1.5);                            
                -o-transform:translate(400px,70px) scale(1.5);
                -webkit-transform:translate(400px,70px) scale(1.5);
                transform:translate(400px,70px) scale(1.5);
                transform-origin:top;
            }

            62% {
                opacity: 0;
               -moz-transform: translate(400px,70px) scale(1.5);
                -ms-transform: translate(400px,70px) scale(1.5);                            
                -o-transform:translate(400px,70px) scale(1.5);
                -webkit-transform:translate(400px,70px) scale(1.5);
                transform:translate(400px,70px) scale(1.5);
                transform-origin:top;
            }

            63% {
                opacity: 0;
               -moz-transform: translate(400px,70px) scale(1);
                -ms-transform: translate(400px,70px) scale(1);                            
                -o-transform:translate(400px,70px) scale(1);
                -webkit-transform:translate(400px,70px) scale(1);
                transform:translate(400px,70px) scale(1);
                transform-origin:top;
            }

            63% {
                opacity: 1;
                -moz-transform: translate(400px,70px) scale(1.5);
                -ms-transform: translate(400px,70px) scale(1.5);                            
                -o-transform:translate(400px,70px) scale(1.5);
                -webkit-transform:translate(400px,70px) scale(1.5);
                transform:translate(400px,70px) scale(1.5);
                transform-origin:top;
            }

            63% {
                opacity: 0;
                 -moz-transform: translate(400px,70px) scale(1);
                -ms-transform: translate(400px,70px) scale(1);                            
                -o-transform:translate(400px,70px) scale(1.5);
                -webkit-transform:translate(400px,70px) scale(1.5);
                transform:translate(400px,70px) scale(1.5);
                transform-origin:top;
            }

            100% {
                opacity: 0;
                -moz-transform: translate(400px,70px) scale(1);
                -ms-transform: translate(400px,70px) scale(1);
                -o-transform: translate(400px,70px) scale(1);
                -webkit-transform: translate(400px,70px) scale(1);
                transform: translate(400px,98px) scale(1);
                transform-origin:top;
            }
        }



        #Sing {
            width: 150px;
            height: auto;
            -webkit-transition: .2s ease all;
            -moz-transition: .2s ease all;
            -o-transition: .2s ease all;
            transition: .2s ease all;
        }

            #Sing:hover {
                -moz-transform: scale(1.2) translate(20px,0px);
                -ms-transform: scale(1.2) translate(20px,0px);
                -o-transform: scale(1.2) translate(20px,0px);
                -webkit-transform: scale(1.2) translate(20px,0px);
                transform: scale(1.2) translate(20px,0px);
            }
    </style>
</head>
<body>

    <div class="TitDesc">右侧弹入：</div>
    <div class="container">
        <div class="elem">
        </div>
        <div class="elem2">
        </div>
        <div class="elem3">
        </div>
        <div class="elem31">
        </div>
    </div>
    <div>
        <img id="Sing" src="Imgs/font.png" alt="" />
    </div>
</body>
</html>

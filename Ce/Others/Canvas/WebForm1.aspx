<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>学习</title>
    <%--<link href="Css/Site.css" rel="stylesheet" />--%>
    <style type="text/css">
        .Par {
        position:relative;   
        width:370px;
        height:130px;
        background-image:url(/Imgs/1444466037343.png);  
        overflow:hidden;
        }
        /*.Pot {
            width: 36px;
            height: 36px;
            border-radius: 18px;         
            animation:myfirst 5s infinite linear;          
            position:absolute;           
        }*/

         .Pot {           
            font-size: 18px;                   
            animation:myfirst 5s infinite linear;          
            position:absolute;
            display:inline;           
        }
 

@keyframes myfirst {
            0% {
                color: green;
                left:0px;
                top:0px;               
            }  
            10% {
                color: green;
                left:0px;
                top:0px;
                transform:scale(1.2);
            }            
            15% {
                color:black; 
                left:80px; 
                top:0px;
                transform:scale(1.2);                      
            }  
            85% {
                color:black; 
                left:80px; 
                top:0px;
                transform:scale(1.2);                      
            }
            90% {
                color:green;    
                left:0px; 
                top:0px;   
                transform:scale(1);           
            }               
                  100% {
                color:green;    
                left:0px; 
                top:0px;
                transform:scale(1);              
            }                      
        }


    </style>
</head>
<body>      
    <div class="Par">       
    <div class="Pot">
        北京欢迎您
    </div>
</div>
</body>
</html>

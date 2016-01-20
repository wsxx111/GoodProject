<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xin.aspx.cs" Inherits="WebApplication2.Xin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .container {
            /* 自定义开始 */         
            /* 自定义结束 */
            position: relative;
            overflow: hidden;
            border:1px solid #808080;
            background-color:#bebebe;
            background-size: 100% 100%;
            background-repeat: no-repeat;                     
        }
        .Close {
        text-align:right;
        font-size:12px;
        color:red;
        margin-bottom:5px;       
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="新建" OnClick="Button1_Click" />
        <asp:Button ID="Button3" runat="server"  Visible="false" Text="设置总时间" OnClick="Button3_Click" />
        <asp:Button ID="Button6" runat="server"  Visible="false" Text="添加元素" OnClick="Button6_Click" /> 
               
        <asp:Button ID="Button9" runat="server"  Text="生成css" OnClick="Button9_Click" />
    </div>
       
        <asp:Panel ID="Inp" Visible="false"  runat="server" BorderColor="#999966" Height="37px" Width="334px">
            <%--<div class="Close">关闭</div>--%>
            长度：<asp:TextBox ID="Wid" runat="server" Text="450" Width="55px"></asp:TextBox>px
             宽度：<asp:TextBox ID="Hei" runat="server" Text="300" Width="55px"></asp:TextBox>px
            <asp:Button ID="Button2" runat="server" Text="确定" OnClick="Button2_Click" />
        </asp:Panel>
        <br />

        <asp:Panel ID="TimeTotal" Visible="false" runat="server">
            总时间：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>&nbsp;
            <asp:Button ID="Button5" runat="server" Text="确定" OnClick="Button5_Click" />
        </asp:Panel>

        <asp:Panel ID="Panel2" Visible="false" runat="server" Width="476px">
         设置背景图片：<asp:FileUpload ID="FileUpload1"  runat="server" Width="66px" />
            &nbsp;
            <asp:Button ID="Button4" runat="server" Text="确定" OnClick="Button4_Click" />           
            </asp:Panel>
        <br />

         <asp:Panel ID="Panel3" Visible="false" runat="server" Width="476px">
             元素名称：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
             <br />
             添加：<asp:FileUpload ID="FileUpload2"  runat="server" Width="71px" />
            &nbsp;
            <asp:Button ID="Button7" runat="server" Text="确定" OnClick="Button7_Click" />
             <br />
             选择元素：<asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>    
             &nbsp;<asp:Button ID="Button8" runat="server" Text="删除元素" />
            </asp:Panel>
        <br />
        <asp:Panel ID="Panel1" CssClass="container" Visible="false" runat="server">
        </asp:Panel>
        <asp:Panel ID="Panel4" Visible="false" runat="server">
            <asp:TextBox ID="TextBox3" TextMode="MultiLine" runat="server" Height="228px" Width="456px"></asp:TextBox>                      
        </asp:Panel>           
    </form>
</body>
</html>

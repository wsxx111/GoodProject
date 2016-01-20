<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Redis.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="LabelTxt" runat="server"  Text="测试前请务必打开redis-server.exe" BackColor="White" ForeColor="Red"></asp:Label>            
            <br />
        </div>
        <div style="height:40px;">
            <asp:Button ID="Button1" runat="server" Text="Set开始" OnClick="Button1_Click" Height="33px" Width="94px" />
            <asp:TextBox ID="ResumeTime" runat="server" Height="22px" Width="260px"></asp:TextBox>
        </div>
        <div style="height:40px;">
            <asp:Button ID="Button2" runat="server" Text="Store开始" Height="33px" Width="94px" OnClick="Button2_Click" />
            <asp:TextBox ID="ResumeTime2" runat="server" Height="22px" Width="260px"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="Result" runat="server" Text="Label" ForeColor="#FF0066"></asp:Label>            
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="webconfig.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="AppSetting" runat="server" Text="AppSetting:"></asp:Label><asp:TextBox ID="AppSetValue" runat="server" Width="692px"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="ConnectionString" runat="server" Text="ConnectionString:"></asp:Label><asp:TextBox ID="ConstrValue" runat="server" Width="647px"></asp:TextBox>
        </div>
         <div>
            <asp:Label ID="SectionPeople" runat="server" Text="SectionPeople:"></asp:Label><asp:TextBox ID="SectionPeopleValue" runat="server" Width="647px"></asp:TextBox>
        </div>
         <div>
            <asp:Label ID="SectionHuman" runat="server" Text="SectionHuman:"></asp:Label><asp:TextBox ID="SectionHumanValue" runat="server" Width="647px"></asp:TextBox>
        </div>
         <div>
            <asp:Label ID="SectionPerson" runat="server" Text="SectionPerson:"></asp:Label><asp:TextBox ID="SectionPersonValue" runat="server" Width="647px"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="SectionMan" runat="server" Text="SectionMan:"></asp:Label><asp:TextBox ID="SectionManValue" runat="server" Width="647px"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="SectionRen" runat="server" Text="SectionRen:"></asp:Label><asp:TextBox ID="SectionRenValue" runat="server" Width="647px"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="SectionCurture" runat="server" Text="SectionCurture:"></asp:Label><asp:TextBox ID="SectionCurtureValue" runat="server" Width="647px"></asp:TextBox>
        </div>
    </form>
</body>
</html>

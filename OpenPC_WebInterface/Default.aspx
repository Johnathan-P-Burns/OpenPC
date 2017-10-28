<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OpenPC_WebInterface._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>The OpenPC Project</title>
    <link rel="stylesheet" href="styles/styles.css" type="text/css" media="screen" charset="utf-8" />
</head>
<html>
<body>
    <form id="TestForms" runat="server">
        <asp:DropDownList ID="BuildingList" runat="server">
            <asp:ListItem Enabled="true" Text="Select Location" Value="-1"></asp:ListItem>
            <asp:ListItem Text="Any" Value="0"></asp:ListItem>
            <asp:ListItem Text="Min Kao" Value="1"></asp:ListItem>
            <asp:ListItem Text="Hodges" Value="2"></asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:DropDownList ID="OSList" runat="server">
            <asp:ListItem Enabled="true" Text="Select OS" Value="-1"></asp:ListItem>
            <asp:ListItem Text="Any" Value="0"></asp:ListItem>
            <asp:ListItem Text="Windows" Value="1"></asp:ListItem>
            <asp:ListItem Text="MacOS" Value="2"></asp:ListItem>
            <asp:ListItem Text="Linux" Value="3"></asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:DropDownList ID="ProcessorList" runat="server">
            <asp:ListItem Enabled="true" Text="Select Processor" Value="-1"></asp:ListItem>
            <asp:ListItem Text="Any" Value="0"></asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Label ID="ApplicationsLabel" runat="server" Text="Applications"></asp:Label>
        <asp:CheckBoxList ID="Applications" runat="server">
            <asp:ListItem Text="Photoshop" Value="0"></asp:ListItem>
        </asp:CheckBoxList>
        <br />
        <asp:CheckBox ID="Printing" runat="server" Text="Printing Capability" />
        <br />

        <asp:Button ID="SubmitQuery" runat="server" Text="Search for open PC" OnClick="SubmitQuery_Click"/>
    </form>

    <center>
        <img align="middle" width="640" height="640" id="GoogleMapsFrame" runat="server" visible="false" src="test"></img>
    </center>
</body>
</html>

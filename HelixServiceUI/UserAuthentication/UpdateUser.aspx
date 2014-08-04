<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateUser.aspx.cs" Inherits="HelixServiceUI.UserAuthentication.updateuser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Repeater ID="rUsers" runat="server">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <AlternatingItemTemplate>
            </AlternatingItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
    </form>
</body>
</html>

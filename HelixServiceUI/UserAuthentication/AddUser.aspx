<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="HelixServiceUI.UserAuthentication.AddUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Authentication - Add User</title>
    <style>
        #divStatus {
        }

        #divAddUser {
        }

            #divAddUser div span {
                display: inline-block;
                width: 130px;
                text-align: right;
                padding: 10px 0 10px 0;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divStatus">
            <asp:Label ID="lblStatus" Text="" runat="server" Visible="false"></asp:Label>
        </div>
        <div id="divAddUser">
            <div>
                <asp:Label ID="lblUsername" Text="Username:" runat="server" />
                <asp:TextBox ID="txtUsername" runat="server" Width="150" />
            </div>
            <div>
                <asp:Label ID="lblPassword" Text="Password:" runat="server" />
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150" />
            </div>
            <div>
                <asp:Label ID="lblConfirmPassword" Text="Confirm Password:" runat="server" />
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Width="150" />
            </div>
            <div>
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClick="btnCancel_Click" />
                <asp:Button ID="btnAdd" Text="Add User" runat="server" OnClick="btnAdd_Click" />
            </div>
        </div>
    </form>
</body>
</html>

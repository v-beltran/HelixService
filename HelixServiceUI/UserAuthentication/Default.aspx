<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HelixServiceUI.UserAuthentication.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User Authentication - Login</title>
    <style>
        #divStatus {
        }

        #divLogin {
        }

            #divLogin div span {
                display: inline-block;
                width: 75px;
                text-align: right;
                padding: 10px 0 10px 0;
            }

        #divLoggedIn {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divLoginForm">
            <div class="divStatus">
                <asp:Label ID="lblStatus" Text="" runat="server" Visible="false"></asp:Label>
            </div>
            <div id="divLogin" runat="server" clientidmode="static">
                <div>
                    <asp:Label ID="lblUsername" Text="Username:" runat="server" />
                    <asp:TextBox ID="txtUsername" runat="server" Width="150" />
                </div>
                <div>
                    <asp:Label ID="lblPassword" Text="Password:" runat="server" />
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150" />
                </div>
                <div>
                    <asp:Button ID="btnLogin" Text="Login" runat="server" OnClick="btnLogin_Click" />
                </div>
            </div>
            <div id="divLoggedIn" runat="server" visible="false" clientidmode="static">
                <div>
                    <asp:Button ID="btnLogout" Text="Logout" runat="server" OnClick="btnLogout_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>

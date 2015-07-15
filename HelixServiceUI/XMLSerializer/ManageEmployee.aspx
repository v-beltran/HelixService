<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageEmployee.aspx.cs" Inherits="HelixServiceUI.XMLSerializer.ManageEmployee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= this.PageTitle %></title>
    <style>
        .content-wrapper {
            width: 980px;
        }

        .divStatus div div {
            display: block;
            width: 100%;
            padding: 10px;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        .divStatus .success div {
            background-color: #08b94d;
            color: white;
        }

        .divStatus .error div {
            background-color: #cc3c3c;
            color: white;
        }

        .display-block {
            display: block;
            width: 100%;
            margin-top: 10px;
            margin-bottom: 10px;
        }

            .display-block label {
                display: inline-block;
                font-weight: bold;
                width: 15%;
            }

            .display-block input[type='text'] {
                display: inline-block;
                width: 30%;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1><%= this.PageTitle %></h1>
        <div class="content-wrapper">
            <div class="divStatus">
                <div id="divSuccess" class="success" runat="server"></div>
                <div id="divError" class="error" runat="server"></div>
            </div>
            <asp:Panel ID="pnlContent" runat="server" CssClass="content-wrapper" DefaultButton="btnSave">
                <div class="display-block">
                    <asp:Label ID="lblFirstName" runat="server" Text="First Name" AssociatedControlID="txtFirstName"></asp:Label>
                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="200"></asp:TextBox>
                </div>
                <div class="display-block">
                    <asp:Label ID="lblLastName" runat="server" Text="Last Name" AssociatedControlID="txtLastName"></asp:Label>
                    <asp:TextBox ID="txtLastName" runat="server" MaxLength="200"></asp:TextBox>
                </div>
                <div class="display-block">
                    <asp:Label ID="lblEmail" runat="server" Text="Email" AssociatedControlID="txtEmail"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="500"></asp:TextBox>
                </div>
                <div class="display-block">
                    <asp:Label ID="lblPhone" runat="server" Text="Phone" AssociatedControlID="txtPhone"></asp:Label>
                    <asp:TextBox ID="txtPhone" runat="server" MaxLength="20"></asp:TextBox>
                </div>
                <div class="display-block">
                    <asp:Label ID="lblState" runat="server" Text="State" AssociatedControlID="txtState"></asp:Label>
                    <asp:TextBox ID="txtState" runat="server" MaxLength="100"></asp:TextBox>
                </div>
                <div class="display-block">
                    <asp:Label ID="lblCity" runat="server" Text="City" AssociatedControlID="txtCity"></asp:Label>
                    <asp:TextBox ID="txtCity" runat="server" MaxLength="100"></asp:TextBox>
                </div>
                <div class="display-block">
                    <asp:Label ID="lblStreet" runat="server" Text="Street" AssociatedControlID="txtStreet"></asp:Label>
                    <asp:TextBox ID="txtStreet" runat="server" MaxLength="100"></asp:TextBox>
                </div>
                <div class="display-block">
                    <asp:Label ID="lblZip" runat="server" Text="Zip" AssociatedControlID="txtZip"></asp:Label>
                    <asp:TextBox ID="txtZip" runat="server" MaxLength="10"></asp:TextBox>
                </div>
                <div class="display-block">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

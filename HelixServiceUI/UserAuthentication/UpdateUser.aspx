<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateUser.aspx.cs" Inherits="HelixServiceUI.UserAuthentication.updateuser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        th, td {
            padding: 10px;
        }

        a {
            padding: 5px;
        }

        .truncate {
            width: 100px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            display: inline-block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Repeater ID="rUsers" runat="server" OnItemCommand="rUsers_ItemCommand">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Guid
                        </th>
                        <th>Username
                        </th>
                        <th>Password
                        </th>
                        <th>Action
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="lblUserGuid" Text='<%# Eval("Guid") %>' runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblUserName" Text='<%# Eval("UserName") %>' runat="server" Width="120" CssClass="truncate"></asp:Label>
                        <asp:TextBox ID="txtUserName" Text='<%# Eval("UserName") %>' runat="server" Width="120" Visible="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblPassword" Text='<%# Eval("UserPassword") %>' runat="server" Width="120" CssClass="truncate"></asp:Label>
                        <asp:HiddenField ID="lblSalt" Value='<%# Eval("UserSalt") %>' runat="server" />
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="120" Visible="false"></asp:TextBox>
                    </td>
                    <td>
                        <div id="divAction" runat="server">
                            <asp:LinkButton ID="lbtnUpdate" CommandArgument='<%# Eval("Guid") %>' Text="Edit" CommandName="Update" ToolTip="Modify a user." runat="server"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Eval("Guid") %>' Text="Delete" CommandName="Delete" ToolTip="Delete a user." runat="server"></asp:LinkButton>
                        </div>
                        <div id="divCommit" runat="server" visible="false">
                            <asp:LinkButton ID="lbtnSubmit" CommandArgument='<%# Eval("Guid") %>' Text="Submit" CommandName="Submit" ToolTip="Submit changes." runat="server"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnCancel" CommandArgument='<%# Eval("Guid") %>' Text="Cancel" CommandName="Cancel" ToolTip="Cancel changes." runat="server"></asp:LinkButton>
                        </div>
                    </td>
                </tr>
                <asp:HiddenField ID="hdnfield" runat="server" Value='<%# Container.DataItem %>' />
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HelixServiceUI.XMLSerializer.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Employee List</title>
    <style>
        .content-wrapper {
            width: 980px;
        }

            .content-wrapper table {
                width: 100%;
                table-layout: fixed;
            }

                .content-wrapper table th, .content-wrapper table td {
                    text-align: left;
                }

        .search-wrapper {
            display: block;
            margin-top: 10px;
            margin-bottom: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Employee List</h1>
        <asp:Panel ID="pnlSearch" runat="server" CssClass="search-wrapper">
            Keyword:
                <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
        </asp:Panel>
        <div class="content-wrapper">
            <div>
                <a href="ManageEmployee.aspx">Add employee</a>
            </div>
            <asp:Repeater ID="rEmployeeList" runat="server">
                <HeaderTemplate>
                    <table>
                        <thead>
                            <tr>
                                <th>First Name</th>
                                <th>Last Name</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# DataBinder.Eval(Container.DataItem, "FirstName").ToString() %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "LastName").ToString() %></td>
                        <td>
                            <asp:HyperLink ID="lnkEdit" runat="server" NavigateUrl='<%# this.ResolveUrl("~/XMLSerializer/ManageEmployee.aspx?eid=" + DataBinder.Eval(Container.DataItem, "Guid").ToString())%>' Text="Edit"></asp:HyperLink>&nbsp;&nbsp;|&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Guid") %>' OnClick="lnkDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this employee?');" Text="Delete"></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
            </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
